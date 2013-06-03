using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendedDisplay.Framework.Cross
{
    public class BitmapContainer
    {
        public int CursorX { get; set; }

        public int CursorY { get; set; }

        public string EncodedBitmap { get; set; }

        public DateTime Created { get; set; }
    }
}
