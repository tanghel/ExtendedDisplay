using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ExtendedDisplay.Framework.Cross;
using System.Net;
using ExtendedDisplay.Framework.iOS;
using Newtonsoft.Json;

namespace ExtendedDisplay.Server.iOS
{
    public partial class ExtendedDisplay_Server_iOSViewController : UIViewController
    {
        private const int PORT = 8080;

        public ExtendedDisplay_Server_iOSViewController() : base ("ExtendedDisplay_Server_iOSViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
            AsyncTcpServer.Initialize(IPAddress.Any, PORT);
            AsyncTcpServer.Instance.Start();

            CaptureService.Initialize(new ScreenCapture(), 1000);
            CaptureService.Instance.ScreenCaptured += (sender, args) => 
            {
                AsyncTcpServer.Instance.Write(JsonConvert.SerializeObject(args.Value));
            };
            CaptureService.Instance.Start();

            lblHelloWorld.Text = (IosExtensions.GetIPAddress() ?? IPAddress.Any).ToString();
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return true;
        }
    }
}

