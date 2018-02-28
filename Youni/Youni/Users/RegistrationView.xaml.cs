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

            this.RegNameEntry.ReturnCommand = new Command(() => RegSurnameEntry.Focus());
            this.RegSurnameEntry.ReturnCommand = new Command(() => RegEmailEntry.Focus());
            this.RegEmailEntry.ReturnCommand = new Command(() => RegPasswordEntry.Focus());
            this.RegPasswordEntry.ReturnCommand = new Command(() => RegPasswordConfirmEntry.Focus());
        }

        public RegistrationView(LoginRegistrationViewModel loginRegistrationViewModel) : this()
        {
            this.BindingContext = loginRegistrationViewModel;
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
    }
}
