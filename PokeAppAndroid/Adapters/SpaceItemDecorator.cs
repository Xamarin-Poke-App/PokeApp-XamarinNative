using System;
using Android.Graphics;
using AndroidX.RecyclerView.Widget;

namespace PokeAppAndroid.Adapters
{
	public class SpaceItemDecorator: RecyclerView.ItemDecoration
	{
		private int _horizontalSpacing;
		private int _verticalSpacing;

		public SpaceItemDecorator(int horizontalSpacing, int verticalSpacing)
		{
			_horizontalSpacing = horizontalSpacing;
			_verticalSpacing = verticalSpacing;
		}

        public override void GetItemOffsets(Rect outRect, Android.Views.View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);
			outRect.Top = _verticalSpacing;
			outRect.Bottom = _verticalSpacing;
			if (parent.GetChildAdapterPosition(view) == 0) return;
            outRect.Left = _horizontalSpacing;
        }
    }
}

