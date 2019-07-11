using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Firebase.sample.Models;
using Firebase.sample.Services;
using Firebase.sample.Services.Implementation;
using Firebase.sample.Views;
using GalaSoft.MvvmLight.Command;

namespace Firebase.sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly NavigationService navigationService;
        private IDataStore<Place> placesSerive;

        private List<Place> places;

        public MainViewModel(
            IDataStore<Place> placesSerive,
            NavigationService navigationService)
        {
            this.placesSerive = placesSerive;
            this.navigationService = navigationService;

            this.AddPlaceCommand = new RelayCommand(() => this.DoAddPlace());
            this.OpenPlaceCommand = new RelayCommand<Place>((place) => this.DoOpenPlace(place));
        }

        public List<Place> Places
        {
            get => places;
            set => Set<List<Place>>(() => this.Places, ref places, value);
        }

        public RelayCommand AddPlaceCommand { get; set; }

        public RelayCommand<Place> OpenPlaceCommand { get; set; }


        public override void OnLoad()
        {
            base.OnLoad();

            this.LoadPlaces();
        }

        private async void LoadPlaces()
        {
            this.Places = await this.placesSerive.GetItemsAsync();
        }

        private void DoAddPlace()
        {
            this.navigationService.PushAsync(new AddPlacePage());
        }

        private void DoOpenPlace(Place place)
        {
            this.navigationService.PushAsync(new PlaceDetailsPage(place));
        }
    }
}
