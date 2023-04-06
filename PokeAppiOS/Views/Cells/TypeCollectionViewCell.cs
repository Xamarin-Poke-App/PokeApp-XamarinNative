// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace PokeAppiOS
{
	public partial class TypeCollectionViewCell : UICollectionViewCell
	{
        public static readonly NSString Key = new NSString("TypeCollectionViewCell");
        public static readonly UINib Nib = UINib.FromName("TypeCollectionViewCell", NSBundle.MainBundle);

		public string TypeName;

        public TypeCollectionViewCell (IntPtr handle) : base (handle)
		{
		}

		public void UpdateCell()
		{
            if (TypeName == null) return;
            typeLabel.Text = TypeName;
            containerBackgroundView.BackgroundColor = UIColor.FromName(TypeName);
        }
	}
}
