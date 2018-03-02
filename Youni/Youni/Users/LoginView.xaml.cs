using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Youni
{
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();

            this.LogEmailEntry.ReturnCommand = new Command(() => LogPasswordEntry.Focus());
        }

        //public LoginView(LoginRegistrationViewModel loginRegistrationViewModel) : this()
        //{
        //    this.BindingContext = loginRegistrationViewModel;
        //}

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
    }
}
