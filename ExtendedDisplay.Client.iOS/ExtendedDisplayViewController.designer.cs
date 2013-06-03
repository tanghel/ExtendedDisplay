// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using MonoTouch.Foundation;

namespace ExtendedDisplay.iOS
{
	[Register ("ExtendedDisplayViewController")]
	partial class ExtendedDisplayViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView imgDisplay { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgDisplay != null) {
				imgDisplay.Dispose ();
				imgDisplay = null;
			}
		}
	}
}
