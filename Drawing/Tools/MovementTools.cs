using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    static class MovementTools
    {
        static public void MoveOnX(Solid solid, double step)
        {
            for (int i = 0; i < solid.Points.Count; i++)
            {
                solid.Points[i].X += step;
            }
        }


    }
}
