// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ExtendedDisplay.Server.iOS
{
	[Register ("ExtendedDisplay_Server_iOSViewController")]
	partial class ExtendedDisplay_Server_iOSViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblHelloWorld { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnClear { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblHelloWorld != null) {
				lblHelloWorld.Dispose ();
				lblHelloWorld = null;
			}

			if (btnClear != null) {
				btnClear.Dispose ();
				btnClear = null;
			}
		}
	}
}
