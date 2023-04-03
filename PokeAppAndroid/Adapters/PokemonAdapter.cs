using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
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

        public List<ResultPokemons> pokemomList;
        public event EventHandler<int> ItemClick;

        public PokemonAdapter(List<ResultPokemons> pokemons)
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
            viewHolder.TvPokemonName.Text = value.FormatedName();
            string pokemonID = value.GetIdFromUrl().ToString();

            viewHolder.TvPokemonNumber.Text = "#" + pokemonID;
            Picasso.Get()
                .Load(value.GetPokemonImageURL(pokemonID))
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

            public PokemonViewHolder(Android.Views.View card, Action<int> listener) : base(card)
            {
                TvPokemonName = card.FindViewById<TextView>(Resource.Id.tv_pokemonName);
                IvPokemonImage = card.FindViewById<ImageView>(Resource.Id.PokemonImage);
                TvPokemonNumber = card.FindViewById<TextView>(Resource.Id.TvPokemonNumber);
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

