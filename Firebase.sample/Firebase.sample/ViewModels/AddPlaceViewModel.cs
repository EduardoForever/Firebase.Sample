using Firebase.sample.Models;
using Firebase.sample.Services;
using Firebase.sample.Services.Implementation;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.sample.ViewModels
{
    public class AddPlaceViewModel : BaseViewModel
    {
        private readonly IDataStore<Place> placesSerive;

        readonly NavigationService navigationService;
        readonly MessageService messageService;

        private string placeName;
        private string placeImage;
        private string placeDetails;

        public String PlaceName
        {
            get => this.placeName;
            set => Set<string>(() => this.PlaceName, ref this.placeName, value);
        }

        public String PlaceImage
        {
            get => this.placeImage;
            set => Set<string>(() => this.PlaceImage, ref this.placeImage, value);
        }

        public String PlaceDetails
        {
            get => this.placeDetails;
            set => Set<string>(() => this.PlaceDetails, ref this.placeDetails, value);
        }

        public RelayCommand AddPlaceCommand { get; set; }

        public AddPlaceViewModel(
            IDataStore<Place> placesSerive, 
            NavigationService navigationService,
            MessageService messageService)
        {
            this.placesSerive = placesSerive;
            this.navigationService = navigationService;
            this.messageService = messageService;

            this.PlaceImage = "https://www.lavocedinewyork.com/wp-content/uploads/2018/05/1.new-york-skyline-foto-sebastian-leon-1021-300x300.jpg";
            this.PlaceName = "New York";
            this.PlaceDetails = "Put some description about place here";

            this.AddPlaceCommand = new RelayCommand(() => this.DoAddPlace());
        }

        private async void DoAddPlace()
        {
            this.IsBusy = true;

            var place = new Place() { PlaceName = this.PlaceName, PlaceImage = this.PlaceImage, Description = this.PlaceDetails };

            try
            {
                var isSuccess = await this.placesSerive.AddItem(place);

                if (isSuccess)
                {
                    this.IsBusy = false;

                    await this.navigationService.PopAsync();
                }
                else
                {
                    await this.messageService.ShowDialog("Error", "Please, check your data and try again", "Ok");
                }
            }
            catch
            {
                await this.messageService.ShowDialog("Error", "Something going wrong", "Ok");
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
