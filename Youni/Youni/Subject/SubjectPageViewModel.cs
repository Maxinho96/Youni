using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using Android.Content;
using Android.Widget;
using UIKit;
using Foundation;

namespace Youni
{
    public class SubjectPageViewModel : BindableObject
    {
        public INavigation Navigation;
        public Command NotifyTapped { get; set; }
        public Command FavouritesTapped { get; set; }
        public Command SearchCommand { get; set; }
        public Command DocumentTapped { get; set; }
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
            this.DocumentTapped = new Command(async (docTitle) =>
            {
                await this.GetDocument((string)docTitle);
            });
        }

        public async Task GetDocument(string documentTitle)
        {
            
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = this.SubjectName.ToLower().Replace(' ', '.'),
                Key = documentTitle
            };

            string filePath;

            using (GetObjectResponse response = await client.GetObjectAsync(request))
            {   
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"..", "Personal", documentTitle);
                        break;
                    case Device.Android:
                        filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,"Youni", documentTitle);
                        break;
                    default:
                        filePath = "piattaforma non supportata";
                        break;
                }

                //scrivo su file
                await response.WriteResponseStreamToFileAsync(filePath,false);
            }

            try
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:

                        var PreviewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filePath));
                        PreviewController.Delegate = new UIDocumentInteractionControllerDelegateClass(UIApplication.SharedApplication.KeyWindow.RootViewController);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            PreviewController.PresentPreview(true);
                        });

                        break;

                    case Device.Android:

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
                        break;

                    default:
                        break;
                }
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
                    BucketName = this.SubjectName.ToLower().Replace(' ', '.')
                };

                ListObjectsV2Response response;
                do
                {
                    response = await client.ListObjectsV2Async(request);

                    // Process response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        this.DocumentsList.Add(new Document(entry.Key,new Random().Next(0,299))); //sostituire il numero random con un valore nel db
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
