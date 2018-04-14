using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace Youni
{
    public class SubjectListViewModel : BindableObject
    {
        public INavigation Navigation;
        public Command ClassChoosedCommand { get; set; }
        public Class TappedClass { get; set; }
        private DataBaseHandler DBHandler;
        private ObservableCollection<Class> classes;
        public ObservableCollection<Class> Classes
        {
            get
            {
                return this.classes;
            }
            set
            {
                this.classes = value;
                OnPropertyChanged("Classes");
            }
        }

        public SubjectListViewModel()
        {
     
            this.DBHandler = new DataBaseHandler();
            this.ClassChoosedCommand = new Command(async () =>
            {
                await this.Navigation.PushAsync(new SubjectPage(new SubjectPageViewModel(this.TappedClass.Name)));
            });

        }

        public async Task GetClasses()
        {
            try
            {
                // Is user logged in?
                if ((bool)Application.Current.Properties["IsLoggedIn"]) // He is logged in
                {
                    this.Classes = await this.DBHandler.RetrieveFavouritesAsync((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it");
                }
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.GetClasses();
            }
        }

    }
}
