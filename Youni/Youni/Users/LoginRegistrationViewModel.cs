using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public class LoginRegistrationViewModel
    {
        public int CurrentPage { get; set; } // Pagina corrente (0=RegistrationView, 1=LoginView)
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginRegistrationViewModel()
        {
            LoginCommand = new Command(async () =>
           {
               //var connString = "Host=younidb.cw9vlhucwihr.eu-central-1.rds.amazonaws.com;Port=5432;Username=younidbmaster;Password=Y982fZhd9B8r;Database=younidb";
               //var conn = new NpgsqlConnection(connString);
               //conn.Open();
               //String query = "SELECT * FROM utenti WHERE email='mas.bruni@stud.uniroma3.it' AND password='max'";
               //var cmd = new NpgsqlCommand(query, conn);
               //cmd.Prepare();
               //if (await cmd.ExecuteScalarAsync() != null)
               await Application.Current.MainPage.Navigation.PopModalAsync();
               //else
               //   await Application.Current.MainPage.DisplayAlert("Errore", "Email e/o password errati", "Riprova");
            });

            RegisterCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
        }
    }
}
