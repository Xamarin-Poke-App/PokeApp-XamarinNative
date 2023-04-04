using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;

namespace PokeAppAndroid.View
{
    public class PokemonEvolutionFragment : AndroidX.Fragment.App.Fragment
    {

        private TextView firstEvolution;
        private TextView secondEvolution;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_evolution_detail, container, false);
            //firstEvolution = view.FindViewById<TextView>(Resource.Id.firstEvolution);
            //secondEvolution = view.FindViewById<TextView>(Resource.Id.secondEvolution);
            return view;
        }

    }
}

