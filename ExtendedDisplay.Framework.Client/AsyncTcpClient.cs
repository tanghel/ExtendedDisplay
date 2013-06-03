//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using System.Net.Sockets;
//using ExtendedDisplay;
//using System.Threading.Tasks;
//using ExtendedDisplay.Framework.Cross;

//namespace ExtendedDisplay.Framework.Client
//{
//    public class AsyncTcpClient
//    {
//        private const int BUFFER_SIZE = 32768;

//        private TcpClient tcpClient;

//        public event GenericDataEventHandler<string> DataReceived;

//        private void NotifyDataReceived(string data)
//        {
//            if (this.DataReceived != null)
//            {
//                this.DataReceived(this, new GenericDataEventArgs<string>(data));
//            }
//        }

//        public AsyncTcpClient()
//        {
//        }

//        private static void Listen(TcpClient tcpClient, Action<string> dataReceivedAction)
//        {
//            var stream = tcpClient.GetStream();
//            var responseBytes = new byte[BUFFER_SIZE];

//            var responseBuilder = new StringBuilder();
//            while (tcpClient.Connected)
//            {
//                int length = 0;
//                try
//                {
//                    length = stream.Read(responseBytes, 0, BUFFER_SIZE);
//                }
//                catch (ObjectDisposedException)
//                {
//                    break;
//                }

//                var responseString = Encoding.ASCII.GetString(responseBytes.Take(length).ToArray());

//                responseBuilder.Append(responseString);

//                if (responseString.Contains(CrossExtensions.END_OF_TRANSMISSION_CHARACTER))
//                {
//                    var index = responseBuilder.ToString().IndexOf(CrossExtensions.END_OF_TRANSMISSION_CHARACTER);

//                    responseBuilder.Remove(index, 1);

//                    var data = responseBuilder.ToString().Substring(0, index);

//                    dataReceivedAction.Invoke(data);

//                    responseBuilder.Remove(0, index);
//                }
//            }
//        }

//        private void Send(string data)
//        {
//            var stream = this.tcpClient.GetStream();
//            var bytes = Encoding.ASCII.GetBytes(data);

//            stream.Write(bytes, 0, bytes.Length);
//        }

//        public void Connect(IPAddress address, int port)
//        {
//            this.tcpClient = new TcpClient();
//            this.tcpClient.Connect(address, port);

//            Task.Factory.StartNew(() => Listen(this.tcpClient, data => this.NotifyDataReceived(data)));
//        }

//        public void Disconnect()
//        {
//            try
//            {
//                if (this.tcpClient != null)
//                {
//                    var tcpStream = this.tcpClient.GetStream();

//                    tcpStream.Close();
//                    this.tcpClient.Close();
//                    this.tcpClient = null;
//                }
//            }
//            catch (InvalidOperationException)
//            {
//            }
//        }

//        public void SendAsync(string data)
//        {
//            Task.Factory.StartNew(() => this.Send(data));
//        }
//    }
//}
