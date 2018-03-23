using System;

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
                OnPropertyChanged("IsInteractionEnabled");
            }
        }
        public bool IsInteractionEnabled
        {
            get
            {
                return !this.IsLoading;
            }
        }
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        public string RegPasswordConfirm { get; set; }
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
            this.IsLoading = false;
            this.DBHandler = new DataBaseHandler();
            this.PageTitle = "Registrazione";

            this.LoginCommand = new Command(async () =>
           {
               this.IsLoading = true;
               try
               {
                   string fullEmail = this.LogEmail + "@stud.uniroma3.it";
                   bool noEmal = String.IsNullOrWhiteSpace(this.LogEmail), noPassword = String.IsNullOrWhiteSpace(this.LogPassword);
                   if (noEmal && noPassword)
                   {
                       this.IsLoading = false;
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire un'email ed una password", "Riprova");
                   }
                   else if (noEmal)
                   {
                       this.IsLoading = false;
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire l'email", "Riprova");
                   }
                   else if (noPassword)
                   {
                       this.IsLoading = false;
                       await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi inserire la password", "Riprova");
                   }
                   else if (!(await DBHandler.IsRegisteredAsync(fullEmail)))
                   {
                       this.IsLoading = false;
                       await Application.Current.MainPage.DisplayAlert("Errore", "Email non registrata", "Riprova");
                   }
                   else if (await this.DBHandler.CheckCredentialsAsync(fullEmail, this.LogPassword))
                   {
                       Application.Current.Properties["UserEmail"] = this.LogEmail;
                       Application.Current.Properties["IsLoggedIn"] = true;
                       await Application.Current.SavePropertiesAsync();
                       await Application.Current.MainPage.Navigation.PopModalAsync();
                       this.IsLoading = false;
                   }
                   else
                   {
                       this.IsLoading = false;
                       await Application.Current.MainPage.DisplayAlert("Errore", "Password errata", "Riprova");
                   }
               }
               catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
               {
                   this.IsLoading = false;
                   await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
               }
           });

            this.RegisterCommand = new Command(async () =>
            {
                this.IsLoading = true;
                string fullEmail = this.RegEmail + "@stud.uniroma3.it";
                try
                {
                    if (String.IsNullOrWhiteSpace(this.RegEmail) || String.IsNullOrWhiteSpace(this.RegPassword) || String.IsNullOrWhiteSpace(this.RegName) || String.IsNullOrWhiteSpace(this.RegSurname))
                    {
                        this.IsLoading = false;
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi riempire tutti i campi", "Riprova");
                    }
                    else if (this.RegPassword != this.RegPasswordConfirm)
                    {
                        this.IsLoading = false;
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Le due password devono coincidere", "Riprova");
                    }
                    else if (await this.DBHandler.IsRegisteredAsync(fullEmail))
                    {
                        this.IsLoading = false;
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Questa email risulta già registrata", "OK");
                    }
                    else
                    {
                        await this.Navigation.PushAsync(new FacultyChooserPage(new FacultyChooserViewModel(this.RegName, this.RegSurname, this.RegEmail, this.RegPassword)));
                        this.IsLoading = false;
                    }
                }
                catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                {
                    this.IsLoading = false;
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
