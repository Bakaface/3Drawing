using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    /// <summary>
    /// Чтение файлов nod и elem, методы генерируют объект класса Solid
    /// </summary>
    static class SolidFileReader
    {
        static public Solid ReadFromFiles(string nodePath, string elemPath)
        {
            SolidBuilder solidBuilder = new SolidBuilder();
            int counter = 1;
            string line;
            using (StreamReader file = new StreamReader(nodePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    int i = 0;
                    double[] coords = new double[3];
                    foreach (var coord in line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries))
                    {
                        coords[i] = Convert.ToDouble(coord);
                        i++;
                    }
                    solidBuilder.AddPoint(counter, coords[0], coords[1], coords[2]);
                    counter++;
                }
            }
            using (StreamReader file = new StreamReader(elemPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    int i = 0;
                    int[] ids = new int[4];
                    foreach (var coord in line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (i < 4)
                            ids[i] = Convert.ToInt32(coord);
                        i++;
                    }
                    solidBuilder.AddPolygon(ids);
                }
            }
            return solidBuilder.Solid;

        }

    }
}
