using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net.Sockets;
using ExtendedDisplay.Client;
using System.Net;
using Newtonsoft.Json;

namespace ExtendedDisplay
{
    public partial class ExtendedDisplayViewController : UIViewController
    {
        private readonly AsyncTcpClient tcpClient;

        private bool isConnected = false;

        public ExtendedDisplayViewController() : base ("ExtendedDisplayViewController", null)
        {
            this.tcpClient = new AsyncTcpClient();
            this.tcpClient.DataReceived += (object sender, GenericDataEventArgs<string> args) =>
            {
                var bitmapContainer = JsonConvert.DeserializeObject<BitmapContainer>(args.Value);
                
                var image = bitmapContainer.EncodedBitmap.ToUIImage();
                if (image != null)
                {
                    UIImage oldImage = null;
                    this.InvokeOnMainThread(() => 
                                            {
                        oldImage = imgDisplay.Image;
                        
                        imgDisplay.Image = image;
                    });
                    
                    if (oldImage != null)
                    {
                        oldImage.Dispose();
                    }
                }
            };
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
//            SignalRClient.Instance.BitmapReceived += (object sender, EventArgs e) => this.InvokeOnMainThread(() => imgDisplay.Image = SignalRClient.Instance.Image);
//            SignalRClient.Instance.Connected += (object sender, EventArgs e) => Console.WriteLine("Connected");
//            SignalRClient.Instance.Failed += (object sender, EventArgs e) => Console.WriteLine("Failed");
//            SignalRClient.Instance.Start("192.168.1.128");

//            SimpleHttpClient.Instance.BitmapReceived += (object sender, EventArgs e) => this.InvokeOnMainThread(() => imgDisplay.Image = SimpleHttpClient.Instance.Image);
//            SimpleHttpClient.Instance.Start("http://192.168.1.128:8080");

//            var ipAddress = "192.168.0.119";
//            var port = "8080";
//
//            tcpClient.Connect(IPAddress.Parse(ipAddress), Int32.Parse(port));
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (!this.isConnected)
            {
                var connectionInputWindow = new ConnectionInputWindow();
                connectionInputWindow.Dismissed += (sender, e) => 
                {
                    this.isConnected = true;

                    UIApplication.SharedApplication.IdleTimerDisabled = true;

                    try 
                    {
                        this.tcpClient.Connect(IPAddress.Parse(connectionInputWindow.IpAddress), Int32.Parse(connectionInputWindow.Port));
                    } catch (Exception ex) 
                    {
                        var alertView = new UIAlertView("Error", ex.Message, null, "OK");
                        alertView.Show();
                    }
                };
                connectionInputWindow.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

                this.PresentViewController(connectionInputWindow, true, null);
            }
        }

        public override bool ShouldAutorotate()
        {
            return true;
        }

        [Obsolete ("Deprecated in iOS6. Replace it with both GetSupportedInterfaceOrientations and PreferredInterfaceOrientationForPresentation")]
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.All;
        }

        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
        {
            return UIInterfaceOrientation.LandscapeLeft;
        }
    }
}

