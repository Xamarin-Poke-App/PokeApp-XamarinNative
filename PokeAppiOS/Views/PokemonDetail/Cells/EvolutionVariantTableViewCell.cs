using System;

using Foundation;
using UIKit;

namespace PokeAppiOS.Views.PokemonDetail.Cells
{
	public partial class EvolutionVariantTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("EvolutionVariantTableViewCell");
		public static readonly UINib Nib;

		static EvolutionVariantTableViewCell ()
		{
			Nib = UINib.FromName ("EvolutionVariantTableViewCell", NSBundle.MainBundle);
		}

		protected EvolutionVariantTableViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public string[] EvolutionChain
		{
			set
			{
				firstEvolutionLabel.Text = value[0];
				secondEvolutionLabel.Text = value[1];
				thirdEvolutionLabel.Text = value[2];
			}
		}
	}
}
