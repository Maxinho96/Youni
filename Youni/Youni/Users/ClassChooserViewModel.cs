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
        public Faculty FacultyTapped { get; set; }
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
        public Class ClassTapped { get; set; }
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
        public Command ClassChoosedCommand { get; set; }
        public Command YearTappedCommand { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;

        public ClassChooserViewModel()
        {
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();

            this.YearTappedCommand = new Command(async (descriptionTapped) =>
            {
                foreach (Year y in this.Years)
                {
                    if(y.Key == (string) descriptionTapped)
                    {
                        y.Clear();
                        ObservableCollection<Class> classes = await this.DBHandler.GetClassesAsync(this.FacultyTapped, y);
                        foreach(Class c in classes)
                        {
                            y.Add(c);
                        }
                    }
                    else
                    {
                        y.Clear();
                    }
                }
            });

            this.ClassChoosedCommand = new Command(async () =>
            {
                //try
                //{
                //    await this.DBHandler.InsertUserAsync(this.RegEmail, this.RegPassword, this.RegName, this.RegSurname);
                //    await Application.Current.MainPage.Navigation.PopModalAsync();
                //}
                //catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
                //{
                //    await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                //}
            });
        }

        public ClassChooserViewModel(string regName, string regSurname, string regEmail, string regPassword, Faculty facultyTapped) : this()
        {
            this.RegName = regName;
            this.RegSurname = regSurname;
            this.RegEmail = regEmail;
            this.RegPassword = regPassword;
            this.FacultyTapped = facultyTapped;
        }

        public async Task LoadYears()
        {
            try
            {
                this.Years = await this.DBHandler.GetYearsAsync(this.FacultyTapped);
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.LoadYears();
            }
        }
    }
}
