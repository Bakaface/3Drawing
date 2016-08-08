using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    /// <summary>
    /// По всякому пересчитывает координаты фигуры для вращения, перемещенрия и сжатия\растяжения
    /// </summary>
    static class AffineTransformationTools
    {
        static public void Rotate(Solid solid, Axis axis, double angle)
        {
            for (int i = 0; i < solid.Points.Count; i++)
            {
                switch (axis)
                {
                    case Axis.X:
                        solid.Points[i].Y = solid.Points[i].Y * Math.Cos(angle) - solid.Points[i].Z * Math.Sin(angle);
                        solid.Points[i].Z = solid.Points[i].Y * Math.Sin(angle) + solid.Points[i].Z * Math.Cos(angle);
                        break;
                    case Axis.Y:
                        solid.Points[i].X = solid.Points[i].X * Math.Cos(angle) - solid.Points[i].Z * Math.Sin(angle);
                        solid.Points[i].Z = solid.Points[i].X * Math.Sin(angle) + solid.Points[i].Z * Math.Cos(angle);
                        break;
                    case Axis.Z:
                        solid.Points[i].X = solid.Points[i].X * Math.Cos(angle) - solid.Points[i].Y * Math.Sin(angle);
                        solid.Points[i].Y = solid.Points[i].X * Math.Sin(angle) + solid.Points[i].Y * Math.Cos(angle);
                        break;
                    default:
                        break;
                }
            }
        }

        static public void Shift(Solid solid, Axis axis, double shift)
        {
            for (int i = 0; i < solid.Points.Count; i++)
            {
                switch (axis)
                {
                    case Axis.X:
                        solid.Points[i].X += shift;
                        break;
                    case Axis.Y:
                        solid.Points[i].Y += shift;
                        break;
                    case Axis.Z:
                        solid.Points[i].Z += shift;
                        break;
                    default:
                        break;
                }
            }
        }

        static public void Scope(Solid solid, Axis axis, double k)
        {
            for (int i = 0; i < solid.Points.Count; i++)
            {
                switch (axis)
                {
                    case Axis.X:
                        solid.Points[i].X *= k;
                        break;
                    case Axis.Y:
                        solid.Points[i].Y *= k;
                        break;
                    case Axis.Z:
                        solid.Points[i].Z *= k;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
