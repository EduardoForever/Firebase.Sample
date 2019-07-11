using Autofac;
using Autofac.Core;
using Firebase.sample.Models;
using Firebase.sample.Services;
using Firebase.sample.Services.Implementation;
using Firebase.sample.ViewModels;
using Firebase.sample.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firebase.sample
{
    public partial class App : Application
    {
        public IContainer Container { get; }

        public App(Module module)
        {
            InitializeComponent();
            Container = BuildContainer(module);
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        IContainer BuildContainer(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AddPlaceViewModel>().AsSelf();
            builder.RegisterType<PlaceDetailsViewModel>().AsSelf();
            builder.RegisterType<NavigationService>().AsSelf().SingleInstance();
            builder.RegisterType<MessageService>().AsSelf().SingleInstance();
            builder.RegisterType<FirebaseAuthService>().As<IFirebaseAuthService>().SingleInstance();
            builder.Register(componentContext => 
            {
                var firebaseAuthService = componentContext.Resolve<IFirebaseAuthService>();

                return new PlacesService(firebaseAuthService, Config.ApiKeys.FirebasePath); 
            }).As<IDataStore<Place>>();
            
            builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
