using System;
using System.Net;
using ExtendedDisplay.Framework.Cross;

namespace SimpleTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int responseCount = 0;

                System.Console.WriteLine("Please enter the IP Address of the server, or leave blank for 192.168.1.128: ");
                var ipAddress = System.Console.ReadLine();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = "192.168.1.128";
                }

                var client = new AsyncTcpClient();
                client.DataReceived += (sender, e) => System.Console.WriteLine("{0}: Received from server something of size: {1}", ++responseCount, e.Value.Length);
                client.Connect(IPAddress.Parse(ipAddress), 8080);

                System.Console.WriteLine("Connected!");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
