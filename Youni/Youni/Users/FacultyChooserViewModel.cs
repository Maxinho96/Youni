using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Youni
{
    public class FacultyChooserViewModel : BindableObject
    {
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
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
        public Faculty FacultyTapped { get; set; }
        private ObservableCollection<Faculty> faculties;
        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                return faculties;
            }
            set
            {
                this.faculties = value;
                OnPropertyChanged("Faculties");
            }
        }
        public Command FacultyChoosedCommand { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;

        public FacultyChooserViewModel()
        {
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();

            this.FacultyChoosedCommand = new Command(async () =>
            {
                try
                {
                    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                {
                    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                }
            });
        }

        public FacultyChooserViewModel(string regName, string regSurname, string regEmail, string regPassword) : this()
        {
            this.RegName = regName;
            this.RegSurname = regSurname;
            this.RegEmail = regEmail;
            this.RegPassword = regPassword;
        }

        public async Task LoadFaculties()
        {
            try
            {
                this.IsLoading = true;
                this.Faculties = await this.DBHandler.GetFacultiesAsync();
                this.IsLoading = false;
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.LoadFaculties();
            }
        }

        //public FacultyChooserViewModel(INavigation navigation) : this()
        //{
        //    this.Navigation = navigation;
        //}
    }
}
