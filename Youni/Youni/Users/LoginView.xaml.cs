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

#if __IOS__
            this.LogEmailEntryiOS.ReturnCommand = new Command(() => LogPasswordEntryiOS.Focus());
#elif __ANDROID__
            this.LogEmailEntryAndroid.ReturnCommand = new Command(() => LogPasswordEntryAndroid.Focus());
#endif
        }

        void RegistrationSwitch_Handle_Pressed_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
        }

        async void RegistrationSwitch_Handle_Released_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
        }
    }
}
