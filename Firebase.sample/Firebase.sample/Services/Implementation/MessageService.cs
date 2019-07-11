using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Firebase.sample.Services.Implementation
{
    public class MessageService
    {
        public Task ShowDialog(string title, string message, string cancel) => (Application.Current as App).MainPage.DisplayAlert(title, message, cancel);
    }
}
