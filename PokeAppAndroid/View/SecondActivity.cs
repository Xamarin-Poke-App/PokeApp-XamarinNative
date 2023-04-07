
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
using SharedCode;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : AppCompatActivity, IPokemonControllerListener
    {
        private Button LogoutButton;
        private IPokemonController controller;
        private TextView tvTest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_second);

            LogoutButton = FindViewById<Button>(Resource.Id.LogoutButton);
            LogoutButton.Click += LogoutButton_Click;
            tvTest = FindViewById<TextView>(Resource.Id.tvTextView);
            controller = IocContainer.GetDependency<IPokemonController>();
            controller.listener = this;
            controller.GetAllPokemonsSpecies();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        public void updateView(Result<List<PokemonLocal>> data)
        {
            if (data.Success)
            {
                tvTest.Text = data.Value.Count.ToString();
            }
            else
            {
                tvTest.Text = data.Error;
            }
        }
    }
}

