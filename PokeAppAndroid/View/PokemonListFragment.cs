using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.RecyclerView.Widget;
using PokeAppAndroid.Adapters;
using SharedCode.Controller;
using SharedCode.Model;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using AndroidX;
using Android.App;
using Android.Widget;
using SharedCode.Util;
using Xamarin.Essentials;
using AndroidX.AppCompat.App;

namespace PokeAppAndroid.View
{
    public class PokemonListFragment : AndroidX.Fragment.App.Fragment, IPokemonController
    {
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private List<ResultPokemons> pokemonList;
        private PokemonAdapter adapter;
        private PokemonController controller;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_list, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.rv_pokemonList);
            mLayoutManager = new GridLayoutManager(view.Context, 2);

            return view;
        }

        private void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(mLayoutManager);
            adapter = new PokemonAdapter(pokemonList);
            adapter.ItemClick += GoToDetailItemClick;
            recyclerView.SetAdapter(adapter);
        }

        public void updateView(Result<List<ResultPokemons>> data)
        {
            if (data.Success)
            {
                pokemonList = new List<ResultPokemons>();
                foreach (var item in data.Value)
                {
                    pokemonList.Add(item);
                }
                SetupRecyclerView();
            }
            else
            {
                pokemonList = new List<ResultPokemons>();
            }
        }

        private void GoToDetailItemClick(object sender, int e)
        {
            PokemonDetailFragment pokemonDetailFragment = new PokemonDetailFragment();
            var appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
            var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Hide(this);
            fragmentTransaction.Add(Resource.Id.fragmentContainer, pokemonDetailFragment, "PokemonDetailFragment");
            fragmentTransaction.AddToBackStack("PokemonDetailFragment");
            fragmentTransaction.Commit();
        }
    }
}

