using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Android.Graphics;

namespace ExtendedDisplay
{
    public static class ClientExtensions
    {
        public static char END_OF_TRANSMISSION_CHARACTER = (char)5;
     
        public static Bitmap ToUIImage (this string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
