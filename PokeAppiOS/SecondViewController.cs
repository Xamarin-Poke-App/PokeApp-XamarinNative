﻿using System;
using SharedCode.Controller;
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

        public void updateView(Result<int> data)
        {
            if (data.Success)
            {
                LabelTest.Text = data.Value.ToString();
            }
            else
            {
                LabelTest.Text = data.Error;
            }
        }
    }
}


