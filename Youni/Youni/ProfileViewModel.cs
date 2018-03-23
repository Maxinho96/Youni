using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Youni
{
    public class ProfileViewModel : BindableObject
    {
        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        private bool canModify;
        public bool CanModify
        {
            get
            {
                return this.canModify;
            }
            set
            {
                this.canModify = value;
                OnPropertyChanged("CanModify");
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }
        private string surname;
        public string Surname
        {
            get
            {
                return this.surname;
            }
            set
            {
                this.surname = value;
                OnPropertyChanged("Surname");
            }
        }
        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged("Email");
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged("Password");
            }
        }
        private string passwordConfirm;
        public string PasswordConfirm
        {
            get
            {
                return this.passwordConfirm;
            }
            set
            {
                this.passwordConfirm = value;
                OnPropertyChanged("PasswordConfirm");
            }
        }
        private string modifyText;
        public string ModifyText
        {
            get
            {
                return this.modifyText;
            }
            set
            {
                this.modifyText = value;
                OnPropertyChanged("ModifyText");
            }
        }
        private DataBaseHandler DBHandler;
        public Command LogoutCommand { get; set; }
        public Command ModifyCommand { get; set; }

        public ProfileViewModel()
        {
            this.IsLoading = false;
            this.CanModify = false;
            this.DBHandler = new DataBaseHandler();
            this.ModifyText = "  Modifica dati  ";

            this.LogoutCommand = new Command(async () =>
            {
                Application.Current.Properties["IsLoggedIn"] = false;
                await Application.Current.SavePropertiesAsync();
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new LoginRegistrationPage(new LoginRegistrationViewModel()))
                {
                    BarBackgroundColor = Color.FromHex("#3A8FDA"),
                    BarTextColor = Color.White
                });
            });

            this.ModifyCommand = new Command(async () =>
            {
                if (this.Password == this.PasswordConfirm)
                {
                    if (this.CanModify)
                    {
                        try
                        {
                            this.IsLoading = true;
                            await this.DBHandler.UpdateUserAsync((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it", this.Name, this.Surname, this.Email + "@stud.uniroma3.it", this.Password);
                            Application.Current.Properties["UserEmail"] = this.Email;
                            await Application.Current.SavePropertiesAsync();
                            this.Password = null;
                            this.PasswordConfirm = null;
                            this.CanModify = false;
                            this.ModifyText = "  Modifica dati  ";
                            this.IsLoading = false;
                            await Application.Current.MainPage.DisplayAlert("Operazione completata", "Le modifiche sono state registrate correttamente", "OK");
                        }
                        catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                        {
                            await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                        }
                    }
                    else
                    {
                        this.CanModify = true;
                        this.ModifyText = "  Conferma modifiche  ";
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Attenzione", "Le due password devono coincidere", "Riprova");
                }
            });
        }

        public async Task LoadUser()
        {
            try
            {
                this.IsLoading = true;
                string userEmail = (string)Application.Current.Properties["UserEmail"];
                this.Name = await this.DBHandler.GetNameAsync(userEmail + "@stud.uniroma3.it");
                this.Surname = await this.DBHandler.GetSurameAsync(userEmail + "@stud.uniroma3.it");
                this.Email = userEmail;
                this.IsLoading = false;
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.LoadUser();
            }
        }

        public void Clear()
        {
            this.Name = null;
            this.Surname = null;
            this.Email = null;
            this.Password = null;
            this.PasswordConfirm = null;
        }
    }
}
