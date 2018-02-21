using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Youni
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
