using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laser_path
{
    class MyPoint
    {
        Point point;

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        double angle;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        static Point center;

        public static Point Center
        {
            get { return MyPoint.center; }
            set { MyPoint.center = value; }
        }

        public MyPoint()
        {
            point = new Point();
            MyPoint.center = new Point(0, 0);
            setAngle();
        }

        public MyPoint(int x, int y)
        {
            point = new Point(x, y);
            MyPoint.center = new Point(0, 0);
        }

        public void setAngle()
        {
            int tempX = point.X - center.X;
            int tempY = point.Y - center.Y;
            angle = Math.Atan2(tempY, tempX);
        }
    }
}
