// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS.Views
{
	[Register ("PokemonBaseInfoViewController")]
	partial class PokemonBaseInfoViewController
	{
		[Outlet]
		UIKit.UILabel descriptionLabel { get; set; }

		[Outlet]
		UIKit.UILabel generationValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel habitadValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel happinessValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel regionNameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView shinyImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (shinyImageView != null) {
				shinyImageView.Dispose ();
				shinyImageView = null;
			}

			if (happinessValueLabel != null) {
				happinessValueLabel.Dispose ();
				happinessValueLabel = null;
			}

			if (habitadValueLabel != null) {
				habitadValueLabel.Dispose ();
				habitadValueLabel = null;
			}

			if (generationValueLabel != null) {
				generationValueLabel.Dispose ();
				generationValueLabel = null;
			}

			if (regionNameLabel != null) {
				regionNameLabel.Dispose ();
				regionNameLabel = null;
			}

			if (descriptionLabel != null) {
				descriptionLabel.Dispose ();
				descriptionLabel = null;
			}
		}
	}
}
