using System;
using System.Collections.Generic;
using CoreAudioKit;
using CoreGraphics;
using Foundation;
using PokeAppiOS.Views.Cells;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS.Controllers
{
    public partial class HomeViewController : UIViewController, IPokemonController
    {

        public static string SegueIdentifier = "ToDetailSegue";
        private PokemonController controller;
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

                controller.Pokemon = Pokemons[indexPath.Row];

            }
        }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
            PokemonCollectionView.RegisterNibForCell(PokemonViewCell.Nib, PokemonViewCell.Key);
            PokemonCollectionView.DataSource = new HomeViewControllerDataSource(this);
            PokemonCollectionView.Delegate = new UICollectionViewFlowDelegate(this);
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


        
        
        public void updateView(Result<PokemonSpeciesResponse> data)
        {
            if (data.Success)
            {
                Pokemons = data.Value.results;
                PokemonCollectionView.ReloadData();
            }
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
                var width = collectionView.Frame.Width * 0.4;
                var height = collectionView.Frame.Height * 0.1;
                return new CGSize(width, height);

            }

            [Export("collectionView:didSelectItemAtIndexPath:")]
            public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var pokemon = viewController.Pokemons[indexPath.Row];

                viewController.PerformSegue(HomeViewController.SegueIdentifier, indexPath);

            }
        }

    }

    
}


