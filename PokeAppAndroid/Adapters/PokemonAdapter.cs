using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using SharedCode.Model;

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
            viewHolder.TvPokemonName.Text = pokemomList[position].name;
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

            public PokemonViewHolder(Android.Views.View card, Action<int> listener) : base(card)
            {
                TvPokemonName = card.FindViewById<TextView>(Resource.Id.tv_pokemonName);
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

