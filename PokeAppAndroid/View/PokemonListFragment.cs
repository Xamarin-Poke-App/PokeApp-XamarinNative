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
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Interfaces;

namespace PokeAppAndroid.View
{
    public class PokemonListFragment : AndroidX.Fragment.App.Fragment, IPokemonControllerListener
    {
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private List<PokemonLocal> pokemonList;
        private PokemonAdapter adapter;
        private IPokemonController controller;
        private StorageUtils storageUtils = IocContainer.GetDependency<StorageUtils>();
        private AndroidX.AppCompat.App.AlertDialog progressDialog;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_list, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.rv_pokemonList);
            mLayoutManager = new GridLayoutManager(view.Context, 2);
            AndroidX.AppCompat.App.AlertDialog.Builder dialogBuilder = new AndroidX.AppCompat.App.AlertDialog.Builder(view.Context);
            dialogBuilder.SetView(Resource.Layout.progress_bar);
            progressDialog = dialogBuilder.Create();
            progressDialog.Show();

            controller = IocContainer.GetDependency<IPokemonController>();
            controller.listener = this;
            controller.GetAllPokemonsSpecies();

            TextInputEditText searchInput = view.FindViewById<TextInputEditText>(Resource.Id.edt_searchPokemon);
            searchInput.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                string query = e.Text.ToString();
                controller.FilterPokemonListByName(query);
            };

            this.HasOptionsMenu = true;

            return view;
        }

        [Obsolete]
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_app, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        [Obsolete]
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Exit:
                    AppCompatActivity appCompatActivity = Platform.CurrentActivity as AppCompatActivity;
                    LoginFragment loginFragment = new LoginFragment();

                    var fragmentTransaction = appCompatActivity?.SupportFragmentManager.BeginTransaction();
                    fragmentTransaction.Hide(this);
                    fragmentTransaction.Add(Resource.Id.fragmentContainer, loginFragment, "LoginFragment");
                    fragmentTransaction.Commit();

                    storageUtils.SetIsLoggedIn(false);
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(mLayoutManager);
            adapter = new PokemonAdapter(pokemonList);
            adapter.ItemClick += GoToDetailItemClick;
            recyclerView.SetAdapter(adapter);
            progressDialog.Dismiss();
        }

        public void updateView(Result<List<PokemonLocal>> data)
        {
            if (data.Success)
            {
                pokemonList = new List<PokemonLocal>();
                foreach (var item in data.Value)
                {
                    pokemonList.Add(item);
                }
                SetupRecyclerView();
            }
            else
            {
                pokemonList = new List<PokemonLocal>();
            }
        }

        private void GoToDetailItemClick(object sender, int e)
        {
            PokemonLocal selectedPokemon = pokemonList[e];
            PokemonDetailFragment pokemonDetailFragment = new PokemonDetailFragment();
            Bundle args = new Bundle();
            args.PutInt(Constants.PokemonIdArg, selectedPokemon.Id);
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

