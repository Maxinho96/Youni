using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using Amazon.S3.Model;
using Amazon.S3;

namespace Youni
{
    public class SubjectListViewModel : BindableObject
    {

        //temp
        public Command caricaRobba { get; set; }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public INavigation Navigation;
        public Command ClassChoosedCommand { get; set; }
        public Command AddClassesTapped { get; set; }
        public Class TappedClass { get; set; }
        private DataBaseHandler DBHandler;
        private ObservableCollection<Class> classes;
        public ObservableCollection<Class> Classes
        {
            get
            {
                return this.classes;
            }
            set
            {
                this.classes = value;
                OnPropertyChanged("Classes");
            }
        }

        public SubjectListViewModel()
        {
     
            this.DBHandler = new DataBaseHandler();
            this.ClassChoosedCommand = new Command(async () =>
            {
                await this.Navigation.PushAsync(new SubjectPage(new SubjectPageViewModel(this.TappedClass)));
            });
            this.AddClassesTapped = new Command(async () =>
            {
                await this.Navigation.PushAsync(new FacultyChooserPage(new FacultyChooserViewModel((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it")));
            });


            //temp
            /*this.caricaRobba = new Command(async () =>
            {
                //inserisci il codice che per ogni bucket presente su S3 aggiunga tutti gli object al suo interno nel DB usando DBHandler.CaricaRobba()
                Dictionary<string, string> buckets = new Dictionary<string, string>();
                buckets.Add("analisi.e.progettazione.del.software", "Analisi e progettazione del software");
                buckets.Add("basi.di.dati.i", "Basi di dati I");
                buckets.Add("economia.applicata.all.ingegneria", "Economia applicata all'ingegneria");
                buckets.Add("mobile.computing", "Mobile computing");
                buckets.Add("programmazione.funzionale", "Programmazione funzionale");
                buckets.Add("reti.di.calcolatori", "Reti di calcolatori");
                buckets.Add("sistemi.informativi.per.il.web", "Sistemi informativi per il web");
                buckets.Add("sistemi.operativi", "Sistemi operativi");

                IAmazonS3 client = new AmazonS3Client("AKIAJHHUXNC3T47W4KVQ", "D1sqEmlpFxhxkMe7z9XErIWU8B6IRDBC76bWG95/", Amazon.RegionEndpoint.EUCentral1);

                foreach(string key in buckets.Keys)
                { 
                    try
                    {
                        ListObjectsV2Request request = new ListObjectsV2Request
                        {
                            BucketName = key
                        };

                        ListObjectsV2Response response;
                        do
                        {
                            response = await client.ListObjectsV2Async(request);
                            
                            // Process response.
                            foreach (S3Object entry in response.S3Objects)
                            {
                                await this.DBHandler.caricaRobba(entry.Key, key, buckets[key]);
                            }

                            request.ContinuationToken = response.NextContinuationToken;
                        } while (response.IsTruncated == true);                        
                    }
                    catch (AmazonS3Exception amazonS3Exception)
                    {
                        if (amazonS3Exception.ErrorCode != null &&
                            (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                        {
                            Console.WriteLine("Check the provided AWS Credentials.");
                            Console.WriteLine("To sign up for service, go to http://aws.amazon.com/s3");
                        }
                        else
                        {
                            Console.WriteLine("Error occurred. Message:'{0}' when listing objects", amazonS3Exception.Message);
                        }
                    }
                }
                await Application.Current.MainPage.DisplayAlert("ho", "finito", "cazzo");
            });*/
        }

        public async Task GetClasses()
        {
            this.IsLoading = true;
            try
            {
                // Is user logged in?
                if ((bool)Application.Current.Properties["IsLoggedIn"]) // He is logged in
                {
                    this.Classes = await this.DBHandler.RetrieveFavouritesAsync((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it");
                }
            }
            catch (Exception ex) when (ex is System.Net.Sockets.SocketException || ex is Npgsql.NpgsqlException)
            {
                await Application.Current.MainPage.DisplayAlert("Errore", "AOAOAProblema di connessione", "Riprova");
                await this.GetClasses();
            }
            this.IsLoading = false;
        }

    }
}
