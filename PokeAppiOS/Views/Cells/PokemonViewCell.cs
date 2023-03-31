using System;
using CoreAnimation;
using System.Drawing;
using CoreGraphics;
using Foundation;
using SharedCode.Model;
using UIKit;

namespace PokeAppiOS.Views.Cells
{
  
    public partial class PokemonViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("PokemonViewCell");
		public static readonly UINib Nib = UINib.FromName("PokemonViewCell", NSBundle.MainBundle);

       
        protected PokemonViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public ResultPokemons Pokemon
		{
			set
			{
                PokemonNameLabel.Text = value.name;

            }
        }

        public void CellNewImageSize(double height, double width)
        {
            var newHeight = height * 0.7;
            var newWidth = width * 0.6;
            PokemonBackgroundImageView.Frame = new CGRect(PokemonBackgroundImageView.Frame.X, PokemonBackgroundImageView.Frame.Y, newWidth, newHeight);
            PokemonImageView.Frame = new CGRect(PokemonImageView.Frame.X, PokemonImageView.Frame.Y, newWidth, newHeight);
            PokemonBackgroundImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            PokemonImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
        }
    }
}
