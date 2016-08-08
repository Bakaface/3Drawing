using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{

    /// <summary>
    /// Класс для упрощения построения фигуры (см., к примеру, метод GetPrism() класса MainWindow)
    /// </summary>
    class SolidBuilder
    {
        public Solid Solid { get; private set; }

        public SolidBuilder()
        {
            Solid = new Solid();
        }
        public SolidBuilder AddPoint(int id, double x, double y, double z)
        {
            Solid.Points.Add(new Point3D()
            {
                Id = id,
                X = x,
                Y = y,
                Z = z
            });
            return this;
        }

        public SolidBuilder AddLine(int beginId, int endId)
        {
            Solid.Lines.Add(new Line2D() {
                BeginId = beginId,
                EndId = endId
            });
            return this;
        }

        public SolidBuilder AddPolygon(params int[] pointsId)
        {
            Solid.Polygons.Add(new Polygon2D() { PointsId = pointsId });
            return this;
        }

        public SolidBuilder SetAlpha(double alpha)
        {
            Solid.Alpha = alpha;
            return this;
        }

        public SolidBuilder SetBeta(double beta)
        {
            Solid.Beta = beta;
            return this;
        }


    }
}
