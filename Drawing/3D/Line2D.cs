using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Drawing
{
    //линия соеденяет 2 точки, которые представлены в виде айдишников
    class Line2D
    {
        public int BeginId { get; set; }
        public int EndId { get; set; }
        public double Distance { get; set; }
    }
}
