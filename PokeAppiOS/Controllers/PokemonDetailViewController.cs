using System;
using Foundation;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;
using SharedCode.Helpers;
using ObjCRuntime;
using UIKit;
using PokeAppiOS.Views;
using PokeAppiOS.CommonView;
using SharedCode.Model.Api;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace PokeAppiOS.Controllers
{
	public partial class PokemonDetailViewController : UIViewController, IPokemonDetailControllerListener
	{
		public int PokemonID;
        private IPokemonDetailController controller;
        private UIViewController CurrentViewController;
        private EvolutionChainResponse _evolutionChainResponse = null;
        private PokemonLocal _selectedPokemon = null;

        public PokemonDetailViewController(IntPtr handle) : base(handle)
        {
		}

        Lazy<PokemonBaseInfoViewController> pokemonBaseInfoViewController = new Lazy<PokemonBaseInfoViewController>(() =>
        {
            try
            {
                var viewController = UIStoryboard.FromName("Home", null).InstantiateViewController("PokemonBaseInfoViewController") as PokemonBaseInfoViewController;
                return viewController;
            } catch (Exception e)
            {
                throw e;
            }
        });

        Lazy<PokemonEvolutionViewController> pokemonEvolutionViewController = new Lazy<PokemonEvolutionViewController>(() =>
        {
            try
            {
                var viewController = UIStoryboard.FromName("Home", null).InstantiateViewController("PokemonEvolutionViewController") as PokemonEvolutionViewController;
                return viewController;
            } catch (Exception e)
            {
                throw e;
            }
        });

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            controller = IocContainer.GetDependency<IPokemonDetailController>();
            controller.listener = this;
			controller.LoadPokemonImage(PokemonID);
			controller.LoadPokemonInfo(PokemonID);
            // SegmentedControl
            statsSegmentedControl.SelectedSegment = 0;
            CurrentViewController = pokemonBaseInfoViewController.Value;
            UpdateView();
		}

		void SetupView(PokemonLocal pokemon)
        {
            NavigationController.NavigationBar.TintColor = UIColor.White;
            var pokemonTypeViews = new List<PokemonTypeCustomView>();
            foreach(var type in pokemon.TypesArray)
            {
                var typeViewColor = UIColor.FromName(type);
                PokemonTypeCustomView typeView = new PokemonTypeCustomView(type, typeViewColor);
                pokemonTypeViews.Add(typeView);
            }
            
            pokemonTypesStackView.Spacing = 5;
            pokemonTypesStackView.Distribution = UIStackViewDistribution.FillProportionally;

            foreach(var typeView in pokemonTypeViews)
            {
                pokemonTypesStackView.AddArrangedSubview(typeView);
            }
            SetupSegmentedControl();
        }

        void SetupSegmentedControl()
        {
            // Configure segmented control
            statsSegmentedControl.RemoveAllSegments();
            statsSegmentedControl.InsertSegment("About", 0, true);
            statsSegmentedControl.InsertSegment("Evolution", 1, true);
            statsSegmentedControl.SelectedSegment = 0;
            statsSegmentedControl.AddTarget(this, new Selector("SelectionDidChange:"), UIControlEvent.ValueChanged);

            var titleNormalTextAttributes = new UITextAttributes();
            titleNormalTextAttributes.TextColor = UIColor.Black;
            statsSegmentedControl.SetTitleTextAttributes(titleNormalTextAttributes, UIControlState.Normal);

            var titleSelectedTextAttributes = new UITextAttributes();
            titleSelectedTextAttributes.TextColor = UIColor.White;
            statsSegmentedControl.SetTitleTextAttributes(titleSelectedTextAttributes, UIControlState.Selected);
        }

        [Export("SelectionDidChange:")]
        void SelectionDidChange(UISegmentedControl sender)
        {
            UpdateView();
        }

        void UpdateView()
        {
            if (statsSegmentedControl.SelectedSegment == 0)
            {
                CurrentViewController.RemoveFromParentViewController();
                CurrentViewController = pokemonBaseInfoViewController.Value;
                pokemonBaseInfoViewController.Value.Pokemon = _selectedPokemon;
                pokemonBaseInfoViewController.Value.UpdateInfo();
                RemoveViewControllerAsChild(pokemonEvolutionViewController.Value);
                AddViewControllerAsChild(pokemonBaseInfoViewController.Value);
            } else
            {
                CurrentViewController.RemoveFromParentViewController();
                CurrentViewController = pokemonEvolutionViewController.Value;
                pokemonEvolutionViewController.Value.EvolutionChainResponse = _evolutionChainResponse;
                pokemonEvolutionViewController.Value.Pokemon = _selectedPokemon;
                pokemonEvolutionViewController.Value.DrawEvolutionChain();
                RemoveViewControllerAsChild(pokemonBaseInfoViewController.Value);
                AddViewControllerAsChild(pokemonEvolutionViewController.Value);
            }
        }

        void AddViewControllerAsChild(UIViewController viewController)
        {
            // Add Child View Controller
            this.AddChildViewController(viewController);

            // Add Child View as Subview
            View.AddSubview(viewController.View);
            viewController.View.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, 0).Active = true;

            viewController.View.Frame = containerView.Bounds;
            containerView.AddSubview(viewController.View);

            // Notify Child View Controller
            viewController.DidMoveToParentViewController(this);
        }

        void RemoveViewControllerAsChild(UIViewController viewController)
        {
            // Notify Child View Controller
            viewController.WillMoveToParentViewController(null);

            // Remove Child View From Superview
            viewController.View.RemoveFromSuperview();

            // Notify Child View Controller
            viewController.RemoveFromParentViewController();
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
                _selectedPokemon = pokemon.Value;
                pokemonNameLabel.Text = StringUtils.ToTitleCase(pokemon.Value.Name);
                Title = "Pokemon Detail";
                var primaryColor = UIColor.FromName(pokemon.Value.TypesArray.FirstOrDefault()).ColorWithAlpha(0.8f);
                View.BackgroundColor = primaryColor;
                statsSegmentedControl.SelectedSegmentTintColor = primaryColor;
                SetupView(pokemon.Value);
                controller.GetEvolutionChainByPokemonId(pokemon.Value.EvolutionChainId);
                UpdateView();
            }
        }

        public void updateEvoutionChain(Result<EvolutionChainResponse> evolutionChain)
        {
            if (evolutionChain.IsFailure) return;
            _evolutionChainResponse = evolutionChain.Value;
        }
    }
}
