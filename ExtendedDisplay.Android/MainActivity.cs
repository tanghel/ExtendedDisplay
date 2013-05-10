using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ExtendedDisplay.Client;
using System.Net;
using Newtonsoft.Json;
using Android.Graphics.Drawables;

namespace ExtendedDisplay.Android
{
    [Activity (Label = "ExtendedDisplay.Android", MainLauncher = true)]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
			
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

            ImageView imageView = FindViewById<ImageView>(Resource.Id.imageView1);

            var tcpClient = new AsyncTcpClient();
            tcpClient.DataReceived += (object sender, GenericDataEventArgs<string> args) =>
            {
                var bitmapContainer = JsonConvert.DeserializeObject<BitmapContainer>(args.Value);

                var oldImage = imageView.ImageMatrix;

                var image = bitmapContainer.EncodedBitmap.ToUIImage();
                if (image != null)
                {
                    this.RunOnUiThread(() => 
                                            {
//                        oldImage.Dispose();
//                        oldImage = null;

                        ((BitmapDrawable)imageView.Drawable).Bitmap.Recycle();

                        imageView.SetImageBitmap(image);
//                        imgDisplay.Image = args.Value.ToUIImage();
                    });
                }
            };
            tcpClient.Connect(IPAddress.Parse("192.168.3.218"), 8080);
        }
    }
}


