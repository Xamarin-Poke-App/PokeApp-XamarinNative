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

namespace PokeAppAndroid.View
{
	public class PokemonDetailFragment : AndroidX.Fragment.App.Fragment
    {
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override Android.Views.View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_detail, container, false);
			return view;
        }
	}
}

