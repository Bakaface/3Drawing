using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Drawing
{
    class Point2D: PointBase
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }
    }
}
