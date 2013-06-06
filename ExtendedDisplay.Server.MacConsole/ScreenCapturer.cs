//using System.Timers;
//using ExtendedDisplay.Framework.Cross;
////using GLib;
//using System;
//using System.IO;
//using System.Diagnostics;
//
//namespace ExtendedDisplay.Client.MacConsole
//{
//    public class ScreenCapturer
//    {
//        #region Constants
//
//        public const int SCREEN_CAPTURE_INTERVAL = 1000;
//
//        public const int MAX_NUMBER_OF_IMAGES = 10;
//
//        #endregion
//
//        #region Private Members
//
//        private int index = 0;
//
//        #endregion
//
//        #region Singleton Implementation
//
//        public static ScreenCapturer Instance { get; private set; }
//
//        static ScreenCapturer()
//        {
//            Instance = new ScreenCapturer();
//        }
//
//        #endregion
//
//        #region Private Members
//
//        private readonly object bitmapLock = new object();
//
//        private readonly Timer screenCaptureTimer;
//
//        #endregion
//
//        #region Public Events
//
//        public event GenericDataEventHandler<BitmapContainer> ScreenCaptured;
//
//        #endregion
//
//        #region Private Methods
//
//        private void NotifyScreenChanged(BitmapContainer bitmapContainer)
//        {
//            if (this.ScreenCaptured != null)
//            {
//                this.ScreenCaptured(this, new GenericDataEventArgs<BitmapContainer>(bitmapContainer));
//            }
//        }
//
//        private BitmapContainer CaptureBitmap()
//        {
//            lock (this.bitmapLock)
//            {
//                var currentIndex = this.index++ % MAX_NUMBER_OF_IMAGES;
//                var fileName = string.Format("screenshot{0}.jpg", currentIndex);
//                Stopwatch.Measure("Screenshot", () => Process.Start("screencapture", string.Format("-C -t jpg {0}", fileName)).WaitForExit());
//
//                var fileBytes = File.ReadAllBytes(fileName);
//                var fileString = Convert.ToBase64String(fileBytes);
//
//                return new BitmapContainer()
//                {
//                    EncodedBitmap = fileString
//                };
//            }
//        }
//
//        #endregion
//
//        #region Constructors
//
//        private ScreenCapturer()
//        {
//            this.screenCaptureTimer = new Timer();
//            this.screenCaptureTimer.Interval = SCREEN_CAPTURE_INTERVAL;
//            this.screenCaptureTimer.Elapsed += (sender, e) =>
//            {
//                BitmapContainer bitmapContainer = null;
//                lock (this.bitmapLock)
//                {
//                    bitmapContainer = this.CaptureBitmap();
//                }
//
//                this.NotifyScreenChanged(bitmapContainer);
//            };
//        }
//
//        #endregion
//
//        #region Public Methods
//
//        public void Start()
//        {
//            this.screenCaptureTimer.Start();
//        }
//
//        public void Stop()
//        {
//            this.screenCaptureTimer.Stop();
//        }
//
//        #endregion
//    }
//}
