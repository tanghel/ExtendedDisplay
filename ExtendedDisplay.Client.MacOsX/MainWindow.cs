using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using ExtendedDisplay.Framework.Cross;
using System.Net;

namespace ExtendedDisplay.Client.MacOsX
{
    public partial class MainWindow : MonoMac.AppKit.NSWindow
    {
        private AsyncTcpClient client;

		#region Constructors
		
		// Called when created from unmanaged code
        public MainWindow(IntPtr handle) : base (handle)
        {
            Initialize();
        }
		// Called when created directly from a XIB file
        [Export ("initWithCoder:")]
		public MainWindow(NSCoder coder) : base (coder)
        {
            Initialize();
        }
		// Shared initialization code
        void Initialize()
        {

            this.client = new AsyncTcpClient();
            this.client.DataReceived += (sender, args) =>
            {
                var startIndex = args.Value.IndexOf("/9");
                var endIndex = args.Value.LastIndexOf("\",\"");

                var imageString = args.Value.Substring(startIndex, endIndex-startIndex);
                var imageBytes = Convert.FromBase64String(imageString);

                this.InvokeOnMainThread(() =>
                {
                    var image = new NSImage(NSData.FromArray(imageBytes));
                    this.imgScreenshot.Image = image;
                });
            };
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.btnConnect.Activated += (sender, e) => 
            {
                this.client.Connect(IPAddress.Parse(this.txtIp.StringValue), this.txtPort.IntValue);
            };
        }

		#endregion
    }
}

