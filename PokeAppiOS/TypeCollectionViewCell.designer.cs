// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PokeAppiOS
{
	[Register ("TypeCollectionViewCell")]
	partial class TypeCollectionViewCell
	{
		[Outlet]
		UIKit.UILabel typeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (typeLabel != null) {
				typeLabel.Dispose ();
				typeLabel = null;
			}
		}
	}
}
