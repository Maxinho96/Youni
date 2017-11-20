using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void Handle_Pressed(object sender, System.EventArgs e)
        {
            #if __ANDROID__
            registerButton.TextColor = Color.Gray;
            registerButton.Opacity = 0.1;
            #endif
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            
        }

        async void Handle_Released(object sender, System.EventArgs e)
        {
            #if __ANDROID__
            registerButton.TextColor = Color.FromHex("#45BFEE");
            await registerButton.FadeTo(1, 500);
            #endif
        }
    }
}
