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
		UIKit.UILabel firstEvolutionLabel { get; set; }

		[Outlet]
		UIKit.UILabel secondEvolutionLabel { get; set; }

		[Outlet]
		UIKit.UILabel thirdEvolutionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (firstEvolutionLabel != null) {
				firstEvolutionLabel.Dispose ();
				firstEvolutionLabel = null;
			}

			if (secondEvolutionLabel != null) {
				secondEvolutionLabel.Dispose ();
				secondEvolutionLabel = null;
			}

			if (thirdEvolutionLabel != null) {
				thirdEvolutionLabel.Dispose ();
				thirdEvolutionLabel = null;
			}
		}
	}
}
