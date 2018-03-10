using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DLToolkit.Forms.Controls;

namespace Youni
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FlowListView.Init(); // Makes FlowListView work

            if (!Properties.ContainsKey("IsLoggedIn")) // E' la prima volta che si apre l'app
            {
                Properties["IsLoggedIn"] = false;
            }

            //MainPage = new MainPage();
            //MainPage = new LoginRegistrationPage();
            MainPage = new ClassChooserPage(new ClassChooserViewModel("a", "b", "b@stud.uniroma3.it", "a", new Faculty("Ingegneria Informatica", "a")));
            //MainPage = new FacultyChooserPage(new FacultyChooserViewModel());
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
