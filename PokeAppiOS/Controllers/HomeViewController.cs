using System;
using System.Collections.Generic;
using CoreAudioKit;
using CoreGraphics;
using Foundation;
using PokeAppiOS.Views.Cells;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS.Controllers
{
    public partial class HomeViewController : UIViewController, IPokemonControllerListener
    {

        public static string SegueIdentifier = "ToDetailSegue";
        private IPokemonController controller;
        private List<ResultPokemons> Pokemons = new List<ResultPokemons>();
        
	
		public HomeViewController (IntPtr handle) : base(handle)
        {  
		}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == SegueIdentifier)
            {
                var indexPath = (NSIndexPath)sender;
                PokemonDetailViewController controller = (PokemonDetailViewController)segue.DestinationViewController;

                controller.PokemonID = Pokemons[indexPath.Row].GetIdFromUrl();

            }
        }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = IocContainer.GetDependency<IPokemonController>();
            controller.listener = this;
            controller.GetAllPokemonsSpecies();

            PokemonCollectionView.RegisterNibForCell(PokemonViewCell.Nib, PokemonViewCell.Key);
            PokemonCollectionView.DataSource = new HomeViewControllerDataSource(this);
            PokemonCollectionView.Delegate = new UICollectionViewFlowDelegate(this);
            pokemonSearchBar.Delegate = new HomeViewControllerSearchBarDelegate(this);
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        public void updateView(Result<List<ResultPokemons>> data)
        {
            if (data.Success)
            {
                Pokemons = data.Value;
                PokemonCollectionView.ReloadData();
            }
        }

        private void SearchByPokemonName(string pokemonName)
        {
            controller.FilterPokemonListByName(pokemonName);
        }

        class HomeViewControllerDataSource : UICollectionViewDataSource
        {
            HomeViewController viewController;

            public HomeViewControllerDataSource(HomeViewController viewController)
            {
                this.viewController = viewController;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                PokemonViewCell cell = (PokemonViewCell)collectionView.DequeueReusableCell(PokemonViewCell.Key, indexPath);
                cell.Layer.CornerRadius = 20;
                var pokemon = viewController.Pokemons[indexPath.Row];
                cell.Pokemon = pokemon;
                return cell;
            }

            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {
                return viewController.Pokemons.Count;
            }
        }
        
        class UICollectionViewFlowDelegate : UICollectionViewDelegateFlowLayout
        {
            HomeViewController viewController;

            public UICollectionViewFlowDelegate(HomeViewController viewController)
            {
                this.viewController = viewController;
            }
            [Export("collectionView:layout:sizeForItemAtIndexPath:")]
            public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
            {
                var width = collectionView.Frame.Width * 0.48;
                var height = collectionView.Frame.Height * 0.15;

                return new CGSize(width: width, height: height);
            }

            [Export("collectionView:didSelectItemAtIndexPath:")]
            public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var pokemon = viewController.Pokemons[indexPath.Row];

                viewController.PerformSegue(HomeViewController.SegueIdentifier, indexPath);

            }
        }

        class HomeViewControllerSearchBarDelegate : UISearchBarDelegate
        {
            HomeViewController viewController;

            public HomeViewControllerSearchBarDelegate(HomeViewController viewController)
            {
                this.viewController = viewController;
            }

            public override void TextChanged(UISearchBar searchBar, string searchText)
            {
                viewController.SearchByPokemonName(searchText);
            }
        }
    }
}


