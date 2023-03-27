using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SharedCode;
using SharedCode.Controller;

namespace PokeAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IPokemonController
    {
        private TextView tvTest;
        private PokemonController controller;

        public void updateView(int? data, string errorMsg)
        {
            if (errorMsg == null)
            {
                tvTest.Text = data.ToString();
            }
            else
            {
                tvTest.Text = errorMsg.ToString();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            tvTest = FindViewById<TextView>(Resource.Id.tvTest);
            tvTest.Text = Class1.test;
            controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
        }
    }
}
