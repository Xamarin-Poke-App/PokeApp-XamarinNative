using CoreGraphics;
using Foundation;
using SharedCode;
using SharedCode.Controller;
using SharedCode.Model;
using System;
using UIKit;

namespace PokeAppiOS
{
    public partial class ViewController : UIViewController
    {

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            Title = "Login View";

            LoginButton.TouchUpInside += LoginButton_TouchUpInside;
        }

        private void LoginButton_TouchUpInside(object sender, EventArgs e)
        {
            string email = "test@test.com";
            string password = "tester";

            User user = new User(email, password);
            var resultLogin = LoginController.DoLogin(user);

            if (resultLogin.Success)
            {
                SceneDelegate.Current.SegueToHome();

            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
