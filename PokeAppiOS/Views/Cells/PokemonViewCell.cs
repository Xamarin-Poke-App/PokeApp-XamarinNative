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
	}
}
