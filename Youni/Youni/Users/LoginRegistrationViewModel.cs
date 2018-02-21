using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public class LoginRegistrationViewModel
    {
        //public int CurrentPage { get; set; } // Pagina corrente (0=RegistrationView, 1=LoginView)
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        public string LogEmail { get; set; }
        public string LogPassword { get; set; }
        private DataBaseHandler DBHandler;

        public LoginRegistrationViewModel()
        {
            DBHandler = new DataBaseHandler();

            LoginCommand = new Command(async () =>
           {
               try
               {
                   if (String.IsNullOrWhiteSpace(this.LogEmail) || String.IsNullOrWhiteSpace(this.LogPassword))
                   {
                       await Application.Current.MainPage.DisplayAlert("Errore", "Devi inserire un'email ed una password", "Riprova");
                   }
                   else if (await this.DBHandler.CheckCredentialsAsync(this.LogEmail, this.LogPassword))
                   {
                       await Application.Current.MainPage.Navigation.PopModalAsync();
                   }
                   else
                   {
                       await Application.Current.MainPage.DisplayAlert("Errore", "Email e/o password errati", "Riprova");
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
                    if (String.IsNullOrWhiteSpace(this.RegEmail) || String.IsNullOrWhiteSpace(this.RegPassword) || String.IsNullOrWhiteSpace(this.RegName) || String.IsNullOrWhiteSpace(this.RegPassword))
                    {
                        await Application.Current.MainPage.DisplayAlert("Errore", "Devi riempire tutti i campi", "Riprova");
                    }
                    else if (await this.DBHandler.IsRegisteredAsync(this.RegEmail))
                    {
                        await Application.Current.MainPage.DisplayAlert("Attenzione", "Questa email risulta già registrata", "OK");
                    }
                    else if (await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname))
                    {
                        await Application.Current.MainPage.DisplayAlert("Complimenti!", "Ora sei un nuovo membro di Youni!", "OK");
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        //Application.Current.Properties["IsLoggedIn"] = true;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });
        }
    }
}
