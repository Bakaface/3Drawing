using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    abstract class Shape
    {
        abstract public void Draw(Painter painter);
    }
}
