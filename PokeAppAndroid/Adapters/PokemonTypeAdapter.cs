using System;
using System.Collections.Generic;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using PokeAppAndroid.Utils;
using static PokeAppAndroid.Adapters.PokemonAdapter;

namespace PokeAppAndroid.Adapters
{
	public class PokemonTypeAdapter: RecyclerView.Adapter
    {
		public List<string> types;

		public PokemonTypeAdapter(List<string> typesList)
		{
			types = typesList;
		}

        public override int ItemCount
        {
            get { return types.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TypesViewHolder typesViewHolder = holder as TypesViewHolder;
            string item = types[position];
            typesViewHolder.TvTypeName.Text = item;
            typesViewHolder.TvTypeName.SetDrawableBackgroundForType(item);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Android.Views.View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.type_card, parent, false);
            TypesViewHolder viewHolder = new TypesViewHolder(view);
            return viewHolder;
        }

        public class TypesViewHolder : RecyclerView.ViewHolder
        {
            public TextView TvTypeName { get; set; }
            public TypesViewHolder(Android.Views.View view) : base(view)
            {
                TvTypeName = view.FindViewById<TextView>(Resource.Id.TextViewType);
            }
        }
    }
}

