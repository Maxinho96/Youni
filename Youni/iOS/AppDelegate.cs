using System;
using System.Collections.Generic;
using System.Linq;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

using Foundation;
using UIKit;

using CarouselView.FormsPlugin.iOS;

namespace Youni.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CarouselViewRenderer.Init(); // Fa funzionare la CarouselView

            KeyboardOverlapRenderer.Init(); // La tastiera non si sovrapporrà al contenuto

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
