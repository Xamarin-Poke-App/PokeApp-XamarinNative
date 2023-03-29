using System;
using System.Collections.Generic;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS
{
	public partial class SecondViewController : UIViewController, IPokemonController
	{
        private PokemonController controller;
        public SecondViewController () : base ("SecondViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			Title = "Second View";
            LogoutButton.TouchUpInside += LogoutButton_TouchUpInside;
			controller = new PokemonController(this);
            controller.GetAllPokemonsSpecies();
		}

        private void LogoutButton_TouchUpInside(object sender, EventArgs e)
        {
			SceneDelegate.Current.Logout();
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
                LabelTest.Text = data.Value.Count.ToString();
            }
            else
            {
                LabelTest.Text = data.Error;
            }
        }
    }
}


