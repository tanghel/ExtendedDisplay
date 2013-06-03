using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedDisplay
{
    [StructLayout(LayoutKind.Sequential)]
    struct CURSORINFO
    {
        public Int32 cbSize;
        public Int32 flags;
        public IntPtr hCursor;
        public POINTAPI ptScreenPos;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINTAPI
    {
        public int x;
        public int y;
    }

    public static class WinApiExtensions
    {

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        const Int32 CURSOR_SHOWING = 0x00000001;

        public static Bitmap CaptureScreen(int screenIndex, bool CaptureMouse)
        {
            var screens = Screen.AllScreens;
            if (screenIndex < screens.Length)
            {
                var secondaryScreen = screens[screenIndex];
                Bitmap bitmap = new Bitmap(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

                try
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Stopwatch.Measure("CopyFromScreen", () => { graphics.CopyFromScreen(secondaryScreen.Bounds.Location, Point.Empty, secondaryScreen.Bounds.Size); });

                        if (CaptureMouse)
                        {
                            CURSORINFO pci;
                            pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                            if (GetCursorInfo(out pci))
                            {
                                if (pci.flags == CURSOR_SHOWING)
                                {
                                    DrawIcon(graphics.GetHdc(), pci.ptScreenPos.x - secondaryScreen.Bounds.Location.X, pci.ptScreenPos.y - secondaryScreen.Bounds.Location.Y, pci.hCursor);
                                    graphics.ReleaseHdc();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }

                return bitmap;
            }

            return null;
        }

        public static Point GetCursorPosition()
        {
            var screens = Screen.AllScreens;
            if (screens.Length >= 2)
            {
                var secondaryScreen = screens[1];

                CURSORINFO cursorInfo;
                cursorInfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                if (GetCursorInfo(out cursorInfo))
                {
                    return new Point(cursorInfo.ptScreenPos.x - secondaryScreen.Bounds.Location.X, cursorInfo.ptScreenPos.y - secondaryScreen.Bounds.Location.Y);
                }
            }

            return Point.Empty;
        }
    }
}
