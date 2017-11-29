using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class PresentationPage : CarouselPage
    {
        public PresentationPage()
        {
            InitializeComponent();
        }

        void RegistrationSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            RegistrationSwitchButton.TextColor = Color.Gray;
            RegistrationSwitchButton.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void RegistrationSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            RegistrationSwitchButton.TextColor = Color.FromHex("#45BFEE");
            await RegistrationSwitchButton.FadeTo(1, 500);
#endif
        }

        void RegistrationSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            this.CurrentPage = RegistrationPage;
        }

        void LoginSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            LoginSwitchButton.TextColor = Color.Gray;
            LoginSwitchButton.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void LoginSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            LoginSwitchButton.TextColor = Color.FromHex("#45BFEE");
            await LoginSwitchButton.FadeTo(1, 500);
#endif
        }

        void LoginSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            this.CurrentPage = LoginPage;
        }
    }
}
