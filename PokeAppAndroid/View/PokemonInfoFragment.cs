using System;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;

namespace PokeAppAndroid.View
{
    public class PokemonInfoFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private PokemonSpecie pokemonInfo;
        private IPokemonDetailController controller;
        private int pokemonId;
        private ImageView secondaryPokemonSprite;
        private TextView pokemonGeneration;
        private TextView pokemonHapiness;
        private TextView pokemonHabitad;
        private TextView pokemonRegion;
        private TextView pokemonBaseHappiness;
        private TextView pokemonDescription;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            checkArgs();

            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
        }


        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_info_detail, container, false);
            secondaryPokemonSprite = view.FindViewById<ImageView>(Resource.Id.secondaryPokemonSprite);
            pokemonGeneration = view.FindViewById<TextView>(Resource.Id.tvPokemonGeneration);
            pokemonHabitad = view.FindViewById<TextView>(Resource.Id.tvPokemonHabitad);
            pokemonBaseHappiness = view.FindViewById<TextView>(Resource.Id.tvPokemonBaseHappiness);
            pokemonRegion = view.FindViewById<TextView>(Resource.Id.tvPokemonRegion);
            pokemonDescription = view.FindViewById<TextView>(Resource.Id.tvPokemonDescription);

            return view;
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            throw new NotImplementedException();
        }

        public void updatePokemonInfo(Result<PokemonSpecie> pokemon)
        {
            if (pokemon.IsFailure) return;
            pokemonInfo = pokemon.Value;
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            throw new NotImplementedException();
        }

        public void updateSecondaryPokemonSprite(Result<byte[]> image)
        {
            if (image.IsFailure) return;
            byte[] imageByte = image.Value;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageByte, 0, imageByte.Length);
            secondaryPokemonSprite.SetImageBitmap(bitmap);
        }

        private void checkArgs()
        {
            if (Arguments == null) return;
            pokemonId = Arguments.GetInt("pokemonId");
        }
    }
}

