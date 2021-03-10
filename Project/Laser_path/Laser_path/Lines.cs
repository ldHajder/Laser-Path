using ObjParser.Types;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laser_path
{
    class Lines
    {
        private System.Drawing.Graphics g;
        private System.Drawing.Pen pen1;
        private System.Drawing.Pen pen2;
        private MyPoint[] points;
        private const uint POINTS = 100;
        private int counter;
        private int small_points = 0;





 
        public List<Vertex> list_X(List<Vertex> vertices, float[] laser_param, Vector3d startP, int layerDirect, int layers)
        {
            counter = vertices.Count();
            List<Vertex> list = new List<Vertex>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.X);
            double high = vertices.Max(d => d.X);
            Vertex vertex = new Vertex();
            
            for (double i = low; i <= high; i += laser_param[1])
            {
                float licz = laser_param[1];
                list = new List<Vertex>();
                list = vertices.FindAll(p => p.X == i);
                while(list == null)
                {
                    list = vertices.FindAll(p => p.X >= i-licz && p.X<=i+licz);
                    licz += laser_param[1];
                }
                //vertex.X = i; vertex.Y = startP.Y; vertex.Z = startP.Z;
                //path.Add(vertex);
                if (layerDirect == 2)
                { // Y direction
                    vertex.X = i;
                    vertex.Y = list.Min(p => p.Y);
                    vertex.Z = startP.Z;

                    path.Add(vertex);

                    vertex.X = i;
                    vertex.Y = list.Max(p => p.Y);
                    vertex.Z = startP.Z;

                    path.Add(vertex);
                }
                if (layerDirect == 3)
                { // Z direction
                    vertex.X = i;
                    vertex.Y = startP.Y;
                    vertex.Z =  list.Min(p => p.Z);

                    path.Add(vertex);

                    vertex.X = i;
                    vertex.Y = startP.Y;
                    vertex.Z = list.Max(p => p.Z);

                    path.Add(vertex);
                }
            }
        
            
            if (path.Count <= 0 || path == null)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
        public List<Vertex> list_Y(List<Vertex> vertices, float[] laser_param, Vector3d startP, int layerDirect, int layers)
        {
            counter = vertices.Count();
            List<Vertex> list = new List<Vertex>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.Y);
            double high = vertices.Max(d => d.Y);
            Vertex vertex = new Vertex();

            for (double i = low; i <= high; i += laser_param[1])
            {
                float licz = laser_param[1];
                list = new List<Vertex>();
                list = vertices.FindAll(p => p.Y == i);
                while (list == null)
                {
                    list = vertices.FindAll(p => p.Y >= i - licz && p.Y <= i + licz);
                    licz += laser_param[1];
                }
                //vertex.X = i; vertex.Y = startP.Y; vertex.Z = startP.Z;
                //path.Add(vertex);
                if (layerDirect == 1)
                { // X direction
                    vertex.Y = i;
                    vertex.X = list.Min(p => p.X);
                    vertex.Z = startP.Z;

                    path.Add(vertex);

                    vertex.Y = i;
                    vertex.X = list.Max(p => p.X);
                    vertex.Z = startP.Z;

                    path.Add(vertex);
                }
                if (layerDirect == 3)
                { // Z direction
                    vertex.Y = i;
                    vertex.X = startP.X;
                    vertex.Z = list.Min(p => p.Z);

                    path.Add(vertex);

                    vertex.Y = i;
                    vertex.X = startP.X;
                    vertex.Z = list.Max(p => p.Z);

                    path.Add(vertex);
                }
            }


            if (path.Count <= 0 || path == null)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
        public List<Vertex> list_Z(List<Vertex> vertices, float[] laser_param, Vector3d startP, int layerDirect, int layers)
        {
            counter = vertices.Count();
            List<Vertex> list = new List<Vertex>();
            List<Vertex> path = new List<Vertex>();
            double low = vertices.Min(d => d.Z);
            double high = vertices.Max(d => d.Z);
            Vertex vertex = new Vertex();

            for (double i = low; i <= high; i += laser_param[1])
            {
                float licz = laser_param[1];
                list = new List<Vertex>();
                list = vertices.FindAll(p => p.Z == i);
                while (list == null || list.Count<2)
                {
                    list = vertices.FindAll(p => p.Z >= i && p.Z <= i + licz);
                    licz += laser_param[1];
                }
                //vertex.X = i; vertex.Y = startP.Y; vertex.Z = startP.Z;
                //path.Add(vertex);
                if (layerDirect == 1)
                { // X direction
                    vertex = new Vertex();
                    vertex.Z = i;
                    vertex.X = list.Min(p => p.X);
                    vertex.Y = startP.Y;

                    path.Add(vertex);
                    vertex = new Vertex();
                    vertex.Z = i;
                    vertex.X = list.Max(p => p.X);
                    vertex.Y = startP.Y;

                    path.Add(vertex);
                }
                else if (layerDirect == 3)
                { // Z direction
                    vertex = new Vertex();
                    vertex.Z = i;
                    vertex.X = startP.X;
                    vertex.Y = list.Min(p => p.Y);

                    path.Add(vertex);
                    vertex = new Vertex();
                    vertex.Z = i;
                    vertex.X = startP.X;
                    vertex.Y = list.Max(p => p.Y);

                    path.Add(vertex);
                }
            }


            if (path.Count <= 0 || path == null)
            {
                string messageBoxText = "That was too small amount of points. In the path can be error!";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return path;
        }
    }
}
