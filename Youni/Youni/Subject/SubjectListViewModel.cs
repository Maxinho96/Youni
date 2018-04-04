using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace Youni
{
    public class SubjectListViewModel : BindableObject
    {
        public INavigation Navigation;
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

        }

        public async Task GetClasses()
        {
            this.Classes = await this.DBHandler.RetrieveFavouritesAsync((string)Application.Current.Properties["UserEmail"]+"@stud.uniroma3.it");
        }

    }
}
