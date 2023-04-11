// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS.Controllers
{
	[Register ("PokemonDetailViewController")]
	partial class PokemonDetailViewController
	{
		[Outlet]
		UIKit.UIView containerView { get; set; }

		[Outlet]
		UIKit.UIImageView pokemonImageView { get; set; }

		[Outlet]
		UIKit.UILabel pokemonNameLabel { get; set; }

		[Outlet]
		UIKit.UIStackView pokemonTypesStackView { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progressIndicator { get; set; }

		[Outlet]
		UIKit.UISegmentedControl statsSegmentedControl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (containerView != null) {
				containerView.Dispose ();
				containerView = null;
			}

			if (pokemonImageView != null) {
				pokemonImageView.Dispose ();
				pokemonImageView = null;
			}

			if (pokemonNameLabel != null) {
				pokemonNameLabel.Dispose ();
				pokemonNameLabel = null;
			}

			if (pokemonTypesStackView != null) {
				pokemonTypesStackView.Dispose ();
				pokemonTypesStackView = null;
			}

			if (statsSegmentedControl != null) {
				statsSegmentedControl.Dispose ();
				statsSegmentedControl = null;
			}

			if (progressIndicator != null) {
				progressIndicator.Dispose ();
				progressIndicator = null;
			}
		}
	}
}
