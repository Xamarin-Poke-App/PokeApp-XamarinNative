// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS.View
{
	[Register ("PokeDetailViewController")]
	partial class PokeDetailViewController
	{
		[Outlet]
		UIKit.UIImageView pokemonImageView { get; set; }

		[Outlet]
		UIKit.UILabel pokemonNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pokemonImageView != null) {
				pokemonImageView.Dispose ();
				pokemonImageView = null;
			}

			if (pokemonNameLabel != null) {
				pokemonNameLabel.Dispose ();
				pokemonNameLabel = null;
			}
		}
	}
}
