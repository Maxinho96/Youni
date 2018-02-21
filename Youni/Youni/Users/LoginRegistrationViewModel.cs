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
                var query = String.Format("SELECT * FROM utenti WHERE email='{0}' AND password='{1}'", LogEmail, LogPassword);
                if(await DBHandler.IsQueryEmptyAsync(query))
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Email e/o password errati", "Riprova");
                    //Application.Current.Properties["IsLoggedIn"] = true;
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            });

            RegisterCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
        }
    }
}
