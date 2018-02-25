using System;

using Xamarin.Forms;

namespace Youni
{
    public class LoginRegistrationViewModel
    {
        //public int CurrentPage { get; set; } // Current Page (0=RegistrationView, 1=LoginView)
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command MecEngCommand { get; set; }
        public Command SofEngCommand { get; set; }
        public Command EleEngCommand { get; set; }
        public Command CivEngCommand { get; set; }
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        public string LogEmail { get; set; }
        public string LogPassword { get; set; }
        private DataBaseHandler DBHandler;
        private INavigation Navigation;

        public LoginRegistrationViewModel()
        {
            DBHandler = new DataBaseHandler();

            LoginCommand = new Command(async () =>
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

            RegisterCommand = new Command(async () =>
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(this.RegEmail) || String.IsNullOrWhiteSpace(this.RegPassword) || String.IsNullOrWhiteSpace(this.RegName) || String.IsNullOrWhiteSpace(this.RegSurname))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Devi riempire tutti i campi", "Riprova");
                    }
                    else if (await this.DBHandler.IsRegisteredAsync(this.RegEmail))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Questa email risulta già registrata", "OK");
                    }
                    else
                    {
                        await this.Navigation.PushAsync(new FacultyChooserPage());
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            MecEngCommand = new Command(async () =>
            {
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname, "Ingegneria Meccanica");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            SofEngCommand = new Command(async () =>
            {
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname, "Ingegneria Informatica");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            EleEngCommand = new Command(async () =>
            {
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname, "Ingegneria Elettronica");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            CivEngCommand = new Command(async () =>
            {
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname, "Ingegneria Civile");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });
        }

        public LoginRegistrationViewModel(INavigation navigation) : this()
        {
            this.Navigation = navigation;
        }
    }
}
