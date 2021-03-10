using ObjParser.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laser_path
{
    class Convex_hull
    {
        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1;
        private System.Drawing.Pen pen2;
        private MyPoint[] points;
        private const uint POINTS = 100;
        private int counter;
        private int small_points = 0;
        private bool pnpoly(MyPoint[] polygon, MyPoint testpoint)
        {
            bool c = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].Point.Y >= testpoint.Point.Y) != (polygon[j].Point.Y >= testpoint.Point.Y)) &&
                    (testpoint.Point.X < (polygon[j].Point.X - polygon[i].Point.X) * (testpoint.Point.Y - polygon[i].Point.Y) /
                    (polygon[j].Point.Y - polygon[i].Point.Y) + polygon[i].Point.X))
                    c = !c;
            }

            return c;
        }

        private Point findCenter()
        {
            Point temp = new Point(0, 0);
            for (int i = 0; i < counter; i++)
            {
                temp.X += points[i].Point.X;
                temp.Y += points[i].Point.Y;
            }
            temp.X /= counter;
            temp.Y /= counter;

            return temp;
        }
   

        private List<PointF> ConvexHull(List<PointF> points)
        {
            if (points.Count < 3)
            {
                //string messageBoxText = "Too small amount of points! Please increase number of points.";
                //string caption = "Warning";
                //MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                small_points++;
                return null;
            }

            List<PointF> hull = new List<PointF>();

            // get leftmost point
            PointF vPointOnHull = points.Where(p => p.X == points.Min(min => min.X)).First();

            PointF vEndpoint;
            do
            {
                hull.Add(vPointOnHull);
                vEndpoint = points[0];

                for (int i = 1; i < points.Count; i++)
                {
                    if ((vPointOnHull == vEndpoint)
                        || (Orientation(vPointOnHull, vEndpoint, points[i]) == -1))
                    {
                        vEndpoint = points[i];
                    }
                }

                vPointOnHull = vEndpoint;

            }
            while (vEndpoint != hull[0]);

            return hull;
        }

        private static int Orientation(PointF p1, PointF p2, PointF p)
        {
            // Determinant
            float Orin = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

            if (Orin > 0)
                return -1; //          (* Orientaion is to the left-hand side  *)
            if (Orin < 0)
                return 1; // (* Orientaion is to the right-hand side *)

            return 0; //  (* Orientaion is neutral aka collinear  *)
        }
        private void grahamButtonX_Click()
        {
            Point center = findCenter();
            MyPoint.Center = center;
            for (int i = 0; i < counter; i++)
                points[i].setAngle();
            QS.grahamPointSort(points, counter, true);
            g.DrawEllipse(pen1, center.X - 3, center.Y - 3, 6, 6);
            List<MyPoint> pointList = new List<MyPoint>(counter);
            for (int i = 0; i < counter; i++)
                pointList.Add(points[i]);

            MyPoint p1, p2, p3;
            MyPoint[] triangle = new MyPoint[3];
            triangle[0] = new MyPoint(center.X, center.Y);
            int size = pointList.Capacity;
            int lowest = pointList.FindIndex(0, pointList.Count, (p => p.Point.X == pointList.Min(min => min.Point.X)));
            for (int i = 0; i < size; i++)
            {
                p1 = pointList.ElementAt((lowest + i) % size);
                p2 = pointList.ElementAt((lowest + i + 1) % size);
                p3 = pointList.ElementAt((lowest + i + 2) % size);

                triangle[1] = p1;
                triangle[2] = p3;
                if (pnpoly(triangle, p2))
                {
                    pointList.RemoveAt((lowest + i + 1) % size);
                    if (i > 0) i -= 3;
                    size--;
                }
            }
            for (int i = 0; i < size; i++)
                g.DrawLine(pen1, pointList.ElementAt(i).Point, pointList.ElementAt((i + 1) % size).Point);
        }
        private void grahamButtonY_Click()
        {
            Point center = findCenter();
            MyPoint.Center = center;
            for (int i = 0; i < counter; i++)
                points[i].setAngle();
            QS.grahamPointSort(points, counter, true);
            g.DrawEllipse(pen1, center.X - 3, center.Y - 3, 6, 6);
            List<MyPoint> pointList = new List<MyPoint>(counter);
            for (int i = 0; i < counter; i++)
                pointList.Add(points[i]);

            MyPoint p1, p2, p3;
            MyPoint[] triangle = new MyPoint[3];
            triangle[0] = new MyPoint(center.X, center.Y);
            int size = pointList.Capacity;
            int lowest = pointList.FindIndex(0, pointList.Count, (p => p.Point.X == pointList.Min(min => min.Point.X)));
            for (int i = 0; i < size; i++)
            {
                p1 = pointList.ElementAt((lowest + i) % size);
                p2 = pointList.ElementAt((lowest + i + 1) % size);
                p3 = pointList.ElementAt((lowest + i + 2) % size);

                triangle[1] = p1;
                triangle[2] = p3;
                if (pnpoly(triangle, p2))
                {
                    pointList.RemoveAt((lowest + i + 1) % size);
                    if (i > 0) i -= 3;
                    size--;
                }
            }
            for (int i = 0; i < size; i++)
                g.DrawLine(pen1, pointList.ElementAt(i).Point, pointList.ElementAt((i + 1) % size).Point);
        }
        public void grahamButtonZ_Click()
        {
            Point center = findCenter();
            MyPoint.Center = center;
            for (int i = 0; i < counter; i++)
                points[i].setAngle();
            QS.grahamPointSort(points, counter, true);
            g.DrawEllipse(pen1, center.X - 3, center.Y - 3, 6, 6);
            List<MyPoint> pointList = new List<MyPoint>(counter);
            for (int i = 0; i < counter; i++)
                pointList.Add(points[i]);

            MyPoint p1, p2, p3;
            MyPoint[] triangle = new MyPoint[3];
            triangle[0] = new MyPoint(center.X, center.Y);
            int size = pointList.Capacity;
            int lowest = pointList.FindIndex(0, pointList.Count, (p => p.Point.X == pointList.Min(min => min.Point.X)));
            for (int i = 0; i < size; i++)
            {
                p1 = pointList.ElementAt((lowest + i) % size);
                p2 = pointList.ElementAt((lowest + i + 1) % size);
                p3 = pointList.ElementAt((lowest + i + 2) % size);

                triangle[1] = p1;
                triangle[2] = p3;
                if (pnpoly(triangle, p2))
                {
                    pointList.RemoveAt((lowest + i + 1) % size);
                    if (i > 0) i -= 3;
                    size--;
                }
            }
            for (int i = 0; i < size; i++)
                g.DrawLine(pen1, pointList.ElementAt(i).Point, pointList.ElementAt((i + 1) % size).Point);
        }

        public List<Vertex> jarvisButton_Z(List<Vertex> vertices, float[] laser_param)
        {
            counter = vertices.Count();
            List<PointF> list = new List<PointF>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.Z);
            double high = vertices.Max(d => d.Z);
            for (double i = low; i <= high; i+=laser_param[1])
            {
                list.Clear();
                foreach (Vertex v in vertices.FindAll(x => (int)x.Z == i))
                {
                    PointF p = new PointF((float)v.X, (float)v.Y);
                    list.Add(p);
                }
                List<PointF> toDraw = ConvexHull(list);
                
                if (toDraw != null)
                {
                    foreach (PointF point in toDraw)
                    {
                        Vertex vertex = new Vertex();
                        vertex.X = point.X; vertex.Y = point.Y; vertex.Z = i;
                        path.Add(vertex);
                    }
                }
            }
            if (small_points > 0)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
        public List<Vertex> jarvisButton_X(List<Vertex> vertices, float[] laser_param)
        {
            counter = vertices.Count();
            List<PointF> list = new List<PointF>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.X);
            double high = vertices.Max(d => d.X);
            for (double i = low; i <= high; i += laser_param[1])
            {
                list.Clear();
                foreach (Vertex v in vertices.FindAll(x => (int)x.X == (int)i))
                {
                    PointF p = new PointF((float)v.Z, (float)v.Y);
                    list.Add(p);
                }
                List<PointF> toDraw = ConvexHull(list);
                
                if (toDraw != null)
                {
                    foreach (PointF point in toDraw)
                    {
                        Vertex vertex = new Vertex();
                        vertex.X = i; vertex.Y = point.Y; vertex.Z = point.X;
                        path.Add(vertex);
                    }
                }
            }
            if (small_points > 0)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
        public List<Vertex> jarvisButton_Y(List<Vertex> vertices, float[] laser_param)
        {
            counter = vertices.Count();
            small_points = 0;
            List<PointF> list = new List<PointF>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.Y);
            double high = vertices.Max(d => d.Y);
            for (double i = low; i <= high; i += laser_param[1])
            {
                list.Clear();
                foreach (Vertex v in vertices.FindAll(x => (int)x.Y == i))
                {
                    PointF p = new PointF((float)v.X, (float)v.Z);
                    list.Add(p);
                }
                List<PointF> toDraw = ConvexHull(list);
                
                if (toDraw != null)
                {
                    foreach (PointF point in toDraw)
                    {
                        Vertex vertex = new Vertex();
                        vertex.X = point.X; vertex.Y = i; vertex.Z = point.Y;
                        path.Add(vertex);
                    }
                }
            }
            if (small_points > 0)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
    }
}
