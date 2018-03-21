using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Youni
{
    public class ClassChooserViewModel : BindableObject
    {
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        public Faculty TappedFaculty { get; set; }
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
            }
        }
        public Class TappedClass { get; set; }
        private ObservableCollection<Year> years;
        public ObservableCollection<Year> Years
        {
            get
            {
                return this.years;
            }
            set
            {
                this.years = value;
                OnPropertyChanged("Years");
            }
        }
        private Collection<Class> SelectedClasses;
        public Command ClassChoosedCommand { get; set; }
        public Command YearTappedCommand { get; set; }
        public Command SkipCommand { get; set; }
        public Command ConfirmCommand { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;

        public ClassChooserViewModel()
        {
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();
            this.SelectedClasses = new Collection<Class>();

            this.YearTappedCommand = new Command(async (descriptionTapped) =>
            {
                this.IsLoading = true;
                try
                {
                    foreach (Year y in this.Years)
                    {
                        if (y.Key == (string)descriptionTapped)
                        {

                            if (y.Count == 0)
                            {
                                ObservableCollection<Class> classes = await this.DBHandler.GetClassesAsync(this.TappedFaculty, y);
                                foreach (Class c in classes)
                                {
                                    y.Add(c);
                                }
                            }
                            else
                            {
                                foreach (Class c in y)
                                {
                                    this.SelectedClasses.Remove(c);
                                }
                                y.Clear();
                            }
                        }

                    }
                    this.IsLoading = false;
                }
                catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                {
                    this.IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                    await this.LoadYears();
                }
            });

            this.ClassChoosedCommand = new Command(() =>
            {
                this.TappedClass.ChangeButtonColor();
                if (this.SelectedClasses.Contains(this.TappedClass))
                {
                    this.SelectedClasses.Remove(this.TappedClass);
                }
                else
                {
                    this.SelectedClasses.Add(this.TappedClass);
                }
            });

            this.SkipCommand = new Command(async () =>
            {
                this.IsLoading = true;
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegName, this.RegSurname, this.RegEmail, this.RegPassword);
                    Application.Current.Properties["IsLoggedIn"] = true;
                    await Application.Current.SavePropertiesAsync();
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    this.IsLoading = false;
                }
                catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                {
                    this.IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });

            this.ConfirmCommand = new Command(async () =>
            {
                this.IsLoading = true;
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegName, this.RegSurname, this.RegEmail, this.RegPassword);
                    await this.DBHandler.InsertFavouritesAsync(this.RegEmail, this.TappedFaculty, this.SelectedClasses);
                    Application.Current.Properties["UserEmail"] = this.RegEmail;
                    Application.Current.Properties["IsLoggedIn"] = true;
                    await Application.Current.SavePropertiesAsync();
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    this.IsLoading = false;
                }
                catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                {
                    this.IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });
        }

        public ClassChooserViewModel(string regName, string regSurname, string regEmail, string regPassword, Faculty tappedFaculty) : this()
        {
            this.RegName = regName;
            this.RegSurname = regSurname;
            this.RegEmail = regEmail;
            this.RegPassword = regPassword;
            this.TappedFaculty = tappedFaculty;
        }

        public async Task LoadYears()
        {
            this.IsLoading = true;
            try
            {
                this.Years = await this.DBHandler.GetYearsAsync(this.TappedFaculty);
                this.IsLoading = false;
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                this.IsLoading = false;
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.LoadYears();
            }
        }
    }
}
