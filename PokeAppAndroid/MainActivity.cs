using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using PokeAppAndroid.View;
using SharedCode;
using SharedCode.Controller;
using SharedCode.Model;

namespace PokeAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView tvTest;
        private Button LoginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            tvTest = FindViewById<TextView>(Resource.Id.tvTest);
            tvTest.Text = Class1.test;

            LoginButton = FindViewById<Button>(Resource.Id.LoginButton);
            LoginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string email = "test@test.com";
            string password = "tester";

            User user = new User(email, password);

            var resultLogin = LoginController.DoLogin(user);

            if (resultLogin.Success)
            {
                StartActivity(typeof(SecondActivity));
                Finish();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
