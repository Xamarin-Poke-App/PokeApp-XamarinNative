﻿using System;
using Foundation;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
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
            controller.GetPokemonSpecieInfo(PokemonID);
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

        public void updatePokemonSpecieInfo(Result<PokemonSpecie> pokemon)
        {
            if (pokemon.Success)
			{
				PokemonInfo = pokemon.Value;
				pokemonNameLabel.Text = PokemonInfo.name;
                Title = PokemonInfo.name;
                controller.LoadPokemonImage(PokemonInfo.id);
			}
        }

        public void updatePokemonInfo(Result<PokemonInfo> pokemon)
        {
            // Nothing to implement
        }
    }
}


