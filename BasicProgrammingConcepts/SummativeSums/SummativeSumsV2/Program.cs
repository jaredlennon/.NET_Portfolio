using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummativeSumsV2
{
    class Program
    {

        static void Main(string[] args)
        {

            int[] array = new int[] { 1, 90, -33, -55, 67, -16, 28, -55, 15 };
            Program.ArraySums(array);
            int sumArray1 = Program.ArraySums(array);
            Console.WriteLine("#1 Array Sum: " + sumArray1);

            array = new int[] { 999, -60, -77, 14, 160, 301 };
            Program.ArraySums(array);
            int sumArray2 = Program.ArraySums(array);
            Console.WriteLine("#2 Array Sum: " + sumArray2);

            array = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, -99 };
            Program.ArraySums(array);
            int sumArray3 = Program.ArraySums(array);
            Console.WriteLine("#3 Array Sum: " + sumArray3);

            Console.ReadKey();
        }

        public static int ArraySums(int []array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

    }
}
