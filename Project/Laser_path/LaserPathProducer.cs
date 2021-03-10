using ObjParser;
using ObjParser.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laser_path
{
    class LaserPathProducer
    {
        private Obj obj;
        private List<Vertex> VertexSortedList;
        private List<Vertex> vertices;
        private int up_axis;
        float laser_high;
        float laser_width;
        float laser_velocity;
        int laser_power;
        float model_width;
        int n_p;

        public LaserPathProducer(Obj obj, float[] laser_param, float model_width, List<Vertex> vertices)
        {
            this.obj = obj;
            this.laser_width = laser_param[0];
            this.laser_high = laser_param[1];
            this.laser_velocity = laser_param[2];
            this.laser_power = (int)laser_param[3];
            this.model_width = model_width;
            this.vertices = vertices;

            n_p = model_width > laser_width ? (int)(model_width / laser_width) : 1;
        }

        private void sortedList()
        {
            if (up_axis == 0)
            {
                VertexSortedList = obj.VertexList.OrderBy(p => p.X).ToList();
            }
            else if (up_axis == 2)
            {
                VertexSortedList = obj.VertexList.OrderBy(p => p.Z).ToList();
            }
            else
            {
                VertexSortedList = obj.VertexList.OrderBy(p => p.Y).ToList();
            }
        }

        public List<Vertex> getMorePoints(int how_much)
        {
            if (how_much == 1)
            {
                return morePoints();
            }
            else
            {
                return morePoints(how_much);
            }

        }

        private List<Vertex> morePoints()
        {
            ////List<Vertex> new_points = new List<Vertex>();
            //////modelwidth();

            ////foreach (Face face in obj.FaceList)
            ////{
            ////    Vertex point1 = obj.VertexList[face.VertexIndexList[0] - 1];
            ////    Vertex point2 = obj.VertexList[face.VertexIndexList[1] - 1];
            ////    Vertex point3 = obj.VertexList[face.VertexIndexList[2] - 1];
            ////    Vertex point12 = new Vertex();
            ////    point12.X = (point1.X + point2.X) / 2; point12.Y = (point1.Y + point2.Y) / 2; point12.Z = (point1.Z + point2.Z) / 2; point12.Index = new_points.Count + obj.VertexList.Count;
            ////    Vertex point23 = new Vertex();
            ////    point23.X = (point3.X + point2.X) / 2; point23.Y = (point3.Y + point2.Y) / 2; point23.Z = (point3.Z + point2.Z) / 2; point23.Index += obj.VertexList.Count;
            ////    Vertex point31 = new Vertex();
            ////    point31.X = (point1.X + point3.X) / 2; point31.Y = (point1.Y + point3.Y) / 2; point31.Z = (point1.Z + point3.Z) / 2; point31.Index += obj.VertexList.Count;

            ////    new_points.Add(point12);
            ////    new_points.Add(point23);
            ////    new_points.Add(point31);
            ////}
            ////VertexSortedList = new List<Vertex>();
            ////foreach (Vertex ver in obj.VertexList)
            ////{
            ////    VertexSortedList.Add(ver);
            ////}
            ////foreach (Vertex ver in new_points)
            ////{
            ////    ver.Index += obj.VertexList.Count;
            ////    VertexSortedList.Add(ver);
            ////}


            List<Vertex> new_points = new List<Vertex>();
            
            foreach (Face face in obj.FaceList)
            {
                
                Vertex point1 = vertices.Find(d=>d.Index == (obj.VertexList[face.VertexIndexList[0] - 1].Index));
                Vertex point2 = vertices.Find(d => d.Index == (obj.VertexList[face.VertexIndexList[1] - 1].Index));
                Vertex point3 = vertices.Find(d => d.Index == (obj.VertexList[face.VertexIndexList[2] - 1].Index));
                Vertex point12 = new Vertex();
                point12.X = (point1.X + point2.X) / 2; point12.Y = (point1.Y + point2.Y) / 2; point12.Z = (point1.Z + point2.Z) / 2; point12.Index = new_points.Count + obj.VertexList.Count;
                Vertex point23 = new Vertex();
                point23.X = (point3.X + point2.X) / 2; point23.Y = (point3.Y + point2.Y) / 2; point23.Z = (point3.Z + point2.Z) / 2; point23.Index += obj.VertexList.Count;
                Vertex point31 = new Vertex();
                point31.X = (point1.X + point3.X) / 2; point31.Y = (point1.Y + point3.Y) / 2; point31.Z = (point1.Z + point3.Z) / 2; point31.Index += obj.VertexList.Count;

                new_points.Add(point12);
                new_points.Add(point23);
                new_points.Add(point31);
            }
            VertexSortedList = new List<Vertex>();
            foreach (Vertex ver in vertices)
            {
                VertexSortedList.Add(ver);
            }
            foreach (Vertex ver in new_points)
            {
                ver.Index += vertices.Count;
                VertexSortedList.Add(ver);
            }

            return /*laserPoints();new_points;*/ VertexSortedList;
        }

        private List<Vertex> morePoints(double how_much)
        {
            List<Vertex> new_points = new List<Vertex>();
            //modelwidth();

            foreach (Face face in obj.FaceList)
            {
                for (int i = 0; i < face.VertexIndexList.Length; i++)
                {
                    Vertex point1 = vertices.Find(d => d.Index == obj.VertexList[face.VertexIndexList[i] - 1].Index);
                    Vertex point2 = vertices.Find(d => d.Index == (obj.VertexList[face.VertexIndexList[(i + 1) % face.VertexIndexList.Length] - 1]).Index);

                    int licz = 0;
                    double[] tab = new double[3];
                    tab[0] = (Math.Abs(point2.X - point1.X) / laser_width);
                    tab[1] = (Math.Abs(point2.Z - point1.Z) / laser_high);
                    tab[2] = (Math.Abs(point2.Y - point1.Y) / laser_width);
                    how_much = (int)tab[1];


                    if (how_much > 0)
                    {

                        for (double j = 0; j < how_much; j++)
                        {
                            Vertex point = new Vertex();
                            point.X = point1.X * (double)(j / how_much) + point2.X * (double)((how_much - j) / how_much);
                            point.Y = point1.Y * (double)(j / how_much) + point2.Y * (double)((how_much - j) / how_much);
                            point.Z = point1.Z * (double)(j / how_much) + point2.Z * (double)((how_much - j) / how_much);
                            point.Index = new_points.Count + obj.VertexList.Count;
                            new_points.Add(point);
                        }
                    }
                }
            }
            VertexSortedList = new List<Vertex>();
            foreach (Vertex ver in vertices)
            {
                VertexSortedList.Add(ver);
            }
            foreach (Vertex ver in new_points)
            {
                ver.Index += obj.VertexList.Count;
                VertexSortedList.Add(ver);
            }
            //List<Twynik> list_of_points = sorting_list(VertexSortedList.FindAll(x => (int)x.Y == 0));


            //list_of_points.OrderBy(x => x.alfa);
            return /*laserPoints();new_points;*/  VertexSortedList;
        }

        private void modelwidth()
        {
            List<Vertex> vertices2 = new List<Vertex>();

            vertices2 = obj.VertexList.FindAll(x => x.X == 0 && x.Y == 0);
            vertices2 = vertices2.FindAll(x => x.Z >= 0);
            if (vertices2.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double maxVal = int.MinValue;
            double minVal = int.MaxValue;

            foreach (Vertex type in vertices2)
            {
                if (type.Z > maxVal)
                {
                    maxVal = type.Z;
                }
                if (type.Z < minVal)
                {
                    minVal = type.Z;
                }
            }
            model_width = (float)(maxVal - minVal);
        }
        private bool linel(Vertex point1, Vertex point2)
        {
            double ll = Math.Sqrt((Math.Pow((point1.X - point2.X), 2)) + (Math.Pow((point1.Y - point2.Y), 2)));
            if ((int)ll == (int)model_width) return true;
            else return false;
        }
        public List<Vertex> laserPoints()
        {
            modelwidth();
            List<Vertex> min_points = new List<Vertex>();
            List<Vertex> sortedList = new List<Vertex>();
            Vertex start = new Vertex();

            List<Vertex> vertices2 = new List<Vertex>();
            sortedList = obj.VertexList.OrderBy(p => p.Z).ToList();
            int it = -1;
            do
            {
                it++;
                start = sortedList[it];

            } while (!(start.Y == 0 && start.Z > 0));
            //vertices2 = obj.VertexList.FindAll(x => x.Y == 0 && x.Z == 0);
            //vertices2 = vertices2.FindAll(x => x.X > 0);
            //if (vertices2.Count == 0)
            //{
            //    throw new InvalidOperationException("Empty list");
            //}
            //start.X = int.MinValue;
            //double minVal = int.MaxValue;


            //foreach (Vertex type in vertices2)
            //{
            //    if (type.X > start.X)
            //    {
            //        start = type;
            //        start.Index = type.Index;
            //    }
            //    //if (type.Z < minVal)
            //    //{
            //    //    minVal = type.Z;
            //    //}
            //}

            min_points.Add(start);



            foreach (Face face in obj.FaceList.FindAll(x => x.VertexIndexList.Any(y => y == min_points[min_points.Count - 1].Index + 1)))
            {

                Vertex point1 = obj.VertexList[face.VertexIndexList[0] - 1];
                Vertex point2 = obj.VertexList[face.VertexIndexList[1] - 1];
                Vertex point3 = obj.VertexList[face.VertexIndexList[2] - 1];


            }
            foreach (Vertex ver in min_points)
            {
                obj.VertexList.Add(ver);
            }


            List<Twynik> list_of_points = sorting_list(min_points.FindAll(x => (int)x.Y == 0));





            return min_points;  //obj.VertexList;


        }

        private void laser_bottom_PathMaking()
        {

        }

        private void sortPoints(List<Vertex> ver_list)
        {

        }


        //______________ POINT SORTING_____________________________________________________
        //Struktura reprezentująca punkt
        struct Tpunkt
        {
            public double x, y;
        };


        //Struktura wyniku: punkt oraz współczynnik alfa
        struct Twynik
        {
            public double x, y, alfa;
        };




        //Wektor wynikowy
        List<Twynik> wynik;

        List<Twynik> sorting_list(List<Vertex> list)
        {        
            List<Twynik> wynik = new List<Twynik>();
            Twynik w = new Twynik();
            double d;
            double alfa = 0;

            foreach (Vertex p in list)
            {

                d = Math.Abs(p.X) + Math.Abs(p.Y);

                if ((p.X >= 0) && (p.Y >= 0))
                    alfa = p.Y / d;

                if ((p.X < 0) && (p.Y >= 0))
                    alfa = 2 - p.Y / d;

                if ((p.X < 0) && (p.Y < 0))
                    alfa = 2 + Math.Abs(p.Y) / d;

                if ((p.X >= 0) && (p.Y < 0))
                    alfa = 4 - Math.Abs(p.Y) / d;

                //Zapisz punkt i wspolczynnik do wektora wynikowego
                w.x = p.X;
                w.y = p.Y;
                w.alfa = alfa;
                wynik.Add(w);
            }            
            return wynik;
        }

        //Funkcja oblicza wyznacznik macierzy 3x3 (metodą Sarrusa)
        double det(Twynik p1, Twynik p2, Twynik p3)
        {
            return p1.x * p2.y + p2.x * p3.y + p3.x * p1.y - p3.x * p2.y - p1.x * p3.y - p2.x * p1.y;
        }


        //Funkcja sprawdza, czy przechodząc do punktu p3 skręcamy w prawo (1), czy w lewo (0)
        double skret_w_prawo(List<Twynik> S, Twynik p3)
        {
            Twynik p2;
            Twynik p1;
            int count = S.Count();
            //Pobieramy ze szczytu stosu punkt p2 oraz punkt p1, bedacy tuż pod szczytem
            p2 = S[count-1];
            p1 = S[count-2];
            
            //Punkt p3 leży po lewej stronie wektora p1->p2
            if (det(p1, p2, p3) > 0)
                return 0;

            //Punkt p3 leży po prawej stronie wektora p1->p2
            if (det(p1, p2, p3) < 0)
                return 1;
            return -1;
        }

        List<Twynik> path(List<Twynik> list)
        {
            //Lista wejściowa (uporządkowana: czyt. na stronie)
            List<Twynik> S = new List<Twynik>();

            S.Add(list[0]);
            S.Add(list[1]);
            S.Add(list[2]);

            for (int i = 3; i < list.Count; i++)
            {
                while (skret_w_prawo(S, list[i]) == 1)
                    S.Remove(S.Last());
                S.Add(list[i]);
            }


            Console.WriteLine("Punkty tworzace wypukla otoczke: \n");
            if (!(S.Count !=0))
            {
                foreach (Twynik t in S) {
                    Console.WriteLine("(%d,%d) ", t.x, t.y);
                }
            }
            return S;
        }

    }
}
