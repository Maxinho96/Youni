using System;

using Xamarin.Forms;

using Naxam.Controls.Forms;

namespace Youni
{
    public partial class MainPage : BottomTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (this.CurrentPage.GetType() == typeof(ProfilePage))
            {
                await ((ProfilePage)this.CurrentPage).ForceAppearing();
            }
            // Is user logged in?
            if (!(bool)Application.Current.Properties["IsLoggedIn"]) // He is not logged in
            {
                await this.Navigation.PushModalAsync(new NavigationPage(new LoginRegistrationPage(new LoginRegistrationViewModel()))
                {
                    BarBackgroundColor = Color.FromHex("#3A8FDA"),
                    BarTextColor = Color.White
                });
            }
        }
    }
}
