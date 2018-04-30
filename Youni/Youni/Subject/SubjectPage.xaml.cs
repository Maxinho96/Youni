using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Amazon.S3;
using Amazon.S3.Model;

namespace Youni
{
    public partial class SubjectPage : ContentPage
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

        public async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            await ((SubjectPageViewModel)this.BindingContext).Search_Async(sender, e);
        }
    }
}