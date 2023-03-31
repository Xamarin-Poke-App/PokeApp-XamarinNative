using System;
using CoreGraphics;
using UIKit;

namespace PokeAppiOS.View
{
    public class TextFieldWithBottomLine : UITextField
    {
        private UIView bottomLine;

        public TextFieldWithBottomLine(string placeholderText)
        {
            this.BackgroundColor = UIColor.Clear;
            this.BorderStyle = UITextBorderStyle.None;
            this.Font = UIFont.SystemFontOfSize(18);
            this.TextColor = UIColor.DarkText;
            this.ClearButtonMode = UITextFieldViewMode.WhileEditing;
            this.TranslatesAutoresizingMaskIntoConstraints = false;

            bottomLine = new UIView
            {
                BackgroundColor = UIColor.DarkGray,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            this.AddSubview(bottomLine);

            this.Placeholder = placeholderText;

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                TopAnchor.ConstraintEqualTo(TopAnchor),
                LeftAnchor.ConstraintEqualTo(LeftAnchor),
                RightAnchor.ConstraintEqualTo(RightAnchor),
                bottomLine.TopAnchor.ConstraintEqualTo(BottomAnchor, constant: 4),
                bottomLine.LeftAnchor.ConstraintEqualTo(LeftAnchor),
                bottomLine.RightAnchor.ConstraintEqualTo(RightAnchor),
                bottomLine.HeightAnchor.ConstraintEqualTo(1)
            });
        }
    }
}
