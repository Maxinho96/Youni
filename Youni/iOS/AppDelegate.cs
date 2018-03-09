using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using CarouselView.FormsPlugin.iOS;
using XFShapeView.iOS;

namespace Youni.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CarouselViewRenderer.Init(); // Makes CarouselView work
            EntryCustomReturn.Forms.Plugin.iOS.CustomReturnEntryRenderer.Init(); // Makes CostumReturnEntry work
            ShapeRenderer.Init(); // Makes ShapeView work

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
