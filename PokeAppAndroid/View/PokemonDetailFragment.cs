﻿using System;
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
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    public class PokemonDetailFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private int pokemonId;
        private PokemonSpecie pokemonInfo;
        private ImageView pokemonImage;
        private TextView pokemonNameText;
        private Button informationButton;
        private Button evolutionButton;
        private IPokemonDetailController controller;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            checkArgs();

            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
            controller.LoadPokemonInfo(pokemonId);
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_detail, container, false);
            pokemonImage = view.FindViewById<ImageView>(Resource.Id.pokemonImageView);
            pokemonNameText = view.FindViewById<TextView>(Resource.Id.pokemonNameTextView);
            informationButton = view.FindViewById<Button>(Resource.Id.informationButton);
            evolutionButton = view.FindViewById<Button>(Resource.Id.evolutionButton);

            informationButton.Click += InformationButton_Click;
            evolutionButton.Click += EvolutionButton_Click;

            SetButtonStyle(informationButton, true);
            SetButtonStyle(evolutionButton, false);

            ReplaceFragment(new PokemonInfoFragment());

            return view;
        }

        private void SetButtonStyle(Button button, bool isActive)
        {
            if (isActive)
            {
                button.SetBackgroundColor(Android.Graphics.Color.LightGray);
                button.SetTextColor(Android.Graphics.Color.White);
            }
            else
            {
                button.SetBackgroundColor(Android.Graphics.Color.White);
                button.SetTextColor(Android.Graphics.Color.LightGray);
            }
        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            SetButtonStyle(informationButton, true);
            SetButtonStyle(evolutionButton, false);

            PokemonInfoFragment pokemonInfoFragment = new PokemonInfoFragment();
            Bundle args = new Bundle();
            pokemonInfoFragment.Arguments = args;

            ReplaceFragment(pokemonInfoFragment);
        }

        private void EvolutionButton_Click(object sender, EventArgs e)
        {
            evolutionButton = (Button)sender;
            SetButtonStyle(informationButton, false);
            SetButtonStyle(evolutionButton, true);
            ReplaceFragment(new PokemonEvolutionFragment());
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.IsFailure) return;
            byte[] imageByte = image.Value;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageByte, 0, imageByte.Length);
            pokemonImage.SetImageBitmap(bitmap);
        }


        private void ReplaceFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            AndroidX.Fragment.App.FragmentTransaction transaction = ChildFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.fragmentBaseDetail, fragment);
            transaction.Commit();
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.IsFailure) return;
            pokemonNameText.Text = pokemon.Value.Name;
            controller.LoadPokemonImage(pokemon.Value.Id);
        }

        private void checkArgs()
        {
            if (Arguments == null) return;
            pokemonId = Arguments.GetInt("pokemonId");
        }
    }
}