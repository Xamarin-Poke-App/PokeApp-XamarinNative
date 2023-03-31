using System;
using CoreGraphics;
using PokeAppiOS.View;
using System.Collections.Generic;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS.Controllers
{
    public partial class LoginViewController : UIViewController
    {
        static LoginService loginService = IocContainer.GetDependency<LoginService>();

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        private UIImage titleImage;
        private UIImageView titleImageView;
        private UIImage pikachuImage;
        private UIImageView pikachuImageView;
        private UILabel usernameLabel;
        private UILabel passwordLabel;
        private UITextField passwordTextField;
        private UIButton logButton;
        private UITextField usernameTextField;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitializeViews();
            AddViewsToSuperview();
            SetupConstraints();
        }

        private void InitializeViews()
        {
            // Background
            UIColor color1 = UIColor.FromRGB(255, 160, 160);
            UIColor color2 = UIColor.White;
            UIColor color3 = UIColor.FromRGB(116, 255, 255);

            View.SetGradientBackground(color1, color2, color3);

            titleImage = UIImage.FromFile("title.png");
            titleImageView = new UIImageView(titleImage)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(titleImageView);

            pikachuImage = UIImage.FromFile("pikachu.png");
            pikachuImageView = new UIImageView(pikachuImage)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.AddSubview(pikachuImageView);

            NSLayoutConstraint.ActivateConstraints(new[] {

                titleImageView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                titleImageView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 230),
                titleImageView.WidthAnchor.ConstraintEqualTo(306),
                titleImageView.HeightAnchor.ConstraintEqualTo(108),

                pikachuImageView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                pikachuImageView.TopAnchor.ConstraintEqualTo(titleImageView.BottomAnchor, 22),
                pikachuImageView.WidthAnchor.ConstraintEqualTo(211),
                pikachuImageView.HeightAnchor.ConstraintEqualTo(214),
            });

            // Content: Label, TextField, Button
            usernameLabel = new UILabel
            {
                Text = "Username:",
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.BoldSystemFontOfSize(20),
                TextColor = UIColor.DarkGray
            };

            passwordLabel = new UILabel
            {
                Text = "Password:",
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.BoldSystemFontOfSize(20),
                TextColor = UIColor.DarkGray
            };

            var bottomLine = new UIView
            {
                BackgroundColor = UIColor.DarkGray,
            };

            logButton = new UIButton(UIButtonType.System);
            logButton.SetTitle("LOGIN", UIControlState.Normal);
            logButton.Font = UIFont.BoldSystemFontOfSize(18);
            logButton.Layer.BorderColor = UIColor.DarkGray.CGColor;
            logButton.SetTitleColor(UIColor.DarkGray, UIControlState.Normal);
            logButton.Layer.BorderWidth = 1.0f;
            logButton.Layer.CornerRadius = 10.0f;
            logButton.TouchUpInside += LoginButton_TouchUpInside;

            usernameTextField = new TextFieldWithBottomLine("Insert you email");
            usernameTextField.TranslatesAutoresizingMaskIntoConstraints = false;

            passwordTextField = new TextFieldWithBottomLine("Insert you password");
            passwordTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            passwordTextField.SecureTextEntry = true;
        }

        private void AddViewsToSuperview()
        {
            // AddViewsToSuperview
            View.AddSubview(titleImageView);
            View.AddSubview(pikachuImageView);
            View.AddSubview(usernameLabel);
            View.AddSubview(passwordLabel);
            View.AddSubview(logButton);
            View.AddSubview(usernameTextField);
            View.AddSubview(passwordTextField);
        }

        private void SetupConstraints()
        {
            usernameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            passwordLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            logButton.TranslatesAutoresizingMaskIntoConstraints = false;

            var constraints = new List<NSLayoutConstraint>
                {
                    titleImageView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                    titleImageView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 200),
                    titleImageView.WidthAnchor.ConstraintEqualTo(306),
                    titleImageView.HeightAnchor.ConstraintEqualTo(108),

                    pikachuImageView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                    pikachuImageView.TopAnchor.ConstraintEqualTo(titleImageView.BottomAnchor, 22),
                    pikachuImageView.WidthAnchor.ConstraintEqualTo(211),
                    pikachuImageView.HeightAnchor.ConstraintEqualTo(214),

                    usernameLabel.TopAnchor.ConstraintEqualTo(pikachuImageView.SafeAreaLayoutGuide.TopAnchor, 250),
                    usernameLabel.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 50),

                    usernameTextField.TopAnchor.ConstraintEqualTo(usernameLabel.TopAnchor),
                    usernameTextField.LeftAnchor.ConstraintEqualTo(usernameLabel.RightAnchor, 8),
                    usernameTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -50),
                    usernameTextField.BottomAnchor.ConstraintEqualTo(usernameLabel.BottomAnchor),

                    passwordLabel.TopAnchor.ConstraintEqualTo(usernameLabel.BottomAnchor, 40),
                    passwordLabel.LeftAnchor.ConstraintEqualTo(usernameLabel.LeftAnchor),
                    passwordLabel.WidthAnchor.ConstraintEqualTo(100),

                    passwordTextField.TopAnchor.ConstraintEqualTo(passwordLabel.TopAnchor),
                    passwordTextField.LeftAnchor.ConstraintEqualTo(passwordLabel.RightAnchor, 8),
                    passwordTextField.RightAnchor.ConstraintEqualTo(View.RightAnchor, -50),
                    passwordTextField.BottomAnchor.ConstraintEqualTo(passwordLabel.BottomAnchor),

                    logButton.TopAnchor.ConstraintEqualTo(passwordLabel.BottomAnchor, 50),
                    logButton.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                    logButton.WidthAnchor.ConstraintEqualTo(100),
                    logButton.HeightAnchor.ConstraintEqualTo(50),
                };


            NSLayoutConstraint.ActivateConstraints(constraints.ToArray());

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

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
