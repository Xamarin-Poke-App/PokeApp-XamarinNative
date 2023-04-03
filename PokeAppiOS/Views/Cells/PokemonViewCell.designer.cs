// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS.Views.Cells
{
	[Register ("PokemonViewCell")]
	partial class PokemonViewCell
	{
		[Outlet]
		UIKit.UIImageView PokemonBackgroundImageView { get; set; }

		[Outlet]
		UIKit.UIImageView PokemonFirstTypeImageView { get; set; }

		[Outlet]
		UIKit.UIImageView PokemonImageView { get; set; }

		[Outlet]
		UIKit.UILabel PokemonNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PokemonNumberLabel { get; set; }

		[Outlet]
		UIKit.UIImageView PokemonSecondTypeImageView { get; set; }

		[Outlet]
		UIKit.UIView PokemonViewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PokemonViewBackground != null) {
				PokemonViewBackground.Dispose ();
				PokemonViewBackground = null;
			}

			if (PokemonBackgroundImageView != null) {
				PokemonBackgroundImageView.Dispose ();
				PokemonBackgroundImageView = null;
			}

			if (PokemonImageView != null) {
				PokemonImageView.Dispose ();
				PokemonImageView = null;
			}

			if (PokemonNameLabel != null) {
				PokemonNameLabel.Dispose ();
				PokemonNameLabel = null;
			}

			if (PokemonNumberLabel != null) {
				PokemonNumberLabel.Dispose ();
				PokemonNumberLabel = null;
			}

			if (PokemonFirstTypeImageView != null) {
				PokemonFirstTypeImageView.Dispose ();
				PokemonFirstTypeImageView = null;
			}

			if (PokemonSecondTypeImageView != null) {
				PokemonSecondTypeImageView.Dispose ();
				PokemonSecondTypeImageView = null;
			}
		}
	}
}
