using System;
using CoreAnimation;
using System.Drawing;
using CoreGraphics;
using Foundation;
using SharedCode.Model;
using UIKit;
using SharedCode.Model.DB;
using SharedCode.Controller;
using SharedCode.Util;
using SharedCode.Services;
using System.Linq;
using SharedCode.Helpers;
using SharedCode.Model.Api;
using FFImageLoading;

namespace PokeAppiOS.Views.Cells
{
  
    public partial class PokemonViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("PokemonViewCell");
		public static readonly UINib Nib = UINib.FromName("PokemonViewCell", NSBundle.MainBundle);


        protected PokemonViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public PokemonLocal Pokemon
		{
			set
			{
                pokemonNameLabel.Text = value.Name.FormatedName();
                pokemonNumberLabel.Text = "#" + value.Id.ToString();
                pokemonRegionLabel.Text = value.Region.FormatedName();
                pokemonFirstTypeView.Layer.CornerRadius = 10;
                pokemonSecondTypeView.Layer.CornerRadius = 10;
                ImageLoaderService.LoadImageFromUrl(value.RegularSpriteUrl)
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(pokemonImageView);
                // Pokemons will only have as max two types of pokemon
                if (value.TypesArray.Count() == 1)
                {
                    pokemonSecondTypeView.Hidden = true;
                    pokemonViewBackground.BackgroundColor = UIColor.FromName(value.TypesArray.FirstOrDefault()).ColorWithAlpha(0.8f);
                    pokemonFirstTypeView.BackgroundColor = UIColor.FromName(value.TypesArray.FirstOrDefault());
                    pokemonFirstTypeLabel.Text = value.TypesArray.FirstOrDefault();
                }
                else if (value.TypesArray.Count() > 1)
                {
                    pokemonSecondTypeView.Hidden = false;
                    pokemonViewBackground.BackgroundColor = UIColor.FromName(value.TypesArray.FirstOrDefault()).ColorWithAlpha(0.8f);
                    pokemonFirstTypeView.BackgroundColor = UIColor.FromName(value.TypesArray.FirstOrDefault());
                    pokemonFirstTypeLabel.Text = value.TypesArray.FirstOrDefault();
                    pokemonSecondTypeView.BackgroundColor = UIColor.FromName(value.TypesArray.Last());
                    pokemonSecondTypeLabel.Text = value.TypesArray.Last();
                }
            }
        }
    }
}
