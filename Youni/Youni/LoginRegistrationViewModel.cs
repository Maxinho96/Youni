using System;
using System.Collections.Generic;
using System.Data;

using Xamarin.Forms;

using Npgsql;

namespace Youni
{
    public class LoginRegistrationViewModel
    {
        public int CurrentPage { get; set; }
        public List<DataTemplate> Pages { get; set; }
        DataTemplate RegistrationPageTemplate = new DataTemplate(() => { return new RegistrationView(); });
        DataTemplate LoginPageTemplate = new DataTemplate(() => { return new LoginView(); });

        public LoginRegistrationViewModel()
        {
            this.Pages = new List<DataTemplate>()
            {
                RegistrationPageTemplate,
                LoginPageTemplate
            };
        }
    }
}
