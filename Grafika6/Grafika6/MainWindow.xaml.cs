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

namespace Grafika6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Point P1 = new Point(10, 300);
            //Point P2 = new Point(10, 50);
            //Point P3 = new Point(400, 50);
            //Point P4 = new Point(250, 300);
            points = new List<Point>();
        }
        public List<Point> points;

        private void Draw_curve(object sender, EventArgs e)
        {
            Bezier(points);
        }

        private void CanvasLBD(object sender, MouseButtonEventArgs e)
        {
            Point point = new Point();
            point = Mouse.GetPosition(Image_placeholder);
            points.Add(point);
            Image_placeholder.Children.Clear();
            if (points.Count > 2) Bezier(points);
        }

        private void Bezier(List<Point> pointsList)
        {
            double t = 0.0;
            List<Point> points_bufor = new List<Point>();
            List<Point> points_bufor1 = new List<Point>();

            for (int i = 0; i < 2000; i++)
            {
                t += 0.0005;
                points_bufor1 = pointsList.ToList();
                while (points_bufor1.Count > 1)
                {
                    points_bufor = new List<Point>();
                    for (int j = 0; j < points_bufor1.Count - 1; j++)
                    {
                        Point point = new Point();
                        point.X = (1 - t) * points_bufor1[j].X + t * points_bufor1[j + 1].X;
                        point.Y = (1 - t) * points_bufor1[j].Y + t * points_bufor1[j + 1].Y;
                        points_bufor.Add(point);
                    }
                    points_bufor1 = points_bufor.ToList();
                }
                Rectangle rec = new Rectangle
                {
                    Fill = Brushes.Red,
                    Width = 2,
                    Height = 2
                };

                Canvas.SetLeft(rec, points_bufor1[0].X);
                Canvas.SetTop(rec, points_bufor1[0].Y);
                Image_placeholder.Children.Add(rec);
            }
        }

        private void AddPoint(object sender, EventArgs e)
        {
            if(int.TryParse(XTextBox.Text, out int x) && int.TryParse(XTextBox.Text, out int y))
            {
                Point point = new Point();
                point.X = Convert.ToInt32(XTextBox.Text);
                point.Y = Convert.ToInt32(YTextBox.Text);
                points.Add(point);
                Image_placeholder.Children.Clear();
                if (points.Count > 2) Bezier(points);
            }
        }
    }
}
