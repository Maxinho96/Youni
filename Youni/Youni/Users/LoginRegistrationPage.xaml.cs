using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;
using EntryCustomReturn.Forms.Plugin.Abstractions;

namespace Youni
{
    public partial class LoginRegistrationPage : ContentPage
    {
        public LoginRegistrationPage()
        {
            InitializeComponent();
        }

        public LoginRegistrationPage(LoginRegistrationViewModel loginRegistrationViewModel) : this()
        {
            loginRegistrationViewModel.Navigation = this.Navigation;
            this.BindingContext = loginRegistrationViewModel;
        }

        /* Disables back button on Android */
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
