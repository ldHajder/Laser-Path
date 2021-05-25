using ObjParser.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laser_path
{
    class View
    {
        private List<Vertex> vertices;
        public View(List<Vertex> list)
        {
            vertices = list;
        }

        public List<Vertex> chosenPoints(int type, float thirdAxis)
        {
            if (type == 0)
            {
                return vertices;
            }
            else
            {
                List<Vertex> chosenVer = new List<Vertex>();
                if (type == 1)
                {
                    foreach (Vertex ver in vertices.FindAll(x => (int)x.Z == thirdAxis))
                    {
                        chosenVer.Add(ver);
                    }
                }
                else if (type == 2)
                {
                    foreach (Vertex ver in vertices.FindAll(x => (int)x.Y == thirdAxis))
                    {
                        chosenVer.Add(ver);
                    }
                }
                else if (type == 3)
                {
                    foreach (Vertex ver in vertices.FindAll(x => (int)x.X == thirdAxis))
                    {
                        chosenVer.Add(ver);
                    }
                }
                return chosenVer;
            }
            //return vertices;
        }
    }
}
