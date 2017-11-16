using System;
using System.Collections.Generic;
using System.Linq;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

using Foundation;
using UIKit;

namespace Youni.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // La tastiera non si sovrapporrà al contenuto
            KeyboardOverlapRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
