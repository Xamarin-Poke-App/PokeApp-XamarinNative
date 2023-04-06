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
using CoreAudioKit;
using PokeAppiOS.Controllers;
using System.Collections.Generic;

namespace PokeAppiOS.Views.Cells
{

    public partial class PokemonViewCell : UICollectionViewCell, IPokemonDetailControllerListener
    {
        public static readonly NSString Key = new NSString("PokemonViewCell");
        public static readonly UINib Nib = UINib.FromName("PokemonViewCell", NSBundle.MainBundle);
        private IPokemonDetailController controller;


        protected PokemonViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public PokemonLocal Pokemon
        {
            get { return Pokemon; }
            set
            {
                pokemonNameLabel.Text = value.Name.FormatedName();
                pokemonNumberLabel.Text = "#" + value.Id.ToString();
                pokemonRegionLabel.Text = value.Region.FormatedName();
                controller = IocContainer.GetDependency<IPokemonDetailController>();
                controller.listener = this;
                controller.LoadPokemonImage(value.Id);
                pokemonViewBackground.BackgroundColor = UIColor.FromName(value.TypesArray.FirstOrDefault()).ColorWithAlpha(0.8f);
                pokemonTypesCollectionView.BackgroundColor = UIColor.Clear.ColorWithAlpha(0f);
                pokemonTypesCollectionView.RegisterNibForCell(TypeCollectionViewCell.Nib, TypeCollectionViewCell.Key);
                pokemonTypesCollectionView.DataSource = new PokemonViewCellDataSource(this, value.TypesArray.ToList());
                pokemonTypesCollectionView.Delegate = new PokemonViewFlowLayout(this);
            }
        }

        public void updatePokemonImage(Result<byte[]> image)
        {
            if (image.Success)
            {
                pokemonImageView.Image = UIImage.LoadFromData(NSData.FromArray(image.Value));
            }
        }

        public void updatePokemonInfo(Result<PokemonLocal> pokemon)
        {
            if (pokemon.Success)
            {

            }
        }

        public class PokemonViewCellDataSource : UICollectionViewDataSource
        {
            PokemonViewCell cell;
            List<string> pokemonTypes;
            public PokemonViewCellDataSource(PokemonViewCell cell, List<string> typesList)
            {
                this.cell = cell;
                pokemonTypes = typesList;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                TypeCollectionViewCell cell = (TypeCollectionViewCell)collectionView.DequeueReusableCell(TypeCollectionViewCell.Key, indexPath);
                cell.Layer.CornerRadius = 5;
                var typeString = pokemonTypes[indexPath.Row];

                cell.TypeName = typeString;
                cell.UpdateCell();

                return cell;
            }

            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {
                return pokemonTypes.Count;
            }
        }

        public class PokemonViewFlowLayout: UICollectionViewDelegateFlowLayout
        {
            PokemonViewCell cell;
            public PokemonViewFlowLayout(PokemonViewCell cell)
            {
                this.cell = cell;
            }

            [Export("collectionView:layout:sizeForItemAtIndexPath:")]
            public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
            {
                var width = 45;
                var height = 25;

                return new CGSize(width: width, height: height);
            }
        }
    }
}
