using CoreGraphics;
using Foundation;
using SharedCode.Services;
using SharedCode.Model;
using System;
using UIKit;
using SharedCode.Util;

namespace PokeAppiOS
{
    public partial class ViewController : UIViewController
    {
        static LoginService loginService = IocContainer.GetDependency<LoginService>();

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
            loginService.UserLoggedIn += LoginService_UserLoggedIn;
            string email = "test@test.com";
            string password = "tester";

            User user = new User(email, password);
            loginService.PerformLogin(user);
        }

        private void LoginService_UserLoggedIn(object sender, Result<string> userLoggedInResult)
        {
            if (userLoggedInResult.Success)
            {
                SceneDelegate.Current.SegueToHome();
            }
            else
            {
                Console.WriteLine(userLoggedInResult.Error);
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
