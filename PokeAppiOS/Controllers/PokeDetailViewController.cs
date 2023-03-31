using System;
using System.Threading.Tasks;
using Foundation;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Util;
using UIKit;

namespace PokeAppiOS.View
{
	public partial class PokeDetailViewController : UIViewController, IPokemonDetailControllerListener
	{
		private PokemonDetailController controller;
		public PokeDetailViewController () : base ("PokeDetailViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			controller = new PokemonDetailController(this);
			controller.GetPokemonInfo(681);
		}

		public async void setupImageAsync()
		{
   //         var data = await NetworkHandler.LoadImage("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png");
			//pokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(data));
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
				pokemonNameLabel.Text = pokemon.Value.name;
			}
        }
    }
}


