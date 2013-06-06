using System;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ExtendedDisplay.Framework.iOS
{
    public static class IosExtensions
    {
        public static byte[] ToBytes(this UIImage image)
        {
            var imageData = image.AsJPEG(0.7f);
            var imageBytes = new byte[imageData.Length];

            Marshal.Copy(imageData.Bytes, imageBytes, 0, Convert.ToInt32(imageData.Length));

            return imageBytes;
        }

        public static UIImage TakeScreenshot(this UIView view)
        {
            UIGraphics.BeginImageContext(new SizeF(1024, 768));
            var context = UIGraphics.GetCurrentContext();

            UIImage image = null;
            if (context != null)
            {
                view.Layer.RenderInContext(context);

                image = UIGraphics.GetImageFromCurrentImageContext();
            }

            UIGraphics.EndImageContext();

            return image;
        }    

        public static IPAddress GetIPAddress()
        {
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces()) {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses) {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork) {
                            return addrInfo.Address;
                        }
                    }
                }  
            }

            return null;
        }
    }
}

