using System;
using Xamarin.Forms;

namespace Youni
{
    public class FacultyChooserViewModel
    {
        public Command MecEngCommand { get; set; }
        public Command SofEngCommand { get; set; }
        public Command EleEngCommand { get; set; }
        public Command CivEngCommand { get; set; }

        public FacultyChooserViewModel()
        {
            MecEngCommand = new Command(async () =>
           {
                await Application.Current.MainPage.Navigation.PopModalAsync();
           });
            SofEngCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
            EleEngCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
            CivEngCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
        }
    }
}
