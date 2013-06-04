// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ExtendedDisplay.iOS
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView imgScreenshot { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnDisconnect { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView viewConnect { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtIpAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtPort { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnConnect { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgScreenshot != null) {
				imgScreenshot.Dispose ();
				imgScreenshot = null;
			}

			if (btnDisconnect != null) {
				btnDisconnect.Dispose ();
				btnDisconnect = null;
			}

			if (viewConnect != null) {
				viewConnect.Dispose ();
				viewConnect = null;
			}

			if (txtIpAddress != null) {
				txtIpAddress.Dispose ();
				txtIpAddress = null;
			}

			if (txtPort != null) {
				txtPort.Dispose ();
				txtPort = null;
			}

			if (btnConnect != null) {
				btnConnect.Dispose ();
				btnConnect = null;
			}
		}
	}
}
