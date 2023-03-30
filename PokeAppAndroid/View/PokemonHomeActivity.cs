using System;
using Android.App;
using Android.OS;
using AndroidX.Fragment.App;
using Android.Content;
using Xamarin.Essentials;
using AndroidX.AppCompat.App;

namespace PokeAppAndroid.View
{
    [Activity(Label = "PokemonHomeActivity")]
    public class PokemonHomeActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pokemon_home);

            PokemonListFragment pokemonListFragment = new PokemonListFragment();

            var appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
            var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.fragmentContainer, pokemonListFragment, "PokemonListFragment");
            fragmentTransaction.Commit();
        }
    }
}

