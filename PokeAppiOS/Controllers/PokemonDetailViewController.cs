using System;
using Foundation;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;
using FFImageLoading;
using UIKit;

namespace PokeAppiOS.Controllers
{
	public partial class PokemonDetailViewController : UIViewController, IPokemonDetailControllerListener
	{
		public int PokemonID;
        private IPokemonDetailController controller;
        public PokemonDetailViewController(IntPtr handle) : base(handle)
        {
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
			controller.LoadPokemonInfo(PokemonID);
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.Success)
			{
                pokemonNameLabel.Text = pokemon.Value.Name;
                Title = pokemon.Value.Name;
                ImageLoaderService.LoadImageFromUrl(pokemon.Value.RegularSpriteUrl)
                    .Error(ex =>
                    {
                        Console.Write(ex);
                    })
                    .Into(pokemonImageView);
            }
        }
    }
}
