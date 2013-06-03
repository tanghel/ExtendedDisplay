using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExtendedDisplay.Android
{
    public class AsyncTcpClient
    {
        private const int BUFFER_SIZE = 32768;
        
        private readonly TcpClient tcpClient;
        
        public event GenericDataEventHandler<string> DataReceived;
        
        private void NotifyDataReceived(string data)
        {
            if (this.DataReceived != null)
            {
                this.DataReceived(this, new GenericDataEventArgs<string>(data));
            }
        }
        
        public AsyncTcpClient()
        {
            this.tcpClient = new TcpClient();
        }
        
        private void Listen()
        {
            var stream = this.tcpClient.GetStream();
            var responseBytes = new byte[BUFFER_SIZE];
            
            var responseBuilder = new StringBuilder();
            while (true)
            {
                var length = stream.Read(responseBytes, 0, BUFFER_SIZE);
                
                var responseString = Encoding.ASCII.GetString(responseBytes.Take(length).ToArray());
                
                responseBuilder.Append(responseString);
                
                if (responseString.Contains(ClientExtensions.END_OF_TRANSMISSION_CHARACTER))
                {
                    var index = responseBuilder.ToString().IndexOf(ClientExtensions.END_OF_TRANSMISSION_CHARACTER);
                    
                    responseBuilder.Remove(index, 1);
                    
                    this.NotifyDataReceived(responseBuilder.ToString().Substring(0, index));
                    responseBuilder.Remove(0, index);
                }
            }
        }
        
        private void Send(string data)
        {
            var stream = this.tcpClient.GetStream();
            var bytes = Encoding.ASCII.GetBytes(data);
            
            stream.Write(bytes, 0, bytes.Length);
        }
        
        public void Connect(IPAddress address, int port)
        {
            this.tcpClient.Connect(address, port);
            
            Task.Factory.StartNew(this.Listen);
        }

        public void Disconnect()
        {
            this.tcpClient.Close();
        }
        
        public void SendAsync(string data)
        {
            Task.Factory.StartNew(() => this.Send(data));
        }
    }
}
