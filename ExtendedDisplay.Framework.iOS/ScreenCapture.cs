using System;
using ExtendedDisplay.Framework.Cross;
using MonoTouch.UIKit;

namespace ExtendedDisplay.Framework.iOS
{
    public class ScreenCapture : IScreenCapture
    {
        public byte[] CaptureScreen()
        {
            UIWindow keyWindow = null;
            UIApplication.SharedApplication.InvokeOnMainThread(() => keyWindow = UIApplication.SharedApplication.KeyWindow);
            if (keyWindow != null)
            {
                UIViewController rootViewController = null;
                UIApplication.SharedApplication.InvokeOnMainThread(() => rootViewController = keyWindow.RootViewController);
                if (rootViewController != null)
                {
                    UIView rootView = null;
                    UIApplication.SharedApplication.InvokeOnMainThread(() => rootView = rootViewController.View);
                    if (rootView != null)
                    {
                        return rootView.TakeScreenshot().ToBytes();
                    }
                }
            }

            return new byte[] {};
        }
    }
}

