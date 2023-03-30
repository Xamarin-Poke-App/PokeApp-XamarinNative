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
		UIKit.UILabel PokemonNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PokemonNameLabel != null) {
				PokemonNameLabel.Dispose ();
				PokemonNameLabel = null;
			}
		}
	}
}
