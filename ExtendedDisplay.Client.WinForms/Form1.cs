using System;
using System.Windows.Forms;
using System.Net;
using ExtendedDisplay;
using Newtonsoft.Json;
using ExtendedDisplay.Framework.Cross;

namespace SimpleTcpClient.WinForms
{
    public partial class Form1 : Form
    {
        private readonly AsyncTcpClient client;

        public Form1()
        {
            InitializeComponent();

            this.client = new AsyncTcpClient();
            this.client.DataReceived += (sender, args) =>
            {
                Cursor.Current = Cursors.Default;

                var bitmapContainer = JsonConvert.DeserializeObject<BitmapContainer>(args.Value);

                var newImage = bitmapContainer.EncodedBitmap.ConvertToImage();
                if (newImage != null)
                {
                    pictureBox1.SetPropertyThreadSafe("Image", newImage);
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.client.Disconnect();

            IPAddress ipAddress;
            if (!IPAddress.TryParse(txtIp.Text, out ipAddress))
            {
                MessageBox.Show("Invalid ip address!");
                return;
            }

            int port;
            if (!Int32.TryParse(txtPort.Text, out port))
            {
                MessageBox.Show("Invalid port number!");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                this.client.Connect(ipAddress, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}