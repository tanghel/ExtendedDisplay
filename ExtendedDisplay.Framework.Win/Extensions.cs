using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ExtendedDisplay
{
    public static class Extensions
    {
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string ConvertToString(this Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                EncoderParameters eps = new EncoderParameters(1);
                eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 70L);
                ImageCodecInfo ici = GetEncoder(ImageFormat.Jpeg);
                Stopwatch.Measure("Image save to jpg", () => img.Save(ms, ici, eps));

                // Convert Image to byte[]
                //img.Save(ms, ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static Image ConvertToImage(this string base64String)
        {
            // Convert Base64 String to byte[]
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static byte ToByte(this int value)
        {
            if (value < byte.MinValue)
            {
                value = byte.MinValue;
            }
            else if (value > byte.MaxValue)
            {
                value = byte.MaxValue;
            }

            return (byte)value;
        }

        private const byte BYTE_MIDDLE = (byte)128;

        private static byte GetNormalizedDifference(byte source, byte destination)
        {
            if (destination > source)
            {
                return (byte)(destination - source + BYTE_MIDDLE);
            }
            else
            {
                return (byte)(destination + BYTE_MIDDLE - source);
            }
            //return ((int)destination - (int)source + 128).ToByte();
        }

        private static byte GetNormalizedSum(byte original, byte diff)
        {
            return ((int)original + (int)diff - 128).ToByte();
        }

        private static Bitmap ApplyTransform(this Bitmap source, Bitmap destination, Func<byte, byte, byte> transformFunction)
        {
            if (source == null || destination == null)
            {
                return null;
            }

            var fastSource = new UnsafeBitmap(source);
            var fastDestination = new UnsafeBitmap(destination);

            fastSource.LockBitmap();
            fastDestination.LockBitmap();

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    var sourcePixel = fastSource.GetPixel(j, i);
                    var destinationPixel = fastDestination.GetPixel(j, i);

                    var diffPixel = new PixelData
                    {
                        red = transformFunction.Invoke(destinationPixel.red, sourcePixel.red),
                        green = transformFunction.Invoke(destinationPixel.green, sourcePixel.green),
                        blue = transformFunction.Invoke(destinationPixel.blue, sourcePixel.blue),
                    };

                    fastDestination.SetPixel(j, i, diffPixel);
                }
            }

            fastSource.UnlockBitmap();
            fastDestination.UnlockBitmap();

            return fastDestination.Bitmap;
        }

        public static Bitmap GetDifferences(this Bitmap source, Bitmap destination)
        {
            return source.ApplyTransform(destination, GetNormalizedDifference);
        }

        public static Bitmap ApplyDifferences(this Bitmap original, Bitmap diff)
        {
            return original.ApplyTransform(diff, GetNormalizedSum);
        }
    }
}
