﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Youni
{
    public partial class FacultyChooserPage : ContentPage
    {
        public FacultyChooserPage()
        {
            InitializeComponent();
        }

        public FacultyChooserPage(FacultyChooserViewModel facultyChooserViewModel) : this()
        {
            facultyChooserViewModel.Navigation = this.Navigation;
            this.BindingContext = facultyChooserViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((FacultyChooserViewModel)this.BindingContext).LoadFaculties();
        }
    }
}
