using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;
using System.Collections.Generic;

namespace ExtendedDisplay.Server.iOS
{
    public class DrawingView : UIView
    {
        private readonly List<PointF> pointsToDraw;

        public DrawingView()
        {
            this.pointsToDraw = new List<PointF>();
        }

        public void Clear()
        {
            this.pointsToDraw.Clear();
            this.SetNeedsDisplay();
        }

        public PointF PointToDraw 
        { 
            set
            {
                this.pointsToDraw.Add(value);
                this.SetNeedsDisplay();
            }
        }

        public override void Draw(System.Drawing.RectangleF rect)
        {
            base.Draw(rect);

            var context = UIGraphics.GetCurrentContext();

            foreach (var point in this.pointsToDraw)
            {
                context.FillEllipseInRect(new RectangleF(point, new SizeF(20, 20)));
            }
        }
    }
}

