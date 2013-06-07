using System;
using ExtendedDisplay.Framework.Cross;
using System.Net;
using System.Threading.Tasks;
using ExtendedDisplay.Client.MacOsX;

namespace ExtendedDisplay.Client.MacConsole
{
    class MainClass
    {
        private static int PORT = 8080;

//        private static string[] GetIpAddresses()
//        {
//            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
//        }

        public static void Main(string[] args)
        {
            AsyncTcpServer.Initialize(IPAddress.Any, PORT);
            AsyncTcpServer.Instance.DataReceived += (sender, e) => System.Console.WriteLine("Received data from client {0}, data: {1}", e.Value1.Client.LocalEndPoint.ToString(), e.Value2);
            AsyncTcpServer.Instance.Start();

            CaptureService.Initialize(new ScreenCapture(), 1000);
            CaptureService.Instance.ScreenCaptured += (sender, e) =>
            {
                var jsonString = "{" + string.Format("\"CursorX\":0,\"CursorY\":0,\"EncodedBitmap\":\"{0}\"", e.Value.EncodedBitmap) + "}";
                AsyncTcpServer.Instance.Write(jsonString);
            };
            CaptureService.Instance.Start();


            System.Console.WriteLine("Listening for incoming connections...");
            System.Console.ReadLine();

            CaptureService.Instance.Stop();
        }
    }
}
