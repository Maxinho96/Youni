using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
#if __ANDROID__
using Android.Content;
using Android.Widget;
#elif __IOS__
using UIKit;
using Foundation;
#endif

namespace Youni
{
    public class SubjectPageViewModel : BindableObject
    {
        public INavigation Navigation;

        public DataBaseHandler DBHandler;

        public Command NotifyTapped { get; set; }
        public Command FavouritesTapped { get; set; }
        public Command SearchCommand { get; set; }
        public Command DocumentTapped { get; set; }
        public string SubjectName { get; set; }
        public string FacultyName { get; set; }
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

        private string notification_icon;
        public string Notification_icon
        {
            get
            {
                return this.notification_icon;
            }
            set
            {
                this.notification_icon = value;
                OnPropertyChanged("Notification_icon");
            }
        }

        private string favourites_icon;
        public string Favourites_icon
        {
            get
            {
                return this.favourites_icon;
            }
            set
            {
                this.favourites_icon = value;
                OnPropertyChanged("Favourites_icon");
            }
        }


        private static IAmazonS3 client = new AmazonS3Client("AKIAJHHUXNC3T47W4KVQ", "D1sqEmlpFxhxkMe7z9XErIWU8B6IRDBC76bWG95/", Amazon.RegionEndpoint.EUCentral1);

        public SubjectPageViewModel(Class tappedClass)
        {
            this.SubjectName = tappedClass.Name;
            this.FacultyName = tappedClass.Faculty;
            this.DocumentsList = new ObservableCollection<Document>();
            this.DBHandler = new DataBaseHandler();
            
            this.Notification_icon = "notification_off";
            this.Favourites_icon = "favourites_on";

            this.NotifyTapped = new Command(() =>
            {
                if (this.Notification_icon.Equals("notification_off"))
                    this.Notification_icon = "notification_on";
                else
                    this.Notification_icon = "notification_off";

            });
            this.FavouritesTapped = new Command(async () =>
            {
                if (this.Favourites_icon.Equals("favourites_off"))
                {
                    this.Favourites_icon = "favourites_on";
                    await this.DBHandler.InsertFavouriteAsync((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it", this.FacultyName, this.SubjectName);
                }
                else
                {
                    this.Favourites_icon = "favourites_off";
                    await this.DBHandler.RemoveFavouriteAsync((string)Application.Current.Properties["UserEmail"] + "@stud.uniroma3.it", this.FacultyName, this.SubjectName);
                }
            });
            this.SearchCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("titolo", "RICERCA", "cancel");
            });
            this.DocumentTapped = new Command(async (docTitle) =>
            {
                await this.GetDocument((string)docTitle);
            });
        }

        public async Task GetDocument(string documentTitle)
        {

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = this.SubjectName.ToLower().Replace(' ', '.').Replace('\'', '.'),
                Key = documentTitle
            };

            string filePath;

            using (GetObjectResponse response = await client.GetObjectAsync(request))
            {
                //switch (Device.RuntimePlatform)
                //{
                //case Device.iOS:
#if __IOS__
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Youni", documentTitle);
                //filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Personal", documentTitle);
#elif __ANDROID__
                //break;
                //case Device.Android:
                filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Youni", documentTitle);
                //break;
                //default:
#else
                filePath = "piattaforma non supportata";
#endif
                //break;
                //}

                //scrivo su file
                await response.WriteResponseStreamToFileAsync(filePath, false);
            }

            try
            {
                //switch (Device.RuntimePlatform)
                //{
                //case Device.iOS:
#if __IOS__

                var PreviewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filePath));
                PreviewController.Delegate = new UIDocumentInteractionControllerDelegateClass(UIApplication.SharedApplication.KeyWindow.RootViewController);
                Device.BeginInvokeOnMainThread(() =>
                {
                    PreviewController.PresentPreview(true);
                });

                //break;

                //case Device.Android:
#elif __ANDROID__

                var bytes = File.ReadAllBytes(filePath);

                string application = "";

                string extension = Path.GetExtension(filePath);

                switch (extension.ToLower())
                {
                    case ".doc":
                    case ".docx":
                        application = "application/msword";
                        break;
                    case ".pdf":
                        application = "application/pdf";
                        break;
                    case ".xls":
                    case ".xlsx":
                        application = "application/vnd.ms-excel";
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        application = "image/jpeg";
                        break;
                    default:
                        application = "*/*";
                        break;
                }

                Java.IO.File file = new Java.IO.File(filePath);
                file.SetReadable(true);

                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, application);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                try
                {
                    Forms.Context.StartActivity(intent);
                }
                catch (Exception)
                {
                    Toast.MakeText(Forms.Context, "No Application Available to View PDF", ToastLength.Short).Show();
                }
                //break;

#endif
                //default:
                //break;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetResources()
        {
            try
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = this.SubjectName.ToLower().Replace(' ', '.').Replace('\'','.')
                };

                ListObjectsV2Response response;
                do
                {
                    response = await client.ListObjectsV2Async(request);

                    // Process response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        this.DocumentsList.Add(new Document(entry.Key, new Random().Next(0, 299))); //sostituire il numero random con un valore nel db
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
    }
}
