using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class ClassChooserPage : ContentPage
    {
        public ClassChooserPage()
        {
            InitializeComponent();
        }

        public ClassChooserPage(ClassChooserViewModel classChooserViewModel) : this()
        {
            classChooserViewModel.Navigation = this.Navigation;
            this.BindingContext = classChooserViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((ClassChooserViewModel)this.BindingContext).OnAppearing();
        }
    }
}
