using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.IO;
using Android.Graphics;
using System.Threading.Tasks;
using Java.Lang;
using ExtendedDisplay.Framework.Cross;
using System.Net;
using ExtendedDisplay.Framework.Android;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;

namespace ExtendedDisplay.Server.Android
{
    [Activity (Label = "ExtendedDisplay.Server.Android", MainLauncher = true)]
	public class Activity1 : Activity
    {
        private const int PORT = 8080;

        int count = 1;

        public static IPAddress GetIPAddress()
        {
//            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces()) {
//                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
//                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) {
//                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses) {
//                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork) {
//                            return addrInfo.Address;
//                        }
//                    }
//                }  
//            }
//
//            return null;

            var iplist = (from a in Dns.GetHostAddresses(Dns.GetHostName())
                          where a.AddressFamily == AddressFamily.InterNetwork
                          select a).ToArray();
            return iplist[0];
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
			
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

//            Task.Factory.StartNew(() =>
//            {
//                Thread.Sleep(5000);
//
//                var rootView = Window.DecorView.RootView;
//                rootView.DrawingCacheEnabled = true;
//                var bitmap = rootView.GetDrawingCache(true);
//
//                using (var memoryStream = new MemoryStream())
//                {
//                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 70, memoryStream);
//
//                    var bytes = memoryStream.ToArray();
//                }
//            });

//            var address = GetIPAddress();

//            AsyncTcpServer.Initialize(IPAddress.Any, PORT);
//            AsyncTcpServer.Instance.Start();
//
//            CaptureService.Initialize(new ScreenCapture(this.Window.DecorView.RootView), 1000);
//            CaptureService.Instance.ScreenCaptured += (sender, args) => 
//            {
//                AsyncTcpServer.Instance.Write(JsonConvert.SerializeObject(args.Value));
//            };
//            CaptureService.Instance.Start();
        }
    }
}


