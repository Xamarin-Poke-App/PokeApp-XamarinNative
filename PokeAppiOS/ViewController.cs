using CoreGraphics;
using Foundation;
using SharedCode;
using System;
using UIKit;

namespace PokeAppiOS
{
    public partial class ViewController : UIViewController
    {
        private UILabel resultLabel;
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            resultLabel = new UILabel()
            {
                Frame = new CGRect(20, 124, View.Bounds.Width - 40, 40),
                TextColor = UIColor.Blue,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(24),
                Text = Class1.test,
            };

            View.AddSubviews(resultLabel);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
