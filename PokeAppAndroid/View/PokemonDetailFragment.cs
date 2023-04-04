using System;
using Android.OS;
using Android.Support.V4.App;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
using Java.Util.Zip;
using System.ComponentModel;

namespace PokeAppAndroid.View
{
	public class PokemonDetailFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private int pokemonId;
        private PokemonSpecie pokemonInfo;
        private ImageView pokemonImage;
        private TextView pokemonNameText;
        private IPokemonDetailController controller;

        public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            checkArgs();

            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
            controller.GetPokemonInfo(pokemonId);
        }

		public override Android.Views.View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_detail, container, false);
            pokemonImage = view.FindViewById<ImageView>(Resource.Id.pokemonImageView);
            pokemonNameText = view.FindViewById<TextView>(Resource.Id.pokemonNameTextView);

            Button informationButton = view.FindViewById<Button>(Resource.Id.informationButton);
            Button evolutionButton = view.FindViewById<Button>(Resource.Id.evolutionButton);

            informationButton.Click += InformationButton_Click;
            evolutionButton.Click += EvolutionButton_Click;

            ReplaceFragment(new PokemonInformationFragment());

            return view;
        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            ReplaceFragment(new PokemonInformationFragment());
        }

        private void EvolutionButton_Click(object sender, EventArgs e)
        {
            ReplaceFragment(new PokemonEvolutionFragment());
        }

        private void ReplaceFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            AndroidX.Fragment.App.FragmentTransaction transaction = ChildFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.fragmentBaseDetail, fragment);
            transaction.Commit();
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.IsFailure) return;
            byte[] imageByte = image.Value;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageByte, 0, imageByte.Length);
            pokemonImage.SetImageBitmap(bitmap);
        }

        public void updatePokemonInfo(Result<PokemonSpecie> pokemon)
        {
            if (pokemon.IsFailure) return;
            pokemonInfo = pokemon.Value;
            pokemonNameText.Text = pokemonInfo.name;
            controller.LoadPokemonImage(pokemonInfo.id);
        }

        private void checkArgs()
        {
            if (Arguments == null) return;
            pokemonId = Arguments.GetInt("pokemonId");
        }
    }
}

