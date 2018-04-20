using System;
using System.Collections.Generic;
using System.Text;
#if __IOS__
using UIKit;
#endif

namespace Youni
{
    public class UIDocumentInteractionControllerDelegateClass
#if __IOS__
        : UIDocumentInteractionControllerDelegate
#endif
    {
#if __IOS__
        UIViewController ownerVC;

        public UIDocumentInteractionControllerDelegateClass(UIViewController vc)
        {
            ownerVC = vc;
        }

        public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return ownerVC;
        }

        public override UIView ViewForPreview(UIDocumentInteractionController controller)
        {
            return ownerVC.View;
        }
#endif
    }
}