using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ExtendedDisplay.Framework.Cross;
using System.Net;
using ExtendedDisplay.Framework.iOS;
using Newtonsoft.Json;
using MonoTouch.CoreGraphics;

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

        private DrawingView drawingView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            UIApplication.SharedApplication.IdleTimerDisabled = true;

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

            drawingView = new DrawingView();
            drawingView.BackgroundColor = UIColor.Clear;
            drawingView.Frame = new RectangleF(0, 0, 1024, 768);
            drawingView.Bounds = new RectangleF(0, 0, 1024, 768);

            this.View.AddSubview(drawingView);
        }

        [Obsolete ("Deprecated in iOS 6.0")]
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            UIApplication.SharedApplication.IdleTimerDisabled = true;

        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var point = touch.LocationInView(touch.View);

                this.drawingView.PointToDraw = point;
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var point = touch.LocationInView(touch.View);

                this.drawingView.PointToDraw = point;
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

//            lblHelloWorld.Text = "Touches ended";
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return true;
        }

        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
        {
            return UIInterfaceOrientation.LandscapeLeft;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Landscape;
        }
    }
}

