using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public partial class PresentationPage : ContentPage
    {
        public PresentationPage()
        {
            InitializeComponent();
        }

        async void Register_Handle_Clicked(object sender, System.EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        void RegistrationSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void RegistrationSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
#endif
        }

        void RegistrationSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            Carousel.Position = 0;
        }

        async void Login_Handle_Clicked(object sender, System.EventArgs e)
        {
            /*if (String.IsNullOrEmpty(EmailEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text))
                await this.DisplayAlert("Errore", "Inserisci una email ed una password", "Riprova");
            else
            {*/
            var connString = "Host=younidb.cw9vlhucwihr.eu-central-1.rds.amazonaws.com;Port=5432;Username=younidbmaster;Password=Y982fZhd9B8r;Database=younidb";
                var conn = new NpgsqlConnection(connString);
                conn.Open();
            //String query = String.Format("SELECT * FROM utenti WHERE email = {0} AND password = {1}", EmailEntry.Text, PasswordEntry.Text);
            String query = "SELECT * FROM utenti WHERE email='mas.bruni@stud.uniroma3.it' AND password='max'";
                var cmd = new NpgsqlCommand(query, conn);
                cmd.Prepare();
                if (await cmd.ExecuteScalarAsync() != null)
                    await this.Navigation.PopModalAsync();
                else
                    await this.DisplayAlert("Errore", "Email e/o password errati", "Riprova");
            //}
        }

        void LoginSwitch_Handle_Pressed(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender; 
            button.TextColor = Color.Gray;
            button.Opacity = 0.1;
#endif
        }

#if __ANDROID__
            async
#endif
        void LoginSwitch_Handle_Released(object sender, System.EventArgs e)
        {
#if __ANDROID__
            var button = (Button)sender;
            button.TextColor = Color.FromHex("#45BFEE");
            await button.FadeTo(1, 500);
#endif
        }

        void LoginSwitch_Handle_Clicked(object sender, System.EventArgs e)
        {
            Carousel.Position = 1;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
