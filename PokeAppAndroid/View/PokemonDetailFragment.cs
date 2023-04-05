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
        private PokemonLocal pokemonInfo;
        private ImageView pokemonImage;
        private TextView pokemonNameText;
        private IPokemonDetailController controller;

        public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            checkArgs();

            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
        }

		public override Android.Views.View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            Android.Views.View view = inflater.Inflate(Resource.Layout.fragment_pokemon_detail, container, false);
            pokemonImage = view.FindViewById<ImageView>(Resource.Id.pokemonImageView);
            pokemonNameText = view.FindViewById<TextView>(Resource.Id.pokemonNameTextView);

            return view;
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.IsFailure) return;
            byte[] imageByte = image.Value;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageByte, 0, imageByte.Length);
            pokemonImage.SetImageBitmap(bitmap);
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.IsFailure) return;
            pokemonInfo = pokemon.Value;
            pokemonNameText.Text = pokemonInfo.Name;
            controller.LoadPokemonImage(pokemonInfo.Id);
        }

        private void checkArgs()
        {
            if (Arguments == null) return;
            pokemonId = Arguments.GetInt("pokemonId");
        }
    }
}

