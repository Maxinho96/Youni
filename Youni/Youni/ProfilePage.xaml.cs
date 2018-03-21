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
