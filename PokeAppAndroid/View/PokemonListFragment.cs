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
using SharedCode.Services;
using Google.Android.Material.TextField;

namespace PokeAppAndroid.View
{
    public class PokemonListFragment : AndroidX.Fragment.App.Fragment, IPokemonControllerListener
    {
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private List<PokemonFixed> pokemonList;
        private PokemonAdapter adapter;
        private IPokemonController controller;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            controller = IocContainer.GetDependency<IPokemonController>();
            controller.listener = this;
            controller.GetAllPokemonsSpecies();
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_list, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.rv_pokemonList);
            mLayoutManager = new GridLayoutManager(view.Context, 2);

            TextInputEditText searchInput = view.FindViewById<TextInputEditText>(Resource.Id.edt_searchPokemon);
            searchInput.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                string query = e.Text.ToString();
                controller.FilterPokemonListByName(query);
            };

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
                pokemonList = new List<PokemonFixed>();
                foreach (var item in data.Value)
                {
                    pokemonList.Add(item.ToPokemonFixed());
                }
                SetupRecyclerView();
            }
            else
            {
                pokemonList = new List<PokemonFixed>();
            }
        }

        private void GoToDetailItemClick(object sender, int e)
        {
            PokemonFixed selectedPokemon = pokemonList[e];
            PokemonDetailFragment pokemonDetailFragment = new PokemonDetailFragment();
            Bundle args = new Bundle();
            args.PutInt("pokemonId", int.Parse(selectedPokemon.ID));
            pokemonDetailFragment.Arguments = args;

            var appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
            var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Hide(this);
            fragmentTransaction.Add(Resource.Id.fragmentContainer, pokemonDetailFragment, "PokemonDetailFragment");
            fragmentTransaction.AddToBackStack("PokemonDetailFragment");
            fragmentTransaction.Commit();
        }
    }
}

