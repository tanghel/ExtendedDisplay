//using System;
//using System.Timers;
//using MonoTouch.UIKit;
//
//namespace ExtendedDisplay.Framework.iOS
//{
//    public class CaptureService
//    {
//        public static CaptureService Instance { get; private set; }
//
//        static CaptureService()
//        {
//            Instance = new CaptureService();
//        }
//
//        private const int CAPTURE_INTERVAL = 1000;
//
//        private readonly Timer timer;
//
//        public Action<UIImage> capturedImageAction;
//
//        private UIImage CaptureImage()
//        {
//            UIWindow keyWindow = null;
//            UIApplication.SharedApplication.InvokeOnMainThread(() => keyWindow = UIApplication.SharedApplication.KeyWindow);
//            if (keyWindow != null)
//            {
//                UIViewController rootViewController = null;
//                UIApplication.SharedApplication.InvokeOnMainThread(() => rootViewController = keyWindow.RootViewController);
//                if (rootViewController != null)
//                {
//                    UIView rootView = null;
//                    UIApplication.SharedApplication.InvokeOnMainThread(() => rootView = rootViewController.View);
//                    if (rootView != null)
//                    {
//                        return rootView.TakeScreenshot();
//                    }
//                }
//            }
//
//            return null;
//        }
//
//        private UIImage CaptureImageFromAsync()
//        {
//            UIImage image = null;
//            UIApplication.SharedApplication.InvokeOnMainThread(() => image = this.CaptureImage());
//
//            return image;
//        }
//
//        private CaptureService()
//        {
//            this.timer = new Timer();
//            this.timer.Interval = CAPTURE_INTERVAL;
//            this.timer.Elapsed += (sender, e) => 
//            {
//                var image = this.CaptureImage();
//                if (image != null && this.capturedImageAction != null)
//                {
//                    this.capturedImageAction.Invoke(image);
//                }
//            };
//        }
//
//        public void Start(Action<UIImage> capturedImageAction)
//        {
//            this.capturedImageAction = capturedImageAction;
//            this.timer.Start();
//        }
//
//        public void Stop()
//        {
//            this.capturedImageAction = null;
//            this.timer.Stop();
//        }
//    }
//}
//
