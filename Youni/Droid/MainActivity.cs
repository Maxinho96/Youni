using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using CarouselView.FormsPlugin.Android;
using Naxam.Controls.Platform.Droid;

namespace Youni.Droid
{
    [Activity(Label = "Youni.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait)] // Blocca il device nella posizione portrait
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CarouselViewRenderer.Init(); // Makes CarouselView work
            EntryCustomReturn.Forms.Plugin.Android.CustomReturnEntryRenderer.Init(); // Makes CustomReturnEntry work
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;

            LoadApplication(new App());
        }
    }
}
