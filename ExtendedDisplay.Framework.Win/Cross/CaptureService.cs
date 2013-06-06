using System;
using System.Timers;

namespace ExtendedDisplay.Framework.Cross
{
    public class CaptureService
    {
        #region Singleton Implementation

        public static CaptureService Instance { get; private set; }

        public static void Initialize(IScreenCapture screenCapture, int timerInterval)
        {
            Instance = new CaptureService(screenCapture, timerInterval);
        }

        #endregion

        #region Private Members

        private readonly object bitmapLock = new object();

        private readonly Timer screenCaptureTimer;

        private readonly IScreenCapture screenCapture;

        #endregion

        #region Public Events

        public event GenericDataEventHandler<BitmapContainer> ScreenCaptured;

        #endregion

        #region Private Methods

        private void NotifyScreenChanged(BitmapContainer bitmapContainer)
        {
            if (this.ScreenCaptured != null)
            {
                this.ScreenCaptured(this, new GenericDataEventArgs<BitmapContainer>(bitmapContainer));
            }
        }

        private BitmapContainer CaptureBitmap()
        {
            lock (this.bitmapLock)
            {
                var fileBytes = screenCapture.CaptureScreen();
                var fileString = Convert.ToBase64String(fileBytes);

                return new BitmapContainer()
                {
                    EncodedBitmap = fileString
                };
            }
        }

        #endregion

        #region Constructors

        private CaptureService(IScreenCapture screenCapture, int timerInterval)
        {
            this.screenCapture = screenCapture;

            this.screenCaptureTimer = new Timer();
            this.screenCaptureTimer.Interval = timerInterval;
            this.screenCaptureTimer.Elapsed += (sender, e) =>
            {
                BitmapContainer bitmapContainer = null;
                lock (this.bitmapLock)
                {
                    bitmapContainer = this.CaptureBitmap();
                }

                this.NotifyScreenChanged(bitmapContainer);
            };
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            this.screenCaptureTimer.Start();
        }

        public void Stop()
        {
            this.screenCaptureTimer.Stop();
        }

        #endregion
    }
}

