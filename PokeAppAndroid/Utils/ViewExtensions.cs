using System;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.Content;
using SharedCode.Util;
using SharedCode.Helpers;
using Android.Views;
using Android.Graphics;
using Android.Content;

namespace PokeAppAndroid.Utils
{
	public static class ViewExtensions
	{
		public static TextView SetDrawableBackgroundForType(this TextView view, string type)
		{
            var colorID = GetColorForType(type);
            var background = ContextCompat.GetDrawable(view.Context, Resource.Drawable.rounded_background_type);
            var color = new Android.Graphics.Color(ContextCompat.GetColor(view.Context, colorID));
            background.SetTint(color);
            view.Background = background;
            return view;
        }


        public static ConstraintLayout SetDrawableBackgroundForType(this ConstraintLayout view, string type)
        {
            var colorID = GetColorForType(type);
            var background = ContextCompat.GetDrawable(view.Context, Resource.Drawable.rounded_background);
            var color = new Android.Graphics.Color(ContextCompat.GetColor(view.Context, colorID));    
            color.A = (byte)(0.80 * 255);
            background.SetTint(color);
            view.Background = background;
            return view;
        }

        public static Color GetColorForType(Context context, string type)
        {
            var colorID = GetColorForType(type);
            var color = new Android.Graphics.Color(ContextCompat.GetColor(context, colorID));
            color.A = (byte)(0.80 * 255);
            return color;
        }

        private static int GetColorForType(string type)
        {
            var formatedType = type.FormatedName();
            switch (formatedType)
            {
                case "Bug":
                    return Resource.Color.bug;
                case "Dark":
                    return Resource.Color.dark;
                case "Dragon":
                    return Resource.Color.dragon;
                case "Electric":
                    return Resource.Color.electric;
                case "Fairy":
                    return Resource.Color.fairy;
                case "Fighting":
                    return Resource.Color.fighting;
                case "Fire":
                    return Resource.Color.fire;
                case "Flying":
                    return Resource.Color.flying;
                case "Ghost":
                    return Resource.Color.ghost;
                case "Grass":
                    return Resource.Color.grass;
                case "Ground":
                    return Resource.Color.ground;
                case "Ice":
                    return Resource.Color.ice;
                case "Normal":
                    return Resource.Color.normal;
                case "Poison":
                    return Resource.Color.poison;
                case "Psychic":
                    return Resource.Color.psychic;
                case "Rock":
                    return Resource.Color.rock;
                case "Steel":
                    return Resource.Color.steel;
                case "Water":
                    return Resource.Color.water;
                default:
                    return Resource.Color.colorAccent;
            }
        }
    }
}

