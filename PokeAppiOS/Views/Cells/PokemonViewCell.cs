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
            }
        }

        public void CellNewImageSize(double height, double width)
        {
            // change the size of the pokemon image and background image depends of the screen size
            var newHeight = height * 0.7;
            var newWidth = width * 0.6;
            PokemonBackgroundImageView.Frame = new CGRect(PokemonBackgroundImageView.Frame.X, PokemonBackgroundImageView.Frame.Y, newWidth, newHeight);
            PokemonImageView.Frame = new CGRect(PokemonImageView.Frame.X, PokemonImageView.Frame.Y, newWidth, newHeight);
            PokemonBackgroundImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            PokemonImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.Success)
            {
                PokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(image.Value));
            }
        }

        public void updatePokemonInfo(Result<PokemonSpecie> pokemon)
        {
           
        }
    }
}
