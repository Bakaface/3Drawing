using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Drawing
{
    class Polygon : Shape
    {
        public Color Color { get; set; }
        public Point[] Points { get; set; }
        public bool IsFill { get; set; }

        public override void Draw(Painter painter)
        {
            painter.DrawPolygon(Points, Color, IsFill);
        }
    }
}
