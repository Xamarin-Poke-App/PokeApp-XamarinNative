using System;
using System.Collections.Generic;
using AndroidX.RecyclerView.Widget;
using SharedCode.Model.DB;
using SharedCode.Model.Api;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using System.Linq;
using Android.Graphics;
using SharedCode.Services;
using SharedCode.Util;
using FFImageLoading;

namespace PokeAppAndroid.Adapters
{
	public class EvolutionChainAdapter : RecyclerView.Adapter
    {
        private List<List<ResultItem>> _pokemomPairList;
        private Color _primaryColor;

        public override int ItemCount => _pokemomPairList.Count;

        public EvolutionChainAdapter(List<List<ResultItem>> pairList, Color primaryColor)
        {
            _pokemomPairList = pairList;
            _primaryColor = primaryColor;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            EvolutionChainHolder viewHolder = holder as EvolutionChainHolder;
            var item = _pokemomPairList[position];

            ResultItem pokemonFrom = item.FirstOrDefault();
            int pokemonFromId = pokemonFrom.GetIdFromUrl();

            ImageLoaderService.LoadImageFromUrl(pokemonFrom.GetPokemonImageURL(pokemonFromId.ToString()))
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(viewHolder.EvolvesFromImage);
            viewHolder.EvolvesFromLabel.Text = pokemonFrom.FormatedName();
            viewHolder.EvolvesFromLabel.SetTextColor(_primaryColor);

            ResultItem pokemonTo = item.LastOrDefault();
            int pokemonToId = pokemonTo.GetIdFromUrl();

            ImageLoaderService.LoadImageFromUrl(pokemonTo.GetPokemonImageURL(pokemonFromId.ToString()))
                .Error(ex =>
                {
                    Console.Write(ex);
                })
                .Into(viewHolder.EvolvesToImage);
            viewHolder.EvolvesToLabel.Text = pokemonTo.FormatedName();
            viewHolder.EvolvesToLabel.SetTextColor(_primaryColor);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Android.Views.View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.evolution_chain_item, parent, false);
            EvolutionChainHolder holder = new EvolutionChainHolder(view);
            return holder;
        }

        public class EvolutionChainHolder : RecyclerView.ViewHolder
        {
            public TextView EvolvesFromLabel { get; set; }
            public ImageView EvolvesFromImage { get; set; }
            public TextView EvolvesToLabel { get; set; }
            public ImageView EvolvesToImage { get; set; }
            public ImageView ArrowImage { get; set; }

            public EvolutionChainHolder(Android.Views.View container) : base(container)
            {
                EvolvesFromLabel = container.FindViewById<TextView>(Resource.Id.TvEvolvesFrom);
                EvolvesFromImage = container.FindViewById<ImageView>(Resource.Id.IvEvolvesFrom);
                EvolvesToLabel = container.FindViewById<TextView>(Resource.Id.TvEvolvesTo);
                EvolvesToImage = container.FindViewById<ImageView>(Resource.Id.IvEvolvesTo);
                ArrowImage = container.FindViewById<ImageView>(Resource.Id.IvArrow);
            }
        }
    }
}

