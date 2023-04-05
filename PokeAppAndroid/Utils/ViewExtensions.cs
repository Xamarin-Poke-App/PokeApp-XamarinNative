using System;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.Content;
using SharedCode.Util;

namespace PokeAppAndroid.Utils
{
	public static class ViewExtensions
	{
		public static TextView SetDrawableBackgroundForType(this TextView view, Enums.PokemonTypes type)
		{
            var colorID = GetColorForType(type);
            var background = ContextCompat.GetDrawable(view.Context, Resource.Drawable.rounded_background_type);
            var color = new Android.Graphics.Color(ContextCompat.GetColor(view.Context, colorID));
            background.SetTint(color);
            view.Background = background;
            return view;
        }


        public static ConstraintLayout SetDrawableBackgroundForType(this ConstraintLayout view, Enums.PokemonTypes type)
        {
            var colorID = GetColorForType(type);
            var background = ContextCompat.GetDrawable(view.Context, Resource.Drawable.rounded_background);
            var color = new Android.Graphics.Color(ContextCompat.GetColor(view.Context, colorID));    
            color.A = (byte)(0.80 * 255);
            background.SetTint(color);
            view.Background = background;
            return view;
        }

        private static int GetColorForType(Enums.PokemonTypes type)
        {
            switch (type)
            {
                case Enums.PokemonTypes.Bug:
                    return Resource.Color.bug;
                case Enums.PokemonTypes.Dark:
                    return Resource.Color.dark;
                case Enums.PokemonTypes.Dragon:
                    return Resource.Color.dragon;
                case Enums.PokemonTypes.Electric:
                    return Resource.Color.electric;
                case Enums.PokemonTypes.Fairy:
                    return Resource.Color.fairy;
                case Enums.PokemonTypes.Fighting:
                    return Resource.Color.fighting;
                case Enums.PokemonTypes.Fire:
                    return Resource.Color.fire;
                case Enums.PokemonTypes.Flying:
                    return Resource.Color.flying;
                case Enums.PokemonTypes.Ghost:
                    return Resource.Color.ghost;
                case Enums.PokemonTypes.Grass:
                    return Resource.Color.grass;
                case Enums.PokemonTypes.Ground:
                    return Resource.Color.ground;
                case Enums.PokemonTypes.Ice:
                    return Resource.Color.ice;
                case Enums.PokemonTypes.Normal:
                    return Resource.Color.normal;
                case Enums.PokemonTypes.Poison:
                    return Resource.Color.poison;
                case Enums.PokemonTypes.Psychic:
                    return Resource.Color.psychic;
                case Enums.PokemonTypes.Rock:
                    return Resource.Color.rock;
                case Enums.PokemonTypes.Steel:
                    return Resource.Color.steel;
                case Enums.PokemonTypes.Water:
                    return Resource.Color.water;
                default:
                    return Resource.Color.colorAccent;
            }
        }
    }
}

