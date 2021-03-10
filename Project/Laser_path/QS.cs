using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laser_path
{
    class QS
    {

        private static void swap(MyPoint[] tab, int i, int j)
        {
            MyPoint temp = tab[i];
            tab[i] = tab[j];
            tab[j] = temp;
        }

        private static double countR(MyPoint point)
        {
            return (point.Point.X - MyPoint.Center.X) * (point.Point.X - MyPoint.Center.X) + (point.Point.Y - MyPoint.Center.Y) * (point.Point.Y - MyPoint.Center.Y);
        }

        private static void QuickSort(MyPoint[] array, int left, int right)
        {
            var i = left;
            var j = right;
            var pivot = array[(left + right) / 2].Angle;
            while (i < j)
            {
                while (array[i].Angle < pivot) i++;
                while (array[j].Angle > pivot) j--;
                if (i <= j)
                {
                    // swap
                    var tmp = array[i];
                    array[i++] = array[j];  // ++ and -- inside array braces for shorter code
                    array[j--] = tmp;
                }
                if (left < j) QuickSort(array, left, j);
                if (i < right) QuickSort(array, i, right);
            }
        }

        private static void rSort(MyPoint[] tab, int size)
        {
            for (int i = 1; i < size; i++)
                if (tab[i - 1].Angle == tab[i].Angle)
                    if (countR(tab[i - 1]) > countR(tab[i]))
                        swap(tab, i - 1, i);
        }

        private static void rJarvisSort(MyPoint[] tab, int size)
        {
            for (int i = 1; i < size; i++)
                if (tab[i - 1].Angle == tab[i].Angle)
                    if (countR(tab[i - 1]) < countR(tab[i]))
                        swap(tab, i - 1, i);
        }

        public static void grahamPointSort(MyPoint[] tab, int size, Boolean which)
        {
            QuickSort(tab, 0, size - 1);
            if (which)
                rSort(tab, size);
            else
                rJarvisSort(tab, size);
        }
    }
}
