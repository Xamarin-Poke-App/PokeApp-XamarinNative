using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using PokeAppAndroid.View;
using SharedCode;
using SharedCode.Controller;
using SharedCode.Services;
using SharedCode.Model;
using SharedCode.Util;
using SharedCode.Interfaces;
using PokeAppAndroid.Utils;
using SharedCode.Event;

namespace PokeAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        LoginEvent loginService = IocContainer.GetDependency<LoginEvent>();
        StorageUtils storageUtils = IocContainer.GetDependency<StorageUtils>();

        private Button LoginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            bool isLoggedIn = storageUtils.GetIsLoggedIn();
            if(isLoggedIn)
            {
                StartActivity(typeof(PokemonHomeActivity));
                Finish();
                return;
            }

            loginService.UserLoggedIn += LoginService_UserLoggedIn;
            
            LoginButton = FindViewById<Button>(Resource.Id.loginButton);
            LoginButton.Click += LoginButton_Click;
        }

        private void LoginService_UserLoggedIn(object sender, Result<string> resultLogin)
        {
            if (resultLogin.Success)
            {
                StartActivity(typeof(PokemonHomeActivity));
                storageUtils.SetIsLoggedIn(true);
                Finish();
            }
            else
            {
                Toast.MakeText(Application.Context, resultLogin.Error, ToastLength.Short).Show();
            }
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string email = FindViewById<TextView>(Resource.Id.usernameEditText).Text;
            string password = FindViewById<TextView>(Resource.Id.passwordEditText).Text;
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
