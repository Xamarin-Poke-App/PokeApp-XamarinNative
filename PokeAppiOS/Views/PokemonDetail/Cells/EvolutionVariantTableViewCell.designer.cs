// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS.Views.PokemonDetail.Cells
{
	[Register ("EvolutionVariantTableViewCell")]
	partial class EvolutionVariantTableViewCell
	{
		[Outlet]
		UIKit.UIImageView arrowImageView { get; set; }

		[Outlet]
		UIKit.UIImageView fromImageView { get; set; }

		[Outlet]
		UIKit.UILabel fromLabel { get; set; }

		[Outlet]
		UIKit.UIImageView toImageView { get; set; }

		[Outlet]
		UIKit.UILabel toLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (fromImageView != null) {
				fromImageView.Dispose ();
				fromImageView = null;
			}

			if (fromLabel != null) {
				fromLabel.Dispose ();
				fromLabel = null;
			}

			if (toImageView != null) {
				toImageView.Dispose ();
				toImageView = null;
			}

			if (toLabel != null) {
				toLabel.Dispose ();
				toLabel = null;
			}

			if (arrowImageView != null) {
				arrowImageView.Dispose ();
				arrowImageView = null;
			}
		}
	}
}
