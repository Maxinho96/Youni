using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public partial class LoginRegistrationPage : ContentPage
    {
        public LoginRegistrationPage()
        {
            InitializeComponent();

            this.BindingContext = new LoginRegistrationViewModel(this.Navigation);
        }

        void RegistrationSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void RegistrationSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
#endif
        }

        void RegistrationSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            Carousel.Position = 0;
        }

        void LoginSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender; 
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void LoginSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
#endif
        }

        void LoginSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            Carousel.Position = 1;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
