using System.Drawing;
using System.Timers;
using ExtendedDisplay.Framework.Cross;

namespace ExtendedDisplay
{
    public class ScreenCapturer
    {
        #region Constants

        public const int SCREEN_CAPTURE_INTERVAL = 200;

        public const int MOUSE_CAPTURE_INTERVAL = 50;

        #endregion

        #region Singleton Implementation

        public static ScreenCapturer Instance { get; private set; }

        static void Initialize(int screenIndex)
        {
            Instance = new ScreenCapturer(screenIndex);
        }

        static ScreenCapturer()
        {
            Instance = new ScreenCapturer(0);
        }

        #endregion

        #region Private Members

        private readonly int screenIndex;

        private readonly object bitmapLock = new object();

        private readonly Timer screenCaptureTimer;

        private readonly Timer mouseCaptureTimer;

        private Bitmap bitmap;

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

        private void DisposeBitmap()
        {
            if (this.bitmap != null)
            {
                this.bitmap.Dispose();
            }

            this.bitmap = null;
        }

        private BitmapContainer CaptureBitmap()
        {
            //this.DisposeBitmap();
            var oldBitmap = this.bitmap;

            Stopwatch.Measure("CaptureScreen", () => { this.bitmap = WinApiExtensions.CaptureScreen(this.screenIndex, true); });

            //var diffBitmap = Extensions.GetDifferences(oldBitmap, this.bitmap);

            var cursorPosition = WinApiExtensions.GetCursorPosition();

            return new BitmapContainer()
            {
                CursorX = cursorPosition.X,
                CursorY = cursorPosition.Y,
                EncodedBitmap = this.bitmap.ConvertToString()
            };
        }

        private BitmapContainer CaptureMouse()
        {
            var cursorPosition = WinApiExtensions.GetCursorPosition();

            return new BitmapContainer()
            {
                CursorX = cursorPosition.X,
                CursorY = cursorPosition.Y
            };
        }

        #endregion

        #region Constructors

        private ScreenCapturer(int screenIndex)
        {
            this.screenIndex = screenIndex;

            this.screenCaptureTimer = new Timer();
            this.screenCaptureTimer.Interval = SCREEN_CAPTURE_INTERVAL;
            this.screenCaptureTimer.Elapsed += (sender, e) =>
                {
                    BitmapContainer bitmapContainer = null;
                    lock (this.bitmapLock)
                    {
                        bitmapContainer = this.CaptureBitmap();
                    }

                    this.NotifyScreenChanged(bitmapContainer);
                };

            this.mouseCaptureTimer = new Timer();
            this.mouseCaptureTimer.Interval = MOUSE_CAPTURE_INTERVAL;
            this.mouseCaptureTimer.Elapsed += (sender, e) =>
                {
                    var bitmapContainer = this.CaptureMouse();

                    this.NotifyScreenChanged(bitmapContainer);
                };
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            this.screenCaptureTimer.Start();
            //this.mouseCaptureTimer.Start();
        }

        public void Stop()
        {
            this.screenCaptureTimer.Stop();
            //this.mouseCaptureTimer.Stop();
        }

        #endregion
    }
}
