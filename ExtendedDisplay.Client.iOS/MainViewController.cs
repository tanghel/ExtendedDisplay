using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ExtendedDisplay.Framework.Cross;
using Newtonsoft.Json;
using System.Net;

namespace ExtendedDisplay.iOS
{
    public partial class MainViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        private AsyncTcpClient tcpClient;

        private void ConnectMode()
        {
            this.CreateTcpClient();

            this.viewConnect.Hidden = false;
            this.imgScreenshot.Hidden = true;
            this.btnDisconnect.Hidden = true;
        }

        private void ScreenCaptureMode()
        {
            this.viewConnect.Hidden = true;
            this.imgScreenshot.Hidden = false;
            this.btnDisconnect.Hidden = false;
        }

        private void Connect()
        {
            try 
            {
                this.tcpClient.Connect(IPAddress.Parse(txtIpAddress.Text), Int32.Parse(txtPort.Text));

                this.ScreenCaptureMode();
            } catch (Exception ex) 
            {
                var alertView = new UIAlertView("Error", ex.Message, null, "OK");
                alertView.Show();
            }
        }

        private void Disconnect()
        {
            this.tcpClient.Disconnect();
            this.ConnectMode();
        }

        private void CreateTcpClient()
        {
            if (this.tcpClient != null)
            {
                this.tcpClient.Disconnect();
            }

            this.tcpClient = new AsyncTcpClient();
            this.tcpClient.DataReceived += (sender, args) =>
            {
                var bitmapContainer = JsonConvert.DeserializeObject<BitmapContainer>(args.Value);

                var image = bitmapContainer.EncodedBitmap.ToUIImage();
                if (image != null)
                {
                    UIImage oldImage = null;
                    this.InvokeOnMainThread(() => 
                                            {
                        oldImage = imgScreenshot.Image;

                        imgScreenshot.Image = image;
                    });

                    if (oldImage != null)
                    {
                        oldImage.Dispose();
                    }
                }
            };
        }

        public MainViewController()
			: base (UserInterfaceIdiomIsPhone ? "MainViewController_iPhone" : "MainViewController_iPad", null)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.

            // Release any cached data, images, etc that aren't in use.
            this.ConnectMode();

            this.btnConnect.TouchUpInside += (sender, e) => this.Connect();
            this.btnDisconnect.TouchUpInside += (sender, e) => this.Disconnect();
        }

        [Obsolete ("Deprecated in iOS6. Replace it with both GetSupportedInterfaceOrientations and PreferredInterfaceOrientationForPresentation")]
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft | toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
        }

        public override UIInterfaceOrientation InterfaceOrientation
        {
            get
            {
                return UIInterfaceOrientation.LandscapeLeft;
            }
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Landscape;
        }

        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
        {
            return UIInterfaceOrientation.LandscapeLeft;
        }
    }
}

