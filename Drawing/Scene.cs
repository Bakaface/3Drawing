using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    enum SceneDrawMode
    {
        Lines,
        Polygons,
        Detail1,
        Detail2,
        Detail3
    }

    class Scene
    {
        public Painter Painter { get; set; }
        public List<Shape3D> Shapes { get; set; }

        SceneDrawMode drawMode;
        public SceneDrawMode DrawMode
        {
            get { return drawMode; }
            set
            {
                drawMode = value;
                switch (value)
                {
                    case SceneDrawMode.Lines:
                        Painter.Shift = new System.Windows.Point(250, 200);
                        break;
                    case SceneDrawMode.Polygons:
                        Painter.Shift = new System.Windows.Point(250, 200);
                        break;
                    case SceneDrawMode.Detail1:
                        Painter.Shift = new System.Windows.Point(300, 300);
                        break;
                    case SceneDrawMode.Detail2:
                        Painter.Shift = new System.Windows.Point(800, 300);
                        break;
                    case SceneDrawMode.Detail3:
                        Painter.Shift = new System.Windows.Point(300, 300);
                        break;
                }
            }
        }

        public Scene (Painter painter)
        {
            Painter = painter;
            Shapes = new List<Shape3D>();
            DrawMode = SceneDrawMode.Lines;
        }

        public void Refresh()
        {
            Painter.Clear();
            foreach (var shape in Shapes)
            {
                switch (DrawMode)
                {
                    case SceneDrawMode.Lines:
                        shape.DrawLines(Painter);
                        break;
                    case SceneDrawMode.Polygons:
                        shape.DrawPolygons(Painter);
                        break;
                    case SceneDrawMode.Detail1:
                        shape.DrawPolygons(Painter);
                        break;
                    case SceneDrawMode.Detail2:
                        shape.DrawPolygons(Painter);
                        break;
                    case SceneDrawMode.Detail3:
                        shape.DrawPolygons(Painter);
                        break;
                }
            }
        }
    }
}
