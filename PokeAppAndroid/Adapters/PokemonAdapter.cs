using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Services;
using SharedCode.Util;
using Square.Picasso;

namespace PokeAppAndroid.Adapters
{
    public class PokemonAdapter : RecyclerView.Adapter
    {

        public List<PokemonFixed> pokemomList;
        public event EventHandler<int> ItemClick;

        public PokemonAdapter(List<PokemonFixed> pokemons)
        {
            pokemomList = pokemons;
        }

        public override int ItemCount
        {
            get { return pokemomList.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PokemonViewHolder viewHolder = holder as PokemonViewHolder;
            var value = pokemomList[position];
            string pokemonID = value.ID;
            int primariIdColor = getColorForType(value.Types[0]);

            viewHolder.Container.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(viewHolder.Container.Context, primariIdColor)));
            viewHolder.TvPokemonName.Text = value.Name;
            viewHolder.TvPokemonNumber.Text = "#" + pokemonID;
            Picasso.Get()
                .Load(value.ImageURL)
                .Into(viewHolder.IvPokemonImage);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Android.Views.View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.pokemon_card, parent, false);
            PokemonViewHolder viewHolder = new PokemonViewHolder(view, OnClick);
            return viewHolder;
        }

        public class PokemonViewHolder : RecyclerView.ViewHolder
        {
            public TextView TvPokemonName { get; set; }
            public ImageView IvPokemonImage { get; set; }
            public TextView TvPokemonNumber { get; set; }
            public ConstraintLayout Container { get; set; }

            public PokemonViewHolder(Android.Views.View card, Action<int> listener) : base(card)
            {
                TvPokemonName = card.FindViewById<TextView>(Resource.Id.tv_pokemonName);
                IvPokemonImage = card.FindViewById<ImageView>(Resource.Id.PokemonImage);
                TvPokemonNumber = card.FindViewById<TextView>(Resource.Id.TvPokemonNumber);
                Container = card.FindViewById<ConstraintLayout>(Resource.Id.Container);
                card.Click += (sender, e) => listener(base.AbsoluteAdapterPosition);
            }
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }

        private int getColorForType(Enums.PokemonTypes type)
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

