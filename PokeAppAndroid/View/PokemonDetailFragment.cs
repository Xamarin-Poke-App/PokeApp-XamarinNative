using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PokeAppAndroid.Utils;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;
using FFImageLoading;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace PokeAppAndroid.View
{
    public class PokemonDetailFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private int _pokemonId;
        private Android.Graphics.Color _primaryColor;

        private Android.Views.View view;
        private ImageView pokemonImage;
        private TextView pokemonNameText;
        private Button informationButton;
        private Button evolutionButton;
        private IPokemonDetailController controller = IocContainer.GetDependency<IPokemonDetailController>();
        private AndroidX.AppCompat.App.AlertDialog progressDialog;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            controller.listener = this;
            checkArgs();
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.fragment_pokemon_detail, container, false);
            pokemonImage = view.FindViewById<ImageView>(Resource.Id.pokemonImageView);
            pokemonNameText = view.FindViewById<TextView>(Resource.Id.pokemonNameTextView);
            informationButton = view.FindViewById<Button>(Resource.Id.informationButton);
            evolutionButton = view.FindViewById<Button>(Resource.Id.evolutionButton);

            informationButton.Click += InformationButton_Click;
            evolutionButton.Click += EvolutionButton_Click;

            SetButtonStyle(informationButton, true);
            SetButtonStyle(evolutionButton, false);

            PokemonInfoFragment pokemonInfoFragment = new PokemonInfoFragment();
            Bundle args = new Bundle();
            args.PutInt(Constants.PokemonIdArg, _pokemonId);
            pokemonInfoFragment.Arguments = args;
            ReplaceFragment(pokemonInfoFragment);

            AndroidX.AppCompat.App.AlertDialog.Builder dialogBuilder = new AndroidX.AppCompat.App.AlertDialog.Builder(view.Context);
            dialogBuilder.SetView(Resource.Layout.progress_bar);
            progressDialog = dialogBuilder.Create();
            progressDialog.Show();

            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
            controller.LoadPokemonInfo(_pokemonId);

            return view;
        }

        private void SetButtonStyle(Button button, bool isActive)
        {
            if (!isActive)
            {
                button.SetBackgroundColor(Android.Graphics.Color.White);
                button.SetTextColor(Android.Graphics.Color.LightGray);
                return;
            }
            button.SetBackgroundColor(Android.Graphics.Color.LightGray);
            button.SetTextColor(Android.Graphics.Color.White);

            if (_primaryColor == null) return;
            button.SetBackgroundColor(_primaryColor);
        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            SetButtonStyle(informationButton, true);
            SetButtonStyle(evolutionButton, false);

            PokemonInfoFragment pokemonInfoFragment = new PokemonInfoFragment();
            Bundle args = new Bundle();
            args.PutInt(Constants.PokemonIdArg, _pokemonId);
            pokemonInfoFragment.Arguments = args;

            ReplaceFragment(pokemonInfoFragment);
        }

        private void EvolutionButton_Click(object sender, EventArgs e)
        {
            evolutionButton = (Button)sender;
            SetButtonStyle(informationButton, false);
            SetButtonStyle(evolutionButton, true);
            var pokemonEvolutionFragment = new PokemonEvolutionFragment();
            Bundle args = new Bundle();
            args.PutInt(Constants.PokemonIdArg, _pokemonId);
            pokemonEvolutionFragment.Arguments = args;
            ReplaceFragment(pokemonEvolutionFragment);
        }

        public void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain)
        {
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
        }

        private void ReplaceFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            AndroidX.Fragment.App.FragmentTransaction transaction = ChildFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.fragmentBaseDetail, fragment);
            transaction.Commit();
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            progressDialog.Dismiss();
            if (pokemon.IsFailure) return;
            var pokemonValue = pokemon.Value;
            var primaryType = pokemonValue.TypesArray.FirstOrDefault();
            _primaryColor = ViewExtensions.GetColorForType(RequireContext(), primaryType);

            view.SetBackgroundColor(_primaryColor);
            pokemonNameText.Text = StringUtils.ToTitleCase(pokemonValue.Name);

            ImageLoaderService.LoadImageFromUrl(pokemonValue.RegularSpriteUrl)
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(pokemonImage);

            SetButtonStyle(informationButton, true);
            SetButtonStyle(evolutionButton, false);
        }

        private void checkArgs()
        {
            if (Arguments == null) return;
            _pokemonId = Arguments.GetInt(Constants.PokemonIdArg);
            controller.LoadPokemonInfo(_pokemonId);
        }
    }
}
