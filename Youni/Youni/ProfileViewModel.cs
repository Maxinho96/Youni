using System;

using Xamarin.Forms;

namespace Youni
{
    public class ProfileViewModel : BindableObject
    {
        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        private DataBaseHandler DBHandler;
        public Command LogoutCommand { get; set; }

        public ProfileViewModel()
        {
            this.IsLoading = false;
            this.DBHandler = new DataBaseHandler();

            this.LogoutCommand = new Command(async () =>
            {
                Application.Current.Properties["IsLoggedIn"] = false;
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new LoginRegistrationPage(new LoginRegistrationViewModel()))
                {
                    BarBackgroundColor = Color.FromHex("#3A8FDA"),
                    BarTextColor = Color.White
                });
            });
        }
    }
}
