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
	[Register ("PokemonEvolutionViewController")]
	partial class PokemonEvolutionViewController
	{
		[Outlet]
		UIKit.UILabel noEvolutionChainLabel { get; set; }

		[Outlet]
		UIKit.UITableView pokemonEvolutionChainTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (pokemonEvolutionChainTableView != null) {
				pokemonEvolutionChainTableView.Dispose ();
				pokemonEvolutionChainTableView = null;
			}

			if (noEvolutionChainLabel != null) {
				noEvolutionChainLabel.Dispose ();
				noEvolutionChainLabel = null;
			}
		}
	}
}
