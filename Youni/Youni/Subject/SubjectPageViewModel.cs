using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Youni
{
    public class SubjectPageViewModel : BindableObject
    {
        public INavigation Navigation;
        public Command NotifyTapped { get; set; }
        public Command FavouritesTapped { get; set; }
        public Command SearchCommand { get; set; }
        public string SubjectName { get; set; }
        private ObservableCollection<Document> documentsList;
        public ObservableCollection<Document> DocumentsList
        {
            get
            {
                return this.documentsList;
            }
            set
            {
                this.documentsList = value;
                OnPropertyChanged("DocumentsList");
            }
        }

        public SubjectPageViewModel(string subjectName)
        {
            this.SubjectName = subjectName;
            this.NotifyTapped = new Command(async () =>
            {

            });
            this.FavouritesTapped = new Command(async () =>
            {

            });
            this.SearchCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("titolo", "RICERCA", "cancel");
            });
        }
        public async Task GetResources()
        {

        }



    }
}
