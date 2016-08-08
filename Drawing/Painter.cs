using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using shapes = System.Windows.Shapes;

namespace Drawing
{
    class Painter
    {
        //static int multiplier = 1;
        //static int shift = 250;

        public Canvas Canvas { get; private set; }

        public double Multiplier { get; set; }
        public Point Shift { get; set; }


        public Painter(Canvas canvas)
        {
            Canvas = canvas;
            //Shift - new Point();
            //Shift = 250;
            //DrawCoordSystem(Colors.Black, 30, 15);

            //UpdateShift();
        }
        
        public void DrawLine(Point begin, Point end, Color color)
        {
            Canvas.Children.Add(new shapes.Line()
            {
                X1 = begin.X * Multiplier + Shift.X,
                X2 = end.X * Multiplier + Shift.X,
                Y1 = begin.Y * Multiplier + Shift.Y,
                Y2 = end.Y * Multiplier + Shift.Y,
                Stroke = new SolidColorBrush(color)
            });
        }


        public void DrawPolygon(Point[] points, Color color, bool isFill)
        {
            Point[] recountedPoints = (Point[])points.Clone();
            for (int i = 0; i < points.Length; i++)
            {
                recountedPoints[i].X *= Multiplier;
                recountedPoints[i].Y *= Multiplier;
                recountedPoints[i].X += Shift.X;
                recountedPoints[i].Y += Shift.Y;
            }
            shapes.Polygon polygon = new shapes.Polygon();
            polygon.Points = new PointCollection(recountedPoints);
            polygon.Stroke = new SolidColorBrush(color);
            if (isFill)
                polygon.Fill = new SolidColorBrush(color);
            Canvas.Children.Add(polygon);
        }

        public void Clear()
        {
            Canvas.Children.Clear();
            //DrawCoordSystem(Colors.Black, 30, 15);
        }
        /*
        public void UpdateShift()
        {
            Shift = new Point(Canvas.Width / 2, Canvas.Height / 2);
        }*/



    }
}
