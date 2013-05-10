using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Runtime.InteropServices;

namespace ExtendedDisplay
{
    public static class ClientExtensions
    {
        public static char END_OF_TRANSMISSION_CHARACTER = (char)5;
     
        public static UIImage ToUIImage (this string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                NSData imageData = NSData.FromArray(imageBytes);
                return UIImage.LoadFromData(imageData);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ToBase64String(this UIImage image)
        {
            var imageData = image.AsJPEG(0.3f);
            var imageBytes = new byte[imageData.Length];

            Marshal.Copy(imageData.Bytes, imageBytes, 0, Convert.ToInt32(imageData.Length));

            return Convert.ToBase64String(imageBytes);
        }

        public static UIImage TakeScreenshot(this UIView view)
        {
            UIGraphics.BeginImageContext(view.Frame.Size);
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
    }
}
