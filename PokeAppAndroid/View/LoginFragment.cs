
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using SharedCode;
using SharedCode.Controller;
using SharedCode.Services;
using SharedCode.Model;
using SharedCode.Util;
using SharedCode.Interfaces;
using PokeAppAndroid.Utils;
using SharedCode.Event;
using Xamarin.Essentials;

namespace PokeAppAndroid.View
{
	public class LoginFragment : AndroidX.Fragment.App.Fragment
    {
        LoginEvent loginService = IocContainer.GetDependency<LoginEvent>();
        StorageUtils storageUtils = IocContainer.GetDependency<StorageUtils>();
        private AppCompatActivity appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
        private PokemonListFragment pokemonListFragment = new PokemonListFragment();

        private Button LoginButton;
        private EditText Username;
        private EditText Password;

        public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            Xamarin.Essentials.Platform.Init(appCompatActivity, savedInstanceState);
        }

		public override Android.Views.View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_login, container, false);
            Username = view.FindViewById<EditText>(Resource.Id.usernameEditText);
            Password = view.FindViewById<EditText>(Resource.Id.passwordEditText);

            bool isLoggedIn = storageUtils.GetIsLoggedIn();
            if (isLoggedIn)
            {
                var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Hide(this);
                fragmentTransaction.Add(Resource.Id.fragmentContainer, pokemonListFragment, "PokemonListFragment");
                fragmentTransaction.Commit();

                return view;
            }

            loginService.UserLoggedIn += LoginService_UserLoggedIn;

            LoginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            LoginButton.Click += LoginButton_Click;

            return view;
        }

        private void LoginService_UserLoggedIn(object sender, Result<string> resultLogin)
        {
            if (resultLogin.Success)
            {
                var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Hide(this);
                fragmentTransaction.Add(Resource.Id.fragmentContainer, pokemonListFragment, "PokemonListFragment");
                fragmentTransaction.Commit();
               
                storageUtils.SetIsLoggedIn(true);
            }
            else
            {
                Toast.MakeText(Application.Context, resultLogin.Error, ToastLength.Short).Show();
            }
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string email = Username.Text;
            string password = Password.Text;
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

