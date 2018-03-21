using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        void Logout_Handle_Pressed_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
        }

        async void Logout_Handle_Released_Android(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.TextColor = Color.Red;
            await button.FadeTo(1, 500);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if ((bool)Application.Current.Properties["IsLoggedIn"])
            {
                await ((ProfileViewModel)this.BindingContext).LoadUser();
            }
        }

		protected override void OnDisappearing()
		{
            base.OnDisappearing();

            ((ProfileViewModel)this.BindingContext).Clear();
		}
	}
}
