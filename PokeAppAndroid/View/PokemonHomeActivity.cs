
using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using AndroidX.RecyclerView.Widget;
using PokeAppAndroid.Adapters;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    [Activity(Label = "PokemonHomeActivity")]
    public class PokemonHomeActivity : Activity, IPokemonController
    {
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private List<ResultPokemons> pokemonList;
        private PokemonAdapter adapter;
        private PokemonController controller;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pokemon_home);

            controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
        }

        private void SetupRecyclerView()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.rv_pokemonList);
            mLayoutManager = new GridLayoutManager(this, 2);
            recyclerView.SetLayoutManager(mLayoutManager);
            adapter = new PokemonAdapter(pokemonList);
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
            else {
                pokemonList = new List<ResultPokemons>();
            }
        }
    }
}

