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
using SharedCode.Helpers;
using FFImageLoading;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace PokeAppAndroid.Adapters
{
    public class PokemonAdapter : RecyclerView.Adapter
    {
        public List<PokemonLocal> pokemomList;
        public event EventHandler<int> ItemClick;

        private Context _context = null;

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
            viewHolder.TvPokemonName.Text = value.Name.FormatedName();
            viewHolder.TvPokemonNumber.Text = "#" + pokemonID;
            ImageLoaderService.LoadImageFromUrl(value.RegularSpriteUrl)
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(viewHolder.IvPokemonImage);

            if (_context == null) return;
            var mLayoutManager = new LinearLayoutManager(_context, LinearLayoutManager.Horizontal, false);
            viewHolder.RvTypes.SetLayoutManager(mLayoutManager);

            if (viewHolder.RvTypes.ItemDecorationCount < 1)
                viewHolder.RvTypes.AddItemDecoration(new SpaceItemDecorator(15, 0));

            var adapter = new PokemonTypeAdapter(value.TypesArray.ToList());
            viewHolder.RvTypes.SetAdapter(adapter);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            _context = parent.Context;
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
            public RecyclerView RvTypes;
            public TextView TvRegion { get; set; }

            public PokemonViewHolder(Android.Views.View card, Action<int> listener) : base(card)
            {
                TvPokemonName = card.FindViewById<TextView>(Resource.Id.tv_pokemonName);
                IvPokemonImage = card.FindViewById<ImageView>(Resource.Id.PokemonImage);
                TvPokemonNumber = card.FindViewById<TextView>(Resource.Id.TvPokemonNumber);
                Container = card.FindViewById<ConstraintLayout>(Resource.Id.Container);
                RvTypes = card.FindViewById<RecyclerView>(Resource.Id.RvTypes);
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

