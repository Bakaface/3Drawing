using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    static class SolidCollection
    {
        public static Solid HyperbolicParabaloid(double a, double b, double fromX, double fromY, double toX, double toY, double step)
        {
            SolidBuilder solidBuilder = new SolidBuilder();
            int idCounter = 0;
            double shiftX = fromX < 0 ? Math.Abs(fromX) : 0;
            double shiftY = fromY < 0 ? Math.Abs(fromY) : 0;

            for (double x = fromX; x <= toX; x += step)
            {
                for (double y = fromY; y <= toY; y += step)
                {
                    double z = (y * y) / (a * a) - (x * x) / (b * b);
                    solidBuilder.AddPoint(idCounter, x, y, z);
                    idCounter++;
                }
            }
            idCounter = 0;
            int length = 0;
            for (double i = fromX; i <= toX; i+= step)
                length++;

            for (double x = fromX; x <= toX - 1; x += step)
            {
                for (double y = fromY; y <= toY - 1; y += step)
                {
                    solidBuilder.AddPolygon(
                        idCounter,
                        idCounter + 1,
                        idCounter + length + 1,
                        idCounter + length);
                    idCounter++;
                }
                idCounter++;
            }
            return solidBuilder.Solid;
        }

    }
}
