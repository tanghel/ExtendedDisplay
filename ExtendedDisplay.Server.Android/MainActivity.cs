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
using Android.Widget;

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

        private DrawingView drawingView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Text = GetIPAddress().ToString();

            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

            this.drawingView = new DrawingView(this.ApplicationContext);

            this.AddContentView(this.drawingView, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.FillParent, LinearLayout.LayoutParams.FillParent));

            this.drawingView.Invalidate();

            AsyncTcpServer.Initialize(IPAddress.Any, PORT);
            AsyncTcpServer.Instance.Start();

            CaptureService.Initialize(new ScreenCapture(this.Window.DecorView.RootView), 1000);
            CaptureService.Instance.ScreenCaptured += (sender, args) => 
            {
                AsyncTcpServer.Instance.Write(JsonConvert.SerializeObject(args.Value));
            };
            CaptureService.Instance.Start();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Move:
                    this.drawingView.PointToDraw = new PointF(e.RawX, e.RawY - 45);
                    break;
                case MotionEventActions.Down:
                    this.drawingView.PointToDraw = new PointF(e.RawX, e.RawY - 45);
                    break;
                default:
                    break;
            }

            return base.OnTouchEvent(e);
        }
    }
}


