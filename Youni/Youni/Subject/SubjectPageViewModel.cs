using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Youni
{
    public class SubjectPageViewModel : BindableObject
    {
        public INavigation Navigation;
        public Command NotifyTapped { get; set; }
        public Command FavouritesTapped { get; set; }
        public string SubjectName { get; set; }

        public SubjectPageViewModel(string subjectName)
        {
            this.SubjectName = subjectName;
            this.NotifyTapped = new Command(async () =>
            {

            });
            this.FavouritesTapped = new Command(async () =>
            {

            });
        }
        public async Task GetResources()
        {
        }



    }
}
