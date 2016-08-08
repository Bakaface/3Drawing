using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Drawing
{
    class CoordinateSystem : Shape
    {
        public Color Color { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
        public int Multiplier { get; set; }

        public override void Draw(Painter painter)
        {
            throw new NotImplementedException();
        }
    }
}
