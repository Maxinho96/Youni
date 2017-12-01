using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youni.Helpers;

namespace Youni
{
    public partial class App : Application
    {

        /*public string IsFirstTime
        {
            get { return Settings.GeneralSettings; }
            set
            {
                if (Settings.GeneralSettings == value)
                    return;
                Settings.GeneralSettings = value;
                OnPropertyChanged();
            }
        }*/

        public App()
        {
            InitializeComponent();

            /* L'app è stata aperta per la prima volta?
            if (IsFirstTime == "yes") // E' la prima volta
            {
                IsFirstTime = "no";
            }
            else // Non è la prima volta
            {
                
            }*/

            if (!Properties.ContainsKey("IsLoggedIn")) // E' la prima volta che si apre l'app
            {
                Properties["IsLoggedIn"] = false;
            }

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
