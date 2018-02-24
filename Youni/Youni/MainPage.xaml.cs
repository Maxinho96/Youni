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

            // Is user logged in?
            if (!(bool)Application.Current.Properties["IsLoggedIn"]) // He is not
            {
                this.Navigation.PushModalAsync(new LoginRegistrationPage());
            }
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // L'utente è loggato?
        //    if (!(bool)Application.Current.Properties["IsLoggedIn"]) // Non è loggato
        //    {
        //        await this.Navigation.PushModalAsync(new LoginRegistrationPage());
        //    }
        //}
    }
}
