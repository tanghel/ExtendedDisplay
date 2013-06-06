using System;
using ExtendedDisplay.Framework.Cross;
using Android.App;
using Android.Views;
using System.IO;
using Android.Graphics;

namespace ExtendedDisplay.Framework.Android
{
    public class ScreenCapture : IScreenCapture
    {
        private readonly View mainView;

        public ScreenCapture(View mainView)
        {
            this.mainView = mainView;
        }

        public byte[] CaptureScreen()
        {
            this.mainView.DrawingCacheEnabled = true;
            var bitmap = this.mainView.GetDrawingCache(true);

            using (var memoryStream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 70, memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}

