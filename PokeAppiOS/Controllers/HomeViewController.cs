using System;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS.Controllers
{
	public partial class HomeViewController : UIViewController, IPokemonController
    {
        private PokemonController controller;
	
		public HomeViewController (IntPtr handle) : base(handle)
        {
            
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
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
                Console.WriteLine(data.Value.count);
            }
        }
    }
}


