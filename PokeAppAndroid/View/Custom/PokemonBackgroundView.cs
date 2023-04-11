using Android.Views;
using Android.Content;
using Android.Util;
using Android.Graphics;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using Android.Runtime;
using System;
using AndroidX.ConstraintLayout.Widget;

namespace PokeAppAndroid.View.Custom
{
    public class PokemonBackgroundView : ConstraintLayout
    {
        private float? alphaValue;
        private bool usingAlpha;
        private bool setForType = false;
        private string mType;


        public PokemonBackgroundView(Context context) : base(context)
        {
            Initialize(context, "");
        }

        public PokemonBackgroundView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, "", attrs);
        }

        protected PokemonBackgroundView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void Initialize(Context context, string type, IAttributeSet attrs = null)
        {
            mType = type;

            if (attrs != null)
            {
                var array = context.ObtainStyledAttributes(attrs, Resource.Styleable.PokemonBackgroundView, 0, 0);

                usingAlpha = array.GetBoolean(Resource.Styleable.PokemonBackgroundView_UsingAlpha, false);
                alphaValue = array.GetFloat(Resource.Styleable.PokemonBackgroundView_AlphaValue, 1);
                setForType = array.GetBoolean(Resource.Styleable.PokemonBackgroundView_SetForType, false);

                var colorForType = new Color(ContextCompat.GetColor(context, GetColorForType(mType)));

                if (usingAlpha)
                    colorForType.A = (byte)(alphaValue * 255);

                var roundedBackground = ContextCompat.GetDrawable(context, setForType ? Resource.Drawable.rounded_background_type : Resource.Drawable.rounded_background);

                roundedBackground.SetTint(colorForType);
                array.Recycle();
            }
        }



        private static int GetColorForType(string type)
        {
            switch (type)
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

