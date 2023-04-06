using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using PokeAppAndroid.Utils;
using SharedCode.Controller;
using SharedCode.Model;
using SharedCode.Model.DB;
using SharedCode.Services;
using SharedCode.Util;
using Square.Picasso;
using SharedCode.Helpers;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace PokeAppAndroid.Adapters
{
    public class PokemonAdapter : RecyclerView.Adapter
    {
        public List<PokemonLocal> pokemomList;
        public event EventHandler<int> ItemClick;

        public PokemonAdapter(List<PokemonLocal> pokemons)
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
            string pokemonID = value.Id.ToString();

            viewHolder.Container.SetDrawableBackgroundForType(value.TypesArray.First());
            viewHolder.TvRegion.Text = value.Region.FormatedName();

            viewHolder.FirstType.Text = value.TypesArray.First().ToString().FormatedName();
            viewHolder.FirstType.SetDrawableBackgroundForType(value.TypesArray.First());
            viewHolder.SecondType.Visibility = ViewStates.Gone;
            if (value.TypesArray.Count() > 1)
            {
                viewHolder.SecondType.Visibility = ViewStates.Visible;
                viewHolder.SecondType.Text = value.TypesArray.Last().ToString().FormatedName();
                viewHolder.SecondType.SetDrawableBackgroundForType(value.TypesArray.Last());
            }

            viewHolder.TvPokemonName.Text = value.Name.FormatedName();
            viewHolder.TvPokemonNumber.Text = "#" + pokemonID;
            Console.WriteLine(value.RegularSpriteUrl);
            Picasso.Get()
                .Load(value.RegularSpriteUrl)
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
            public TextView FirstType { get; set; }
            public TextView SecondType { get; set; }
            public TextView TvRegion { get; set; }


            public PokemonViewHolder(Android.Views.View card, Action<int> listener) : base(card)
            {
                TvPokemonName = card.FindViewById<TextView>(Resource.Id.tv_pokemonName);
                IvPokemonImage = card.FindViewById<ImageView>(Resource.Id.PokemonImage);
                TvPokemonNumber = card.FindViewById<TextView>(Resource.Id.TvPokemonNumber);
                Container = card.FindViewById<ConstraintLayout>(Resource.Id.Container);
                FirstType = card.FindViewById<TextView>(Resource.Id.FirstType);
                SecondType = card.FindViewById<TextView>(Resource.Id.SecondType);
                TvRegion = card.FindViewById<TextView>(Resource.Id.TvRegion);
                card.Click += (sender, e) => listener(base.AbsoluteAdapterPosition);
            }
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }  
    }
}

