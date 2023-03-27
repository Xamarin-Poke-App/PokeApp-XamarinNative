using System;

using UIKit;

namespace PokeAppiOS
{
	public partial class SecondViewController : UIViewController
	{
		public SecondViewController () : base ("SecondViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			Title = "Second View";
            LogoutButton.TouchUpInside += LogoutButton_TouchUpInside;
		}

        private void LogoutButton_TouchUpInside(object sender, EventArgs e)
        {
			SceneDelegate.Current.Logout();
        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


