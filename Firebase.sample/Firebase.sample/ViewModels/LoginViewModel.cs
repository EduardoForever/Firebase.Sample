using Firebase.sample.Services;
using Firebase.sample.Services.Implementation;
using Firebase.sample.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Firebase.sample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly IFirebaseAuthService firebaseAuthService;
        readonly NavigationService navigationService;

        private string email;
        private string password;

        public String Email
        {
            get => this.email;
            set => Set<string>(() => this.Email, ref this.email, value);
        }

        public String Password
        {
            get => this.password;
            set => Set<string>(() => this.Password, ref this.password, value);
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(
            IFirebaseAuthService firebaseAuthService,
            NavigationService navigationService)
        {
            this.firebaseAuthService = firebaseAuthService;
            this.navigationService = navigationService;

            this.LoginCommand = new RelayCommand(async () => await Login());

            this.Email = "porchinskiy@gmail.com";
            this.Password = "123456";
        }

        async Task Login()
        {
            this.IsBusy = true;

            try
            {
                var isSuccess = await this.firebaseAuthService.LoginAsync(this.Email, this.Password);

                if (isSuccess)
                {
                    await this.navigationService.PushAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            this.IsBusy = false;
        }
    }
}
