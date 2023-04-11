using System;
using System.Linq;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using PokeAppAndroid.Utils;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;
using FFImageLoading;
using static Android.Renderscripts.Sampler;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace PokeAppAndroid.View
{
    public class PokemonInfoFragment : AndroidX.Fragment.App.Fragment, IPokemonDetailControllerListener
    {
        private IPokemonDetailController controller = IocContainer.GetDependency<IPokemonDetailController>();
        private PokemonLocal _pokemon;
        private Android.Graphics.Color _primaryColor;
        private int pokemonId;

        private ImageView secondaryPokemonSprite;
        private TextView pokemonGeneration;
        private TextView pokemonHabitad;
        private TextView pokemonRegion;
        private TextView pokemonBaseHappiness;
        private TextView pokemonDescription;

        private TextView pokemonGenerationLabel;
        private TextView pokemonHabitadLabel;
        private TextView pokemonRegionLabel;
        private TextView pokemonBaseHappinessLabel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            checkArgs();
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

            pokemonGenerationLabel = view.FindViewById<TextView>(Resource.Id.tvPokemonGenerationLabel);
            pokemonHabitadLabel = view.FindViewById<TextView>(Resource.Id.tvPokemonHabitadLabel);
            pokemonBaseHappinessLabel = view.FindViewById<TextView>(Resource.Id.tvPokemonBaseHappinessLabel);
            pokemonRegionLabel = view.FindViewById<TextView>(Resource.Id.tvPokemonRegionLabel);

            return view;
        }

        public void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain)
        {
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.IsFailure) return;
            _pokemon = pokemon.Value;
            var primaryType = _pokemon.TypesArray.FirstOrDefault();
            _primaryColor = ViewExtensions.GetColorForType(RequireContext(), primaryType);

            redrawInformation();
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
            pokemonId = Arguments.GetInt(Constants.PokemonIdArg);

            if (pokemonId == 0) return;
            controller.LoadPokemonInfo(pokemonId);
        }

        private void redrawInformation()
        {
            pokemonGeneration.Text = _pokemon.GetGenerationRomanNumeral();
            pokemonHabitad.Text = StringUtils.ToTitleCase(_pokemon.Habitat ?? "No Habitat");
            pokemonBaseHappiness.Text = _pokemon.BaseHappiness.ToString();
            pokemonRegion.Text = StringUtils.ToTitleCase(_pokemon.Region ?? "No Region");
            pokemonDescription.Text = _pokemon.FlavorTextEntry.Replace(System.Environment.NewLine, "");

            pokemonGenerationLabel.SetTextColor(_primaryColor);
            pokemonHabitadLabel.SetTextColor(_primaryColor);
            pokemonBaseHappinessLabel.SetTextColor(_primaryColor);
            pokemonRegionLabel.SetTextColor(_primaryColor);
            ImageLoaderService.LoadImageFromUrl(_pokemon.ShinySprite)
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(secondaryPokemonSprite);
        }
    }
}

