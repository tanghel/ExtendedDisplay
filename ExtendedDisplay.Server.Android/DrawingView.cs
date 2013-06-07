using System;
using Android.Views;
using Android.Graphics;
using Android.Content;
using System.Collections.Generic;

namespace ExtendedDisplay.Server.Android
{
    public class DrawingView : View
    {
        private readonly List<PointF> points;

        public PointF PointToDraw
        {
            set
            {
                this.points.Add(value);
                this.Invalidate();
            }
        }

        public void Clear()
        {
            this.points.Clear();
            this.Invalidate();
        }

        public DrawingView(Context context) : base(context)
        {
            this.points = new List<PointF>();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            PointF previousPoint = null;
            foreach (var point in points.ToArray())
            {
                if (previousPoint != null)
                {
                    canvas.DrawLine(previousPoint.X, previousPoint.Y, point.X, point.Y, new Paint() { Color = Color.Black, StrokeWidth = 5 });
                }

                previousPoint = point;
//                canvas.DrawCircle(point.X, point.Y, 10, new Paint() { Color = Color.Black });
            }
        }
    }
}

