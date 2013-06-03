using System;
using System.Linq;
using System.Net;
using ExtendedDisplay;
using ExtendedDisplay.Framework.Cross;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace SimpleTcpServer
{
    class Program
    {
        private const int PORT = 8080;

        private static string[] GetIpAddresses()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
        }

        static void Main(string[] args)
        {
            AsyncTcpServer.Initialize(IPAddress.Any, PORT);
            AsyncTcpServer.Instance.DataReceived += (sender, e) => System.Console.WriteLine("Received data from client {0}, data: {1}", e.Value1.Client.LocalEndPoint.ToString(), e.Value2);
            AsyncTcpServer.Instance.Start();

            ScreenCapturer.Instance.ScreenCaptured += (sender, e) =>
                {
                    string jsonString = null;

                    e.Value.Created = DateTime.Now;

                    Stopwatch.Measure("JsonParse", () => { jsonString = JsonConvert.SerializeObject(e.Value); });
                    Stopwatch.Measure("TcpSend", () => { AsyncTcpServer.Instance.Write(jsonString); });
                };
            ScreenCapturer.Instance.Start();

            System.Console.WriteLine("Listening for incoming connections on ip addresses {0}, port {1}. press any key to terminate the app..", string.Join(", ", GetIpAddresses()), PORT);
            System.Console.ReadLine();

            ScreenCapturer.Instance.Stop();
            //AsyncTcpServer.Instance.Stop();
        }
    }
}
