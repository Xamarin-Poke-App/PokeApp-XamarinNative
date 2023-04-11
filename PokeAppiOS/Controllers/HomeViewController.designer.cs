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
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UICollectionView PokemonCollectionView { get; set; }

		[Outlet]
		UIKit.UISearchBar pokemonSearchBar { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView progressIndicator { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PokemonCollectionView != null) {
				PokemonCollectionView.Dispose ();
				PokemonCollectionView = null;
			}

			if (pokemonSearchBar != null) {
				pokemonSearchBar.Dispose ();
				pokemonSearchBar = null;
			}

			if (progressIndicator != null) {
				progressIndicator.Dispose ();
				progressIndicator = null;
			}
		}
	}
}
