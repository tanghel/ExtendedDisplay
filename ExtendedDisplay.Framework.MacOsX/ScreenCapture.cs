using System;
using System.Diagnostics;
using System.IO;
using ExtendedDisplay.Framework.Cross;

namespace ExtendedDisplay.Client.MacOsX
{
    public class ScreenCapture : IScreenCapture
    {
        private const int MAX_NUMBER_OF_IMAGES = 10;

        private int index = 0;

        public byte[] CaptureScreen()
        {
            var currentIndex = this.index++ % MAX_NUMBER_OF_IMAGES;
            var fileName = string.Format("screenshot{0}.jpg", currentIndex);
            Process.Start("screencapture", string.Format("-C -x -t jpg {0}", fileName)).WaitForExit();

            return File.ReadAllBytes(fileName);
        }
    }
}

