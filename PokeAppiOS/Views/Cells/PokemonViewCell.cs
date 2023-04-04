using System;
using CoreAnimation;
using System.Drawing;
using CoreGraphics;
using Foundation;
using SharedCode.Model;
using UIKit;
using SharedCode.Controller;
using SharedCode.Util;
using SharedCode.Services;
using System.Linq;

namespace PokeAppiOS.Views.Cells
{
  
    public partial class PokemonViewCell : UICollectionViewCell, IPokemonDetailControllerListener
	{
		public static readonly NSString Key = new NSString ("PokemonViewCell");
		public static readonly UINib Nib = UINib.FromName("PokemonViewCell", NSBundle.MainBundle);
        private IPokemonDetailController controller;


        protected PokemonViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public ResultPokemons Pokemon
		{
			set
			{
                pokemonNameLabel.Text = value.FormatedName();
                pokemonNumberLabel.Text = "#" + value.GetIdFromUrl().ToString();
                pokemonFirstTypeView.Layer.CornerRadius = 10;
                pokemonSecondTypeView.Layer.CornerRadius = 10;
                controller = IocContainer.GetDependency<IPokemonDetailController>();
                controller.listener = this;
                controller.LoadPokemonImage(value.GetIdFromUrl());
                controller.GetPokemonInfo(value.GetIdFromUrl());
            }
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.Success)
            {
                pokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(image.Value));
            }
        }

        public void updatePokemonInfo(Result<PokemonInfo> pokemon)
        {
            if (pokemon.Success)
            {
                // Pokemons will only have as max two types of pokemon
                if (pokemon.Value.types.Count == 1)
                {
                    pokemonSecondTypeView.Hidden = true;
                    pokemonViewBackground.BackgroundColor = UIColor.FromName(pokemon.Value.types.FirstOrDefault().type.name).ColorWithAlpha(0.8f);
                    pokemonFirstTypeView.BackgroundColor = UIColor.FromName(pokemon.Value.types.FirstOrDefault().type.name);
                    pokemonFirstTypeLabel.Text = pokemon.Value.types.FirstOrDefault().type.name;
                }
                else if (pokemon.Value.types.Count > 1)
                {
                    pokemonSecondTypeView.Hidden = false;
                    pokemonViewBackground.BackgroundColor = UIColor.FromName(pokemon.Value.types.FirstOrDefault().type.name).ColorWithAlpha(0.8f);
                    pokemonFirstTypeView.BackgroundColor = UIColor.FromName(pokemon.Value.types.FirstOrDefault().type.name);
                    pokemonFirstTypeLabel.Text = pokemon.Value.types.FirstOrDefault().type.name;
                    pokemonSecondTypeView.BackgroundColor = UIColor.FromName(pokemon.Value.types.Last().type.name);
                    pokemonSecondTypeLabel.Text = pokemon.Value.types.Last().type.name;
                }
            }
        }

        public void updatePokemonSpecieInfo(Result<PokemonSpecie> pokemon)
        {
            // Nothing to implement
        }
    }
}
