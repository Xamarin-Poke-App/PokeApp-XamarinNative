
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace PokeAppAndroid.View
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : AppCompatActivity
    {
        private Button LogoutButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_second);

            // Create your application here

            LogoutButton = FindViewById<Button>(Resource.Id.LogoutButton);
            LogoutButton.Click += LogoutButton_Click;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}

