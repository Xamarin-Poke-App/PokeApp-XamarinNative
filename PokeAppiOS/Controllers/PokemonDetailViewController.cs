using System;
using Foundation;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
using SharedCode.Helper;
using UIKit;

namespace PokeAppiOS.Controllers
{
	public partial class PokemonDetailViewController : UIViewController, IPokemonDetailControllerListener
	{
		public int PokemonID;
		private PokemonSpecie PokemonInfo;
        private IPokemonDetailController controller;
        public PokemonDetailViewController(IntPtr handle) : base(handle)
        {
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
            controller.GetPokemonInfo(PokemonID);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.Success)
			{
				pokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(image.Value));
			}
        }

        public void updatePokemonInfo(Result<PokemonSpecie> pokemon)
        {
            if (pokemon.Success)
			{
				PokemonInfo = pokemon.Value;
				pokemonNameLabel.Text = PokemonInfo.name.Capitalize();
                controller.LoadPokemonImage(PokemonInfo.id);
			}
        }

        class PokemonDetailViewControllerDataSource : UITableViewDataSource
        {
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                throw new NotImplementedException();
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                throw new NotImplementedException();
            }

        }
    }
}


