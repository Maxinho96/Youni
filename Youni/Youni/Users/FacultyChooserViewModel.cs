﻿using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Youni
{
    public class FacultyChooserViewModel : BindableObject
    {
        public string RegName { get; set; }
        public string RegSurname { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }
        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public Faculty TappedFaculty { get; set; }
        private ObservableCollection<Faculty> faculties;
        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                return faculties;
            }
            set
            {
                this.faculties = value;
                OnPropertyChanged("Faculties");
            }
        }
        public Command FacultyChoosedCommand { get; set; }
        private DataBaseHandler DBHandler;
        public INavigation Navigation;

        public FacultyChooserViewModel()
        {
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();

            this.FacultyChoosedCommand = new Command(async () =>
            {
                await this.Navigation.PushAsync(new ClassChooserPage(new ClassChooserViewModel(this.RegName, this.RegSurname, this.RegEmail, this.RegPassword, this.TappedFaculty)));
                this.Faculties.Clear();
                this.IsLoading = true;
            });
        }

        public FacultyChooserViewModel(string regName, string regSurname, string regEmail, string regPassword) : this()
        {
            this.RegName = regName;
            this.RegSurname = regSurname;
            this.RegEmail = regEmail;
            this.RegPassword = regPassword;
        }

        //costruttore usato per aggiungere classes (dal bottone +)
        public FacultyChooserViewModel(string usrEmail)
        {
            this.RegEmail = usrEmail;
            this.IsLoading = true;
            this.DBHandler = new DataBaseHandler();

            this.FacultyChoosedCommand = new Command(async () =>
            {
                await this.Navigation.PushAsync(new ClassChooserPage(new ClassChooserViewModel(this.RegEmail, this.TappedFaculty)));
                this.Faculties.Clear();
                this.IsLoading = true;
            });
        }

        public async Task LoadFaculties()
        {
            try
            {
                this.IsLoading = true;
                this.Faculties = await this.DBHandler.GetFacultiesAsync();
                this.IsLoading = false;
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "Problema di connessione", "Riprova");
                await this.LoadFaculties();
            }
        }

        //public FacultyChooserViewModel(INavigation navigation) : this()
        //{
        //    this.Navigation = navigation;
        //}
    }
}
