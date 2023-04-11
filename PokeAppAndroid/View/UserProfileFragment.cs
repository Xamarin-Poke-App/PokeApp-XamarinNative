
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using SharedCode.Services;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    public class UserProfileFragment : AndroidX.Fragment.App.Fragment
    {
        private StorageUtils storageUtils = IocContainer.GetDependency<StorageUtils>();
        private Button LogoutButton;
        private TextView UserNameTextView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_profile_user, container, false);

            LogoutButton = view.FindViewById<Button>(Resource.Id.btnLogout);
            UserNameTextView = view.FindViewById<TextView>(Resource.Id.tvUserName);

            LogoutButton.Click += LogoutButton_Click;

            return view;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
            LoginFragment loginFragment = new LoginFragment();

            var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Hide(this);
            fragmentTransaction.Add(Resource.Id.fragmentContainer, loginFragment, "LoginFragment");
            fragmentTransaction.Commit();

            storageUtils.SetIsLoggedIn(false);
        }
    }
}

