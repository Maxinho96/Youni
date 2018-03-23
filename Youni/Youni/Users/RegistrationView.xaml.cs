using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Youni
{
    public partial class RegistrationView : ContentView
    {
        public RegistrationView()
        {
            InitializeComponent();

#if __IOS__
            this.RegNameEntryiOS.ReturnCommand = new Command(() => RegSurnameEntryiOS.Focus());
            this.RegSurnameEntryiOS.ReturnCommand = new Command(() => RegEmailEntryiOS.Focus());
            this.RegEmailEntryiOS.ReturnCommand = new Command(() => RegPasswordEntryiOS.Focus());
            this.RegPasswordEntryiOS.ReturnCommand = new Command(() => RegPasswordConfirmEntryiOS.Focus());
#elif __ANDROID__
            this.RegNameEntryAndroid.ReturnCommand = new Command(() => RegSurnameEntryAndroid.Focus());
            this.RegSurnameEntryAndroid.ReturnCommand = new Command(() => RegEmailEntryAndroid.Focus());
            this.RegEmailEntryAndroid.ReturnCommand = new Command(() => RegPasswordEntryAndroid.Focus());
            this.RegPasswordEntryAndroid.ReturnCommand = new Command(() => RegPasswordConfirmEntryAndroid.Focus());
#endif
        }

        void LoginSwitch_Handle_Pressed_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
        }

        async void LoginSwitch_Handle_Released_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
        }
    }
}
