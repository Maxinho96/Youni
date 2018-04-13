using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Youni
{
    public partial class SubjectPage : NavigationPage
    {
        public SubjectPage()
        {
            InitializeComponent();
        }
        public SubjectPage(SubjectPageViewModel subjectPageViewModel) : this()
        {
            subjectPageViewModel.Navigation = this.Navigation;
            this.BindingContext = subjectPageViewModel;
        }
        public async Task ForceAppearing()
        {
            await Task.Run(() => OnAppearing());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((SubjectPageViewModel)this.BindingContext).GetResources();
        }

    }
}