using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PokeAppiOS.View
{
    public static class UIViewExtensions
    {
        public static void SetGradientBackground(this UIView view, UIColor color1, UIColor color2, UIColor color3)
        {
            var gradientLayer = new CAGradientLayer
            {
                Frame = view.Bounds,
                Colors = new CGColor[] { color1.CGColor, color2.CGColor, color3.CGColor },
                StartPoint = new CGPoint(0.5, 0), 
                EndPoint = new CGPoint(0.5, 1),
                Locations = new NSNumber[] { 0, 0.5, 1 }
            };

            view.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}

