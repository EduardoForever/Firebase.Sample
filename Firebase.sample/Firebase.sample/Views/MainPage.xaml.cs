using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Firebase.sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firebase.sample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            this.viewModel = (Application.Current as App).Container.Resolve<MainViewModel>();

            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.OnLoadCommand.Execute(null);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.viewModel.OpenPlaceCommand.Execute(e.Item);
        }
    }
}