using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;

namespace PokeAppAndroid.View
{
	public class PokemonInformationFragment : AndroidX.Fragment.App.Fragment
    {
        private TextView pokemonNameLabel;
        private TextView pokemonNameItem;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_information_detail, container, false);
            //pokemonNameLabel = view.FindViewById<TextView>(Resource.Id.pokemonNameLabel);
            //pokemonNameItem = view.FindViewById<TextView>(Resource.Id.pokemonNameItem);
            return view;
        }

    }
}

