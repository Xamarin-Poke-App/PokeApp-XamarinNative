using System;

using Foundation;
using UIKit;
using SharedCode.Model.Api;
using System.Collections.Generic;
using CoreAudioKit;
using SharedCode.Controller;
using SharedCode.Services;
using SharedCode.Model.DB;
using SharedCode.Util;

namespace PokeAppiOS.Views.PokemonDetail.Cells
{
	public partial class EvolutionVariantTableViewCell : UITableViewCell
    {
		public static readonly NSString Key = new NSString ("EvolutionVariantTableViewCell");
		public static readonly UINib Nib;

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
                fromLabel.Text = value[0].Name;
                toLabel.Text = value[1].Name;
                loadImages(value);
            }
		}

        private void loadImages(List<ResultItem> evolutionChainPairList)
        {
            loadEvolutionFromImage(evolutionChainPairList[0]);
            loadEvolutionToImage(evolutionChainPairList[1]);
        }

        private async void loadEvolutionFromImage(ResultItem specieFrom)
        {
            var fromImageResponse = await controller.LoadPokemonImageAsync(specieFrom.GetIdFromUrl());
            if (fromImageResponse.IsFailure) return;
            fromImageView.Image = UIImage.LoadFromData(NSData.FromArray(fromImageResponse.Value));
        }

        private async void loadEvolutionToImage(ResultItem specieTo) {
            var toImageResponse = await controller.LoadPokemonImageAsync(specieTo.GetIdFromUrl());
            if (toImageResponse.IsFailure) return;
            toImageView.Image = UIImage.LoadFromData(NSData.FromArray(toImageResponse.Value));
        }
    }
}
