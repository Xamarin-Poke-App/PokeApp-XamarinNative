using System;

using Foundation;
using UIKit;
using SharedCode.Model.Api;
using System.Collections.Generic;
using System.Linq;
using CoreAudioKit;
using SharedCode.Controller;
using SharedCode.Services;
using SharedCode.Model.DB;
using SharedCode.Util;
using FFImageLoading;

namespace PokeAppiOS.Views.PokemonDetail.Cells
{
	public partial class EvolutionVariantTableViewCell : UITableViewCell
    {
		public static readonly NSString Key = new NSString ("EvolutionVariantTableViewCell");
		public static readonly UINib Nib;

        public UIColor PrimaryColor;

        IPokemonDetailController controller = IocContainer.GetDependency<IPokemonDetailController>();

        static EvolutionVariantTableViewCell ()
		{
			Nib = UINib.FromName ("EvolutionVariantTableViewCell", NSBundle.MainBundle);
		}

		protected EvolutionVariantTableViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public List<ResultItem> EvolutionChainPairList
		{
			set
			{
                fromLabel.Text = StringUtils.ToTitleCase(value.FirstOrDefault().Name);
                toLabel.Text = StringUtils.ToTitleCase(value.LastOrDefault().Name);
                loadImages(value);
                if (PrimaryColor != null)
                {
                    fromLabel.TextColor = PrimaryColor;
                    toLabel.TextColor = PrimaryColor;
                    arrowImageView.TintColor = PrimaryColor;
                }
            }
		}

        private void loadImages(List<ResultItem> evolutionChainPairList)
        {
            loadEvolutionFromImage(evolutionChainPairList.FirstOrDefault());
            loadEvolutionToImage(evolutionChainPairList.LastOrDefault());
        }

        private void loadEvolutionFromImage(ResultItem specieFrom)
        {
            string pokemonFromId = specieFrom.GetIdFromUrl().ToString();
            ImageLoaderService.LoadImageFromUrl(specieFrom.GetPokemonImageURL(pokemonFromId))
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(fromImageView);
        }

        private void loadEvolutionToImage(ResultItem specieTo) {
            string pokemonToId = specieTo.GetIdFromUrl().ToString();
            ImageLoaderService.LoadImageFromUrl(specieTo.GetPokemonImageURL(pokemonToId))
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(toImageView);
        }
    }
}
