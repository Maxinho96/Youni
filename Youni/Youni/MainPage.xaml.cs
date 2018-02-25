using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Is user logged in?
            if (!(bool)Application.Current.Properties["IsLoggedIn"]) // He is not logged in
            {
                await this.Navigation.PushModalAsync(new NavigationPage(new LoginRegistrationPage(new LoginRegistrationViewModel())));
            }
        }
    }
}
