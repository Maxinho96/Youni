﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DLToolkit.Forms.Controls;

namespace Youni
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FlowListView.Init(); // Makes FlowListView work

            if (!Properties.ContainsKey("IsLoggedIn")) // First time opening the app
            {
                Properties["IsLoggedIn"] = false;
            }

            this.MainPage = new MainPage();
            //MainPage = new LoginRegistrationPage();
            //MainPage = new NavigationPage(new ClassChooserPage(new ClassChooserViewModel("a", "b", "b@stud.uniroma3.it", "a", new Faculty("Ingegneria Informatica", "a"))));
            //MainPage = new FacultyChooserPage(new FacultyChooserViewModel());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override async void OnSleep()
        {
            // Handle when your app sleeps
            await this.SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
