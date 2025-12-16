using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;


namespace Surface3DHelix
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BuildSurface(1.0, 1.0);
        }

        private void BuildClick(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ATextBox.Text, out double a) || Math.Abs(a) < double.Epsilon)
            {
                MessageBox.Show("Введите ненулевое число a.");
                return;
            }

            if (!double.TryParse(CTextBox.Text, out double c))
            {
                MessageBox.Show("Введите число c.");
                return;
            }

            BuildSurface(a, c);
        }

        private void BuildSurface(double a, double c)
        {
            const int gridSize = 50;
            const double min = -5.0;
            const double max = 5.0;
            double step = (max - min) / (gridSize - 1);

            var mesh = new MeshGeometry3D();

            // вершины
            for (int iy = 0; iy < gridSize; iy++)
            {
                double y = min + iy * step;
                for (int ix = 0; ix < gridSize; ix++)
                {
                    double x = min + ix * step;
                    double z = (c / a) * (x * x + y * y);

                    mesh.Positions.Add(new Point3D(x, y, z));
                    mesh.Normals.Add(new Vector3D(0, 0, 1));
                    mesh.TextureCoordinates.Add(
                        new System.Windows.Point(
                            (double)ix / (gridSize - 1),
                            (double)iy / (gridSize - 1)));
                }
            }

            // треугольники
            for (int iy = 0; iy < gridSize - 1; iy++)
            {
                for (int ix = 0; ix < gridSize - 1; ix++)
                {
                    int i0 = iy * gridSize + ix;
                    int i1 = iy * gridSize + ix + 1;
                    int i2 = (iy + 1) * gridSize + ix;
                    int i3 = (iy + 1) * gridSize + ix + 1;

                    mesh.TriangleIndices.Add(i0);
                    mesh.TriangleIndices.Add(i2);
                    mesh.TriangleIndices.Add(i1);

                    mesh.TriangleIndices.Add(i1);
                    mesh.TriangleIndices.Add(i2);
                    mesh.TriangleIndices.Add(i3);
                }
            }

            var material = MaterialHelper.CreateMaterial(Colors.LightSkyBlue);
            var model = new GeometryModel3D
            {
                Geometry = mesh,
                Material = material,
                BackMaterial = material
            };

            var surfaceVisual = new ModelVisual3D { Content = model };

            HelixView.Children.Clear();
            HelixView.Children.Add(new DefaultLights());
            HelixView.Children.Add(surfaceVisual);
            HelixView.ZoomExtents();
        }
    }
}
