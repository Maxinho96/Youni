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

        protected async void OnDocumentTapped(object sender, EventArgs args)
        {
            var doc = (Label)sender;
            await ((SubjectPageViewModel)this.BindingContext).GetDocument(doc.Text);
        }


    }
}