using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public class LoginRegistrationViewModelOld
    {
        public int CurrentPage { get; set; } // Pagina corrente (0=RegistrationView, 1=LoginView)
        public List<DataTemplate> Pages { get; set; } // Lista dei DataTemplate, uno per ogni pagina
        DataTemplate RegistrationPageTemplate = new DataTemplate(() => { return new RegistrationView(); }); // DataTemplate della RegistrationView
        DataTemplate LoginPageTemplate = new DataTemplate(() => { return new LoginView(); }); // DataTemplate della LoginView
        public Command LoginSwitchCommand { get; set; } // Comando del bottone per passare da RegistrationView a LoginView

        public LoginRegistrationViewModelOld()
        {
            /* Inizializzo i DataTemplates */
            this.Pages = new List<DataTemplate>()
            {
                RegistrationPageTemplate,
                LoginPageTemplate
            };

            /* Inizializzo i comandi */
            LoginSwitchCommand = new Command( () =>
            {
                this.CurrentPage = 1;
            } );
        }
    }
}
