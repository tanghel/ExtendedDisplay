// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace ExtendedDisplay.Client.MacOsX
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		MonoMac.AppKit.NSTextField txtIp { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtPort { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView imgScreenshot { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton btnConnect { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtIp != null) {
				txtIp.Dispose ();
				txtIp = null;
			}

			if (txtPort != null) {
				txtPort.Dispose ();
				txtPort = null;
			}

			if (imgScreenshot != null) {
				imgScreenshot.Dispose ();
				imgScreenshot = null;
			}

			if (btnConnect != null) {
				btnConnect.Dispose ();
				btnConnect = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
