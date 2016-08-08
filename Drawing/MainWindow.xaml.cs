using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Painter painter;
        Scene scene;

        Solid solid;

        TimeWatcher timeWatcher;

        public MainWindow()
        {
            InitializeComponent();
            //Solid par = GetHyperbolicParabaloid();


        }

        void InitControls()
        {
        }

        private void btnShift_Click(object sender, RoutedEventArgs e)
        {
            //AffineTranslator2D.Rotate(polygon, 0.1);
            scene.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            painter = new Painter(canvas);
            //painter.Multiplier = 10;
            scene = new Scene(painter);
            solid = SolidCollection.HyperbolicParabaloid(2, 2, -10, -10, 10, 10, 1);//SolidCollection.HyperbolicParabaloid(2, 2, -10, -10, 10, 10, 1);//GetPrism();//SolidFileReader.ReadFromFiles("details\\nap.node", "details\\nap.elem");//
            scene.Shapes.Add(solid);
            /*
            Point[] points = new Point[4];
            points[0] = new Point(3, 3);
            points[1] = new Point(5, 3);
            points[2] = new Point(7, 7);
            points[3] = new Point(2, 7);
            polygon.Points = points;
            polygon.Color = Colors.Blue;
            polygon.IsFill = false;
            scene.Shapes.Add(polygon);
            */

            timeWatcher = new TimeWatcher(solid, scene);

            comboBoxAxis.Items.Add(new ContentControl() { Content = Axis.X, Tag = Axis.X });
            comboBoxAxis.Items.Add(new ContentControl() { Content = Axis.Y, Tag = Axis.Y });
            comboBoxAxis.Items.Add(new ContentControl() { Content = Axis.Z, Tag = Axis.Z });
            comboBoxAxis.SelectedIndex = 0;

            comboBoxDirection.Items.Add(new ContentControl() { Content = Direction.Forward, Tag = Direction.Forward });
            comboBoxDirection.Items.Add(new ContentControl() { Content = Direction.Backward, Tag = Direction.Backward });
            comboBoxDirection.SelectedIndex = 0;

            comboBoxProjectionType.Items.Add(new ContentControl() { Content = ProjectionType.Axonometric, Tag = ProjectionType.Axonometric });
            comboBoxProjectionType.Items.Add(new ContentControl() { Content = ProjectionType.Prespective, Tag = ProjectionType.Prespective });
            comboBoxProjectionType.SelectedIndex = 0;

            comboBoxMode.Items.Add(new ContentControl() { Content = Mode.Prism, Tag = Mode.Prism });
            comboBoxMode.Items.Add(new ContentControl() { Content = Mode.Paraboloid, Tag = Mode.Paraboloid });
            comboBoxMode.Items.Add(new ContentControl() { Content = "Detail 1", Tag = Mode.Detail1 });
            comboBoxMode.Items.Add(new ContentControl() { Content = "Detail 2", Tag = Mode.Detail2 });
            comboBoxMode.Items.Add(new ContentControl() { Content = "Detail 3", Tag = Mode.Detail3 });
            comboBoxMode.SelectedIndex = 0;
            
            scene.Refresh();
        }


        Solid GetCube()
        {
            SolidBuilder solidBuilder = new SolidBuilder();
            return solidBuilder.
                AddPoint(1, 4, 0, 3).
                AddPoint(2, 4, 0, 1).
                AddPoint(3, 7, 0, 1).
                AddPoint(4, 7, 0, 3).
                AddPoint(5, 4, 1, 3).
                AddPoint(6, 4, 1, 1).
                AddPoint(7, 7, 1, 1).
                AddPoint(8, 7, 1, 3).
                AddLine(1, 2).
                AddLine(2, 3).
                AddLine(3, 4).
                AddLine(4, 1).
                AddLine(5, 6).
                AddLine(6, 7).
                AddLine(7, 8).
                AddLine(8, 5).
                AddLine(1, 5).
                AddLine(2, 6).
                AddLine(3, 7).
                AddLine(4, 8).
                SetAlpha(30).
                SetBeta(30).
                Solid;
        }

        Solid GetPrism()
        {
            SolidBuilder solidBuilder = new SolidBuilder();
            for (int i = 0; i < 5; i++)
            {
                double x = 2 * Math.Cos((2 * Math.PI * i) / 5);
                double z = 2 * Math.Sin((2 * Math.PI * i) / 5);
                solidBuilder.AddPoint(i, x, -1, z);
            }
            for (int i = 0; i < 5; i++)
            {
                double x = 2 * Math.Cos((2 * Math.PI * i) / 5);
                double z = 2 * Math.Sin((2 * Math.PI * i) / 5);
                solidBuilder.AddPoint(i + 5, x, 1, z);
            }
            return solidBuilder.
                AddLine(0, 1).
                AddLine(1, 2).
                AddLine(2, 3).
                AddLine(3, 4).
                AddLine(4, 0).
                AddLine(5, 6).
                AddLine(6, 7).
                AddLine(7, 8).
                AddLine(8, 9).
                AddLine(9, 5).
                AddLine(0, 5).
                AddLine(1, 6).
                AddLine(2, 7).
                AddLine(3, 8).
                AddLine(4, 9).
                SetAlpha(30).
                SetBeta(30).
                Solid;
        }

        private void buttonRotatePlus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Rotate(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, 0.1);
            scene.Refresh();
        }

        private void buttonRotateMinus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Rotate(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, -0.1);
            scene.Refresh();
        }
        
        private void buttonShiftPlus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Shift(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, 0.1);
            scene.Refresh();
        }

        private void buttonShiftMinus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Shift(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, -0.1);
            scene.Refresh();
        }

        private void buttonScopePlus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Scope(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, 0.9);
            scene.Refresh();
        }

        private void buttonScopeMinus_Click(object sender, RoutedEventArgs e)
        {
            AffineTransformationTools.Scope(solid, (Axis)(comboBoxAxis.SelectedItem as ContentControl).Tag, 1.1);
            scene.Refresh();
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            scene.Shapes.Clear();
            solid = GetPrism();
            scene.Shapes.Add(solid);
            scene.Refresh();
        }

        private void buttonMovingStart_Click(object sender, RoutedEventArgs e)
        {
            timeWatcher.Direction = (Direction)(comboBoxDirection.SelectedItem as ContentControl).Tag;
            timeWatcher.Start();
        }

        private void buttonMovingStop_Click(object sender, RoutedEventArgs e)
        {
            timeWatcher.Stop();
        }

        private void sliderAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (doubleUpDownAlpha != null && sliderAlpha != null && scene != null)
            {
                doubleUpDownAlpha.Value = sliderAlpha.Value;
                solid.Alpha = sliderAlpha.Value;
                scene.Refresh();
            }
        }

        private void doubleUpDownAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (doubleUpDownAlpha != null && sliderAlpha != null && scene != null)
            {
                sliderAlpha.Value = (double)doubleUpDownAlpha.Value;
                solid.Alpha = (double)doubleUpDownAlpha.Value;
                scene.Refresh();
            }
        }

        private void sliderBeta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (doubleUpDownBeta != null && sliderBeta != null && scene != null)
            {
                doubleUpDownBeta.Value = sliderBeta.Value;
                solid.Beta = sliderBeta.Value;
                scene.Refresh();
            }
        }

        private void doubleUpDownBeta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (doubleUpDownBeta != null && sliderBeta != null && scene != null)
            {
                sliderBeta.Value = (double)doubleUpDownBeta.Value;
                solid.Beta = (double)doubleUpDownBeta.Value;
                scene.Refresh();
            }
        }

        private void comboBoxProjectionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            solid.ProectionType = (ProjectionType)(e.AddedItems[0] as ContentControl).Tag;
            scene.Refresh();
        }

        private void doubleUpDownMulitplier_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (doubleUpDownMulitplier != null && painter != null)
            {
                painter.Multiplier = (double)doubleUpDownMulitplier.Value;
                scene.Refresh();
            }
        }

        private void comboBoxMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((Mode)(comboBoxMode.SelectedItem as ContentControl).Tag)
            {
                case Mode.Prism:
                    scene.DrawMode = SceneDrawMode.Lines;
                    scene.Shapes.Clear();
                    solid = GetPrism();
                    scene.Shapes.Add(solid);
                    doubleUpDownMulitplier.Value = 40;
                    scene.Painter.Multiplier = 40;
                    scene.Refresh();
                    break;
                case Mode.Paraboloid:
                    scene.DrawMode = SceneDrawMode.Polygons;
                    scene.Shapes.Clear();
                    solid = SolidCollection.HyperbolicParabaloid(2, 2, -10, -10, 10, 10, 1);
                    scene.Shapes.Add(solid);
                    doubleUpDownMulitplier.Value = 10;
                    scene.Painter.Multiplier = 10;
                    scene.Refresh();
                    break;
                case Mode.Detail1:
                    scene.DrawMode = SceneDrawMode.Detail1;
                    scene.Shapes.Clear();
                    solid = SolidFileReader.ReadFromFiles("details\\nap.node", "details\\nap.elem");
                    scene.Shapes.Add(solid);
                    doubleUpDownMulitplier.Value = 3;
                    scene.Painter.Multiplier = 3;
                    scene.Refresh();
                    break;
                case Mode.Detail2:
                    scene.DrawMode = SceneDrawMode.Detail2;
                    scene.Shapes.Clear();
                    solid = SolidFileReader.ReadFromFiles("details\\cl.node", "details\\cl.elem");
                    scene.Shapes.Add(solid);
                    doubleUpDownMulitplier.Value = 3;
                    scene.Painter.Multiplier = 3;
                    scene.Refresh();
                    break;
                case Mode.Detail3:
                    scene.DrawMode = SceneDrawMode.Detail3;
                    scene.Shapes.Clear();
                    solid = SolidFileReader.ReadFromFiles("details\\st.node", "details\\st.elem");
                    scene.Shapes.Add(solid);
                    doubleUpDownMulitplier.Value = 500;
                    scene.Painter.Multiplier = 500;
                    scene.Refresh();
                    break;
            }
            timeWatcher.Solid = solid;
        }

        private void integerUpDownLightingX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (scene != null)
            {
                (scene.Shapes[0] as Solid).LightingPoint.X = (int)integerUpDownLightingX.Value;
                scene.Refresh();
            }
        }

        private void integerUpDownLightingY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (scene != null)
            {
                (scene.Shapes[0] as Solid).LightingPoint.Y = (int)integerUpDownLightingY.Value;
                scene.Refresh();
            }
        }

        private void integerUpDownLightingZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (scene != null)
            {
                (scene.Shapes[0] as Solid).LightingPoint.Z = (int)integerUpDownLightingZ.Value;
                scene.Refresh();
            }
        }
    }
}
