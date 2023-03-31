using System;
using SharedCode.Model;
using UIKit;

namespace PokeAppiOS.Controllers
{
	public partial class PokemonDetailViewController : UIViewController
	{
		public ResultPokemons Pokemon;

		public PokemonDetailViewController(IntPtr handle) : base(handle)
        {
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			Title = Pokemon.name;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


