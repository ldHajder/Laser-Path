using ObjParser.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laser_path
{
    class PathMaker
    {
        public double[] time;
        public int[] power;
        private int direction;
        private int layerDirect;
        bool cont;
        public PathMaker(List<Vertex> vertices, double velocity, double laser_power, int direction, bool continuous)
        {
            time = new double[vertices.Count()];
            power = new int[vertices.Count()];
            this.direction = direction;
            cont = continuous;
            if(time.Count() != 0 && time != null)
                addTime_to_path(vertices, velocity);
            if (vertices.Count() != 0 && vertices != null)
            {
                if(cont == true)
                    addPower_to_path(vertices, laser_power);
            }
            
        }

        public PathMaker(List<Vertex> vertices, double velocity, double laser_power, int direction, int layerDirect, bool continuous)
        {
            time = new double[vertices.Count()];
            power = new int[vertices.Count()];
            this.direction = direction;
            this.layerDirect = layerDirect;
            cont = continuous;
            if (time.Count() != 0 && time != null)
                addTime_to_path2(vertices, velocity, layerDirect);
            if (vertices.Count() != 0 && vertices != null)
            {
                if (cont == true)
                    addPower_to_path2(vertices, laser_power, layerDirect, continuous);
            }

        }

        public double[] time_steps()
        {
            return time;
        }
        public int[] power_steps()
        {
            return power;
        }

        private void addTime_to_path(List<Vertex> vertices, double velocity)
        {
            time[0] = 0;
            for(int i = 1; i < vertices.Count; i++)
            {
                double length = Math.Sqrt(Math.Pow((vertices[i].X - vertices[i - 1].X), 2) + Math.Pow((vertices[i].Y - vertices[i - 1].Y), 2));
                time[i] = time[i-1] + length / velocity;
                if (direction == 0)
                {
                    if ((vertices[i].X - vertices[i - 1].X) != 0)
                        time[i] = time[i - 1] + 0.25 * length / velocity;
                }
                else if (direction == 1)
                {
                    if ((vertices[i].Y - vertices[i - 1].Y) != 0)
                        time[i] = time[i - 1] + 0.25 * length / velocity;
                }
                else
                {
                    if ((vertices[i].Z - vertices[i - 1].Z) != 0)
                        time[i] = time[i - 1] + 0.25 * length / velocity;
                }
            }
            time[time.Count()-1] += velocity * 25;
            //return time;
        }

        private void addTime_to_path2(List<Vertex> vertices, double velocity, int layerDirect)
        {
            if (layerDirect == 1)
            {
                time[0] = 0;
                for (int i = 1; i < vertices.Count; i++)
                {
                    double length = Math.Sqrt(Math.Pow((vertices[i].X - vertices[i - 1].X), 2));
                    time[i] = time[i - 1] + length / velocity;
                    if (direction == 0)
                    {
                        if ((vertices[i].X - vertices[i - 1].X) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else if (direction == 1)
                    {
                        if ((vertices[i].Y - vertices[i - 1].Y) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else
                    {
                        if ((vertices[i].Z - vertices[i - 1].Z) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                }
                time[time.Count() - 1] += velocity * 25;
                //return time;
            }
            else if(layerDirect == 2)
            {
                time[0] = 0;
                for (int i = 1; i < vertices.Count; i++)
                {
                    double length = Math.Sqrt(Math.Pow((vertices[i].Y - vertices[i - 1].Y), 2));
                    time[i] = time[i - 1] + length / velocity;
                    if (direction == 0)
                    {
                        if ((vertices[i].X - vertices[i - 1].X) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else if (direction == 1)
                    {
                        if ((vertices[i].Y - vertices[i - 1].Y) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else
                    {
                        if ((vertices[i].Z - vertices[i - 1].Z) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                }
                time[time.Count() - 1] += velocity * 25;
                //return time;
            }
            else
            {
                time[0] = 0;
                for (int i = 1; i < vertices.Count; i++)
                {
                    double length = Math.Sqrt(Math.Pow((vertices[i].Z - vertices[i - 1].Z), 2));
                    time[i] = time[i - 1] + length / velocity;
                    if (direction == 0)
                    {
                        if ((vertices[i].X - vertices[i - 1].X) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else if (direction == 1)
                    {
                        if ((vertices[i].Y - vertices[i - 1].Y) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                    else
                    {
                        if ((vertices[i].Z - vertices[i - 1].Z) != 0)
                            time[i] = time[i - 1] + 0.25 * length / velocity;
                    }
                }
                time[time.Count() - 1] += velocity * 25;
                //return time;
            }
        }

        private void addPower_to_path(List<Vertex> vertices, double laser_power)
        {
            power[0] = (int)laser_power;
            if(direction == 0)
            {                
                for (int i = 1; i < vertices.Count; i++)
                {
                    power[i] = (int)laser_power;    //      power[i] = ((vertices[i].X - vertices[i - 1].X) == 0) ? (int)laser_power : 0;
                }
            }
            else if (direction == 1){
                for (int i = 1; i < vertices.Count; i++)
                {
                    power[i] = (int)laser_power;    //      power[i] = ((vertices[i].Y - vertices[i - 1].Y) == 0) ? (int)laser_power : 0;
                }
            }
            else{
                for (int i = 1; i < vertices.Count; i++)
                {
                    power[i] = (int)laser_power;    //      power[i] = ((vertices[i].Z - vertices[i - 1].Z) == 0) ? (int)laser_power : 0;
                }
            }
            //power[vertices.Count] = 0;
            //return power;
        }

        private void addPower_to_path2(List<Vertex> vertices, double laser_power, int layerDirect, bool continuous)
        {
            if (continuous)
            {
                power[0] = (int)laser_power;
                if (direction == 0)
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (vertices[i].X > vertices[i - 1].X) ? (int)laser_power : 0;    //      power[i] = ((vertices[i].X - vertices[i - 1].X) == 0) ? (int)laser_power : 0;
                    }
                }
                else if (direction == 1)
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (vertices[i].Y > vertices[i - 1].Y) ? (int)laser_power : 0;    //      power[i] = ((vertices[i].Y - vertices[i - 1].Y) == 0) ? (int)laser_power : 0;
                    }
                }
                else
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (vertices[i].Z > vertices[i - 1].Z) ? (int)laser_power : 0;    //      power[i] = ((vertices[i].Z - vertices[i - 1].Z) == 0) ? (int)laser_power : 0;
                    }
                }
            }
            else
            {
                power[0] = (int)laser_power;
                if (layerDirect == 1)
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (int)laser_power;    //      power[i] = ((vertices[i].X - vertices[i - 1].X) == 0) ? (int)laser_power : 0;
                    }
                }
                else if (layerDirect == 2)
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (int)laser_power;    //      power[i] = ((vertices[i].Y - vertices[i - 1].Y) == 0) ? (int)laser_power : 0;
                    }
                }
                else
                {
                    for (int i = 1; i < vertices.Count; i++)
                    {
                        power[i] = (int)laser_power;    //      power[i] = ((vertices[i].Z - vertices[i - 1].Z) == 0) ? (int)laser_power : 0;
                    }
                }
            }
            //power[vertices.Count] = 0;
            //return power;
        }
    }
}
