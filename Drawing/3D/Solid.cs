using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Drawing
{
    enum ProjectionType
    {
        Axonometric,
        Prespective
    }

    /// <summary>
    /// Представляет собой геометрическое тело. В зависимости от метода отрисовки (линияни либо полигонами) методами отрисовки юзается либо массив Lines, либо Polygons.
    /// </summary>
    class Solid : Shape3D
    {
        //Координаты 3д
        public List<Point3D> Points { get; set; }
        //Проекционные координаты (для рассчета цветов полигонов)
        public List<Point3D> ProjectionPoints { get; set; }

        public List<Line2D> Lines { get; set; }
        public List<Polygon2D> Polygons { get; set; }
        public Dictionary<int, double> Distances { get; set; }

        //Углы наклона системы координат
        public double Alpha { get; set; }
        public double Beta { get; set; }

        public Point3D LightingPoint { get; set; }
        public Point3D PerspectivePoint { get; set; }


        public ProjectionType ProectionType { get; set; }

        public Solid()
        {
            Points = new List<Point3D>();
            Lines = new List<Line2D>();
            Polygons = new List<Polygon2D>();
            Distances = new Dictionary<int, double>();
            ProjectionPoints = new List<Point3D>();
            PerspectivePoint = new Point3D();
            LightingPoint = new Point3D()
            {
                X = 0,
                Y = 0,
                Z = 50
            };
        }

        double DegreesToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        
        //Пересчет координат в экранные
        IEnumerable<Point2D> GetAxonometricProjection(double alpha, double beta)
        {
            foreach (var point in Points)
            {
                yield return new Point2D()
                {
                    Id = point.Id,
                    X = point.X * Math.Cos(alpha) - point.Y * Math.Sin(alpha),
                    Y = point.X * Math.Sin(alpha) * Math.Cos(beta) + point.Y * Math.Cos(alpha) * Math.Cos(beta) - point.Z * Math.Sin(beta),
                };
            }
        }

        //Пересчет координат в экранные в перспективе
        IEnumerable<Point2D> GetPrespectiveProjection(double alpha, double beta, double k)
        {
            Distances.Clear();
            ProjectionPoints.Clear();
            foreach (var point in Points)
            {
                double x = point.X * Math.Cos(alpha) - point.Y * Math.Sin(alpha);
                double y = point.X * Math.Sin(alpha) * Math.Cos(beta) + point.Y * Math.Cos(alpha) * Math.Cos(beta) - point.Z * Math.Sin(beta);
                double z = point.X * Math.Sin(alpha) * Math.Sin(beta) + point.Y * Math.Cos(alpha) * Math.Sin(beta) + point.Z * Math.Cos(beta);
                //point.Distance = k - z;
                //Distances.Add(point.Id, k - z);
                ProjectionPoints.Add(new Point3D()
                {
                    X = x,
                    Y = y,
                    Z = z
                });
                yield return new Point2D()
                {
                    Id = point.Id,
                    X = z + k < Math.Pow(1, -100) ? k * x / Math.Pow(1, -100) : k * x / (z + k),
                    Y = z + k < Math.Pow(1, -100) ? k * y / Math.Pow(1, -100) : k * y / (z + k),
                };
            }
        }
        //Отрисовка линиями
        public override void DrawLines(Painter painter)
        {
            List<Point2D> screenPoints = null;
            switch (ProectionType)
            {
                case ProjectionType.Axonometric:
                    screenPoints = GetAxonometricProjection(DegreesToRadians(Alpha), DegreesToRadians(Beta)).ToList();
                    break;
                case ProjectionType.Prespective:
                    screenPoints = GetPrespectiveProjection(DegreesToRadians(Alpha), DegreesToRadians(Beta), 10).ToList();
                    break;
                default:
                    break;
            }
            //screenPoints = GetAxonometricProjection(DegreesToRadians(Alpha), DegreesToRadians(Beta)).ToList();
            foreach (var line in Lines)
            {
                painter.DrawLine(screenPoints.FindLast(x => x.Id == line.BeginId).ToPoint(),
                    screenPoints.FindLast(x => x.Id == line.EndId).ToPoint(),
                    Colors.Blue);
            }
        }

        //Нахождение косинуса угла между вектором нормали и лучом света (по трем вершинам)
        int GetCos(Point3D p1, Point3D p2, Point3D p3)
        {
            Point3D prA = new Point3D()
            {
                X = p1.X - p2.X,
                Y = p1.Y - p2.Y,
                Z = p1.Z - p2.Z
            };
            Point3D prB = new Point3D()
            {
                X = p3.X - p2.X,
                Y = p3.Y - p2.Y,
                Z = p3.Z - p2.Z
            };
            Point3D prC = new Point3D()
            {
                X = prA.Y * prB.Z - prA.Z * prB.Y,
                Y = prB.X * prA.Z - prA.X * prB.Z,
                Z = prA.X * prB.Y - prB.X * prA.Y,
            };
            /*
            Point3D lightingSource = new Point3D()
            {
                X = 0,
                Y = 0,
                Z = -300
            };*/
            double cd = prC.X * LightingPoint.X + prC.Y * LightingPoint.Y + prC.Z * LightingPoint.Z;
            double lc = Math.Sqrt(prC.X * prC.X + prC.Y * prC.Y + prC.Z * prC.Z);
            double ld = Math.Sqrt(LightingPoint.X * LightingPoint.X + LightingPoint.Y * LightingPoint.Y + LightingPoint.Z * LightingPoint.Z);
            double cos = (cd / (lc * ld));
            int max = 255;
            int min = 0;
            if (cos > 0)
            {
                return Convert.ToInt32(min + (max - min) * cos);
            }
            else
            {
                return min;
            }
        }

        //Отрисовка полигонами
        public override void DrawPolygons(Painter painter)
        {
            List<Point2D> screenPoints = null;
            switch (ProectionType)
            {
                case ProjectionType.Axonometric:
                    screenPoints = GetAxonometricProjection(DegreesToRadians(Alpha), DegreesToRadians(Beta)).ToList();
                    break;
                case ProjectionType.Prespective:
                    screenPoints = GetPrespectiveProjection(DegreesToRadians(Alpha), DegreesToRadians(Beta), 10).ToList();
                    break;
                default:
                    break;
            }
            screenPoints.Add(new Point2D());
            Points.Add(new Point3D() { Distance = 0 });
            foreach (var polygon in Polygons)
            {
                List<int> ids = polygon.PointsId.ToList();
                polygon.Distance = (Points[ids[0]].Distance + Points[ids[1]].Distance + Points[ids[2]].Distance + Points[ids[3]].Distance) / 4;
            }
            
            if (ProectionType == ProjectionType.Prespective)
            {
                Polygons.Sort((x, y) => x.Distance.CompareTo(y.Distance));
                //Polygons.Sort((x, y) => Points[x.PointsId.First()].Distance.CompareTo(Points[y.PointsId.First()].Distance));
                //Polygons.Sort((x, y) => Points.FindLast(point => point.Id == x.PointsId.First()).Distance.CompareTo(Points.FindLast(point => point.Id == y.PointsId.First()).Distance));
            }
            foreach (var polygon in Polygons)
            {
                List<Point2D> points = new List<Point2D>();
                foreach (var id in polygon.PointsId)
                {
                    points.Add(screenPoints[id]);
                }
                //IEnumerable<Point2D> points1 = from pointId in polygon.PointsId select screenPoints.FindLast(x => x.Id == pointId);
                

                bool isFill = false;
                Color color = Colors.Black;
                if (ProectionType == ProjectionType.Prespective)
                {
                    //byte col = (byte)(255 * (Distances[polygon.PointsId.First()] * 100 / maxDistance) / 100);
                    List<int> polygonPointsId = polygon.PointsId.ToList();
                    byte col = (byte)GetCos(ProjectionPoints[polygonPointsId[0]], ProjectionPoints[polygonPointsId[1]], ProjectionPoints[polygonPointsId[2]]);
                    isFill = true;
                    color = Color.FromRgb(col, col, 20);
                }

                painter.DrawPolygon((from point in points select new Point(point.X, point.Y)).ToArray(), color, isFill);
            }
        }
    }
}
