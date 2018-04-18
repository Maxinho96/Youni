using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace Youni
{
    public class SubjectPageViewModel : BindableObject
    {
        public INavigation Navigation;
        public Command NotifyTapped { get; set; }
        public Command FavouritesTapped { get; set; }
        public Command SearchCommand { get; set; }
        public string SubjectName { get; set; }
        private ObservableCollection<Document> documentsList;
        public ObservableCollection<Document> DocumentsList
        {
            get
            {
                return this.documentsList;
            }
            set
            {
                this.documentsList = value;
                OnPropertyChanged("DocumentsList");
            }
        }

        private static IAmazonS3 client = new AmazonS3Client("AKIAJHHUXNC3T47W4KVQ", "D1sqEmlpFxhxkMe7z9XErIWU8B6IRDBC76bWG95/", Amazon.RegionEndpoint.EUCentral1);

        public SubjectPageViewModel(string subjectName)
        {
            this.SubjectName = subjectName;
            this.DocumentsList = new ObservableCollection<Document>();

            this.NotifyTapped = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("titolo", "NOTIFICHE", "cancel");
            });
            this.FavouritesTapped = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("titolo", "PREFERITI", "cancel");
            });
            this.SearchCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("titolo", "RICERCA", "cancel");
            });
        }

        public async Task GetDocument(string documentTitle)
        {
            
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = this.SubjectName.ToLower().Replace(' ', '.'),
                Key = documentTitle
            };

            using (GetObjectResponse response = await client.GetObjectAsync(request))
            {
                string dest;

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"..", "Personal", documentTitle);
                        break;
                    case Device.Android:
                        dest = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), documentTitle);
                        break;
                    default:
                        dest = "piattaforma non supportata";
                        break;
                }

                await response.WriteResponseStreamToFileAsync(dest,false);
            }
        }

        public async Task GetResources()
        {
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = this.SubjectName.ToLower().Replace(' ', '.')
                };

                ListObjectsV2Response response;
                do
                {
                    response = await client.ListObjectsV2Async(request);

                    // Process response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        this.DocumentsList.Add(new Document(entry.Key));
                    }

                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated == true);

                /*foreach (Document doc in this.DocumentsList)
                {
                    Console.WriteLine(doc.DocumentTitle);
                }*/

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
    }
}
