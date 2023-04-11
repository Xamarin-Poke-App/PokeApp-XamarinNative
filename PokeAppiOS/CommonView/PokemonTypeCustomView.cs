using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PokeAppiOS.CommonView
{
	public partial class PokemonTypeCustomView : UIView
	{
        public UILabel PokemonTypeNameLabel;

        public PokemonTypeCustomView(string TypeName, UIColor Color)
        {
            PokemonTypeNameLabel = new UILabel();
            PokemonTypeNameLabel.Text = TypeName;
            SetupView(Color);
            // Note: this .ctor should not contain any initialization logic.
        }

        private void SetupView(UIColor Color)
        {
            var margins = this.LayoutMarginsGuide;
            this.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(PokemonTypeNameLabel);
            this.BackgroundColor = Color.ColorWithAlpha((nfloat)0.4);
            PokemonTypeNameLabel.Font = PokemonTypeNameLabel.Font.WithSize(12);
            PokemonTypeNameLabel.Font = UIFont.BoldSystemFontOfSize(12);
            PokemonTypeNameLabel.TextColor = UIColor.White;
            PokemonTypeNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                this.LayoutMarginsGuide.LeadingAnchor.ConstraintEqualTo(PokemonTypeNameLabel.LayoutMarginsGuide.LeadingAnchor, -15),
                this.LayoutMarginsGuide.TrailingAnchor.ConstraintEqualTo(PokemonTypeNameLabel.LayoutMarginsGuide.TrailingAnchor, 15),
                this.LayoutMarginsGuide.TopAnchor.ConstraintEqualTo(PokemonTypeNameLabel.LayoutMarginsGuide.TopAnchor, -5),
                this.LayoutMarginsGuide.BottomAnchor.ConstraintEqualTo(PokemonTypeNameLabel.LayoutMarginsGuide.BottomAnchor, 5)
            });

            this.Layer.CornerRadius = 5;
        }
    }
}

