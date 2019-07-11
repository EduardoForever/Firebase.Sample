using Firebase.sample.Models;
using Firebase.sample.Services;
using Firebase.sample.Services.Implementation;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.sample.ViewModels
{
    public class PlaceDetailsViewModel : BaseViewModel
    {
        private readonly IDataStore<Place> placesSerive;

        readonly NavigationService navigationService;
        readonly MessageService messageService;

        private Place place;

        public Place Place
        {
            get => this.place;
            set => Set<Place>(() => this.Place, ref this.place, value);
        }

        public RelayCommand RemovePlaceCommand { get; set; }

        public PlaceDetailsViewModel(
            IDataStore<Place> placesSerive,
            NavigationService navigationService,
            MessageService messageService)
        {
            this.placesSerive = placesSerive;
            this.navigationService = navigationService;
            this.messageService = messageService;
            
            this.RemovePlaceCommand = new RelayCommand(() => this.DoRemovePlace());
        }
        private async void DoRemovePlace()
        {
            this.IsBusy = true;

            try
            {
                var isSuccess = await this.placesSerive.DeleteItem(this.Place);

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
