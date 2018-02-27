using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace Youni
{
    public class LoginRegistrationViewModel : BindableObject
    {
        private int currentPage;
        public int CurrentPage // Current Page (0=RegistrationView, 1=LoginView)
        {
            get
            {
                return currentPage;
            }
            set
            {
                this.currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        private string pageTitle;
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                this.pageTitle = value;
                OnPropertyChanged("PageTitle");
            }
        }
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        public string LogEmail { get; set; }
        public string LogPassword { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command RegistrationSwitchCommand { get; set; }
        public Command LoginSwitchCommand { get; set; }

        public LoginRegistrationViewModel()
        {
            this.DBHandler = new DataBaseHandler();
            this.PageTitle = "Registrazione";

            this.LoginCommand = new Command(async () =>
           {
               try
               {
                   bool noEmal = String.IsNullOrWhiteSpace(this.LogEmail), noPassword = String.IsNullOrWhiteSpace(this.LogPassword);
                   if (noEmal && noPassword)
                   {
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire un'email ed una password", "Riprova");
                   }
                   else if (noEmal)
                   {
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire l'email", "Riprova");
                   }
                   else if (noPassword)
                   {
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire la password", "Riprova");
                   }
                   else if (!(await DBHandler.IsRegisteredAsync(this.LogEmail)))
                   {
                       await Application.Current.MainPage.DisplayAlert("Errore", "Email non registrata", "Riprova");
                   }
                   else if (await this.DBHandler.CheckCredentialsAsync(this.LogEmail, this.LogPassword))
                   {
                       await Application.Current.MainPage.Navigation.PopModalAsync();
                   }
                   else
                   {
                       await Application.Current.MainPage.DisplayAlert("Errore", "Password errata", "Riprova");
                       //Application.Current.Properties["IsLoggedIn"] = true;
                   }
               }
               catch (System.Net.Sockets.SocketException)
               {
                   await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
               }
           });

            this.RegisterCommand = new Command(async () =>
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(this.RegEmail) || String.IsNullOrWhiteSpace(this.RegPassword) || String.IsNullOrWhiteSpace(this.RegName) || String.IsNullOrWhiteSpace(this.RegSurname))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi riempire tutti i campi", "Riprova");
                    }
                    else if (!(new Regex(@"^.*@stud\.uniroma3\.it$").IsMatch(this.RegEmail)))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire la tua email istituzionale (es. mario.rossi@stud.uniroma3.it", "Riprova");
                    }
                    else if (await this.DBHandler.IsRegisteredAsync(this.RegEmail))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Questa email risulta già registrata", "OK");
                    }
                    else
                    {
                        await this.Navigation.PushAsync(new FacultyChooserPage(new FacultyChooserViewModel(this.RegName, this.RegSurname, this.RegEmail, this.RegPassword)));
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            this.RegistrationSwitchCommand = new Command(() =>
           {
               this.CurrentPage = 0;
               this.PageTitle = "Registrazione";
           });

            this.LoginSwitchCommand = new Command(() =>
            {
                this.CurrentPage = 1;
                this.PageTitle = "Login";
            });
        }

        //public LoginRegistrationViewModel(INavigation navigation) : this()
        //{
        //    this.Navigation = navigation;
        //}
    }
}
