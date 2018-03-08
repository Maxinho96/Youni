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
        private ObservableCollection<ClassGroup> groupedClasses;
        public ObservableCollection<ClassGroup> GroupedClasses
        {
            get
            {
                return this.groupedClasses;
            }
            set
            {
                this.groupedClasses = value;
                OnPropertyChanged("GroupedClasses");
            }
        }
        public Command ClassChoosedCommand { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;

        public ClassChooserViewModel()
        {
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();

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

        public ClassChooserViewModel(string regName, string regSurname, string regEmail, string regPassword) : this()
        {
            this.RegName = regName;
            this.RegSurname = regSurname;
            this.RegEmail = regEmail;
            this.RegPassword = regPassword;
        }

        public async Task OnAppearing()
        {
            try
            {
                this.IsLoading = true;
                ObservableCollection<Class> classes = await this.DBHandler.GetClassesAsync();
                ClassGroup g1 = new ClassGroup("Primo anno");
                ClassGroup g2 = new ClassGroup("Secondo anno");
                ClassGroup g3 = new ClassGroup("Terzo anno");
                foreach(Class c in classes)
                {
                    switch(c.Year)
                    {
                        case 1:
                            g1.Add(c);
                            break;
                        case 2:
                            g2.Add(c);
                            break;
                        case 3:
                            g3.Add(c);
                            break;
                    }
                }
                ObservableCollection<ClassGroup> groups = new ObservableCollection<ClassGroup>();
                groups.Add(g1);
                groups.Add(g2);
                groups.Add(g3);
                this.GroupedClasses = groups;
                this.IsLoading = false;
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.OnAppearing();
            }
        }
    }
}
