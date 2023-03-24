using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using PokeAppAndroid.View;
using SharedCode;
using SharedCode.Services;
using SharedCode.Model;
using SharedCode.Util;
using System;

namespace PokeAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static LoginService loginService = new LoginService();

        private TextView tvTest;
        private Button LoginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            loginService.UserLoggedIn += LoginService_UserLoggedIn;
            tvTest = FindViewById<TextView>(Resource.Id.tvTest);
            tvTest.Text = Class1.test;

            LoginButton = FindViewById<Button>(Resource.Id.LoginButton);
            LoginButton.Click += LoginButton_Click;
        }

        private void LoginService_UserLoggedIn(object sender, Result<string> resultLogin)
        {
            if (resultLogin.Success)
            {
                StartActivity(typeof(SecondActivity));
                Finish();
            }
            else
            {
                Console.WriteLine(resultLogin.Error);
            }
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string email = "test@test.com";
            string password = "tester";

            User user = new User(email, password);

            loginService.PerformLogin(user);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
