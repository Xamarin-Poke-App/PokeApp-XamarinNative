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
		UIKit.UIImageView pokemonBackgroundImageView { get; set; }

		[Outlet]
		UIKit.UILabel pokemonFirstTypeLabel { get; set; }

		[Outlet]
		UIKit.UIView pokemonFirstTypeView { get; set; }

		[Outlet]
		UIKit.UIImageView pokemonImageView { get; set; }

		[Outlet]
		UIKit.UILabel pokemonNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel pokemonNumberLabel { get; set; }

		[Outlet]
		UIKit.UILabel pokemonSecondTypeLabel { get; set; }

		[Outlet]
		UIKit.UIView pokemonSecondTypeView { get; set; }

		[Outlet]
		UIKit.UIView pokemonViewBackground { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pokemonBackgroundImageView != null) {
				pokemonBackgroundImageView.Dispose ();
				pokemonBackgroundImageView = null;
			}

			if (pokemonImageView != null) {
				pokemonImageView.Dispose ();
				pokemonImageView = null;
			}

			if (pokemonNameLabel != null) {
				pokemonNameLabel.Dispose ();
				pokemonNameLabel = null;
			}

			if (pokemonNumberLabel != null) {
				pokemonNumberLabel.Dispose ();
				pokemonNumberLabel = null;
			}

			if (pokemonViewBackground != null) {
				pokemonViewBackground.Dispose ();
				pokemonViewBackground = null;
			}

			if (pokemonFirstTypeView != null) {
				pokemonFirstTypeView.Dispose ();
				pokemonFirstTypeView = null;
			}

			if (pokemonSecondTypeView != null) {
				pokemonSecondTypeView.Dispose ();
				pokemonSecondTypeView = null;
			}

			if (pokemonFirstTypeLabel != null) {
				pokemonFirstTypeLabel.Dispose ();
				pokemonFirstTypeLabel = null;
			}

			if (pokemonSecondTypeLabel != null) {
				pokemonSecondTypeLabel.Dispose ();
				pokemonSecondTypeLabel = null;
			}
		}
	}
}
