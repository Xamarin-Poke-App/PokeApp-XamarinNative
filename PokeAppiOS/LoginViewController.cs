using CoreGraphics;
using Foundation;
using PokeAppiOS.View;
using System;
using System.Collections.Generic;
using UIKit;

namespace PokeAppiOS
{
    public partial class LoginViewController : UIViewController
    {
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
        private UIButton loginButton;
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

            loginButton = new UIButton(UIButtonType.System);
            loginButton.SetTitle("LOGIN", UIControlState.Normal);
            loginButton.Font = UIFont.BoldSystemFontOfSize(18);
            loginButton.Layer.BorderColor = UIColor.DarkGray.CGColor;
            loginButton.SetTitleColor(UIColor.DarkGray, UIControlState.Normal);
            loginButton.Layer.BorderWidth = 1.0f;
            loginButton.Layer.CornerRadius = 10.0f;
            loginButton.TouchUpInside += LoginButton_TouchUpInside;

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
            View.AddSubview(loginButton);
            View.AddSubview(usernameTextField);
            View.AddSubview(passwordTextField);
        }

        private void SetupConstraints()
        {
            usernameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            passwordLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            loginButton.TranslatesAutoresizingMaskIntoConstraints = false;

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

                    loginButton.TopAnchor.ConstraintEqualTo(passwordLabel.BottomAnchor, 50),
                    loginButton.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                    loginButton.WidthAnchor.ConstraintEqualTo(100),
                    loginButton.HeightAnchor.ConstraintEqualTo(50),
                };


            NSLayoutConstraint.ActivateConstraints(constraints.ToArray());

        }


        private void LoginButton_TouchUpInside(object sender, EventArgs e)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}


