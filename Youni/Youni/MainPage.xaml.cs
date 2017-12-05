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

            // L'utente è loggato?
            if (!(bool) Application.Current.Properties["IsLoggedIn"]) // Non è loggato
            {
                this.Navigation.PushModalAsync(new LoginRegistrationPage());
            }
        }
    }
}
