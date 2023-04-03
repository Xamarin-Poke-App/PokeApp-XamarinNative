using System;
using CoreAnimation;
using System.Drawing;
using CoreGraphics;
using Foundation;
using SharedCode.Model;
using UIKit;
using SharedCode.Controller;
using SharedCode.Util;
using SharedCode.Services;

namespace PokeAppiOS.Views.Cells
{
  
    public partial class PokemonViewCell : UICollectionViewCell, IPokemonDetailControllerListener
	{
		public static readonly NSString Key = new NSString ("PokemonViewCell");
		public static readonly UINib Nib = UINib.FromName("PokemonViewCell", NSBundle.MainBundle);
        private IPokemonDetailController controller;


        protected PokemonViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public ResultPokemons Pokemon
		{
			set
			{
                PokemonNameLabel.Text = value.FormatedName();
                PokemonNumberLabel.Text = "#" + value.GetIdFromUrl().ToString();
                controller = IocContainer.GetDependency<IPokemonDetailController>();
                controller.listener = this;
                controller.LoadPokemonImage(value.GetIdFromUrl());
                controller.GetPokemonInfo(value.GetIdFromUrl());
            }
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.Success)
            {
                PokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(image.Value));
            }
        }

        public void updatePokemonInfo(Result<PokemonInfo> pokemon)
        {
            if (pokemon.Success)
            {
                //Will only use the first type for background color
                PokemonViewBackground.BackgroundColor = UIColor.FromName(pokemon.Value.types[0].type.name).ColorWithAlpha(0.8f);

                // This is temporary until be repleaced for another component
                if (pokemon.Value.types.Count == 1)
                {
                    PokemonFirstTypeImageView.Image = UIImage.FromBundle(pokemon.Value.types[0].type.name + "Type");
                }
                else if (pokemon.Value.types.Count > 1)
                {
                    PokemonFirstTypeImageView.Image = UIImage.FromBundle(pokemon.Value.types[0].type.name + "Type");
                    PokemonSecondTypeImageView.Image = UIImage.FromBundle(pokemon.Value.types[1].type.name + "Type");
                }
            }
        }

        public void updatePokemonSpecieInfo(Result<PokemonSpecie> pokemon)
        {
            // Nothing to implement
        }
    }
}
