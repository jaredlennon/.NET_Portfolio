using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyHeartsV2
{
    class Program
    {
        static void Main(string[] args)
        {
            int userAge;
            int maxHR;
            int hrZoneLowerLimit;
            int hrZoneUpperLimit;

            Console.Write("What is your age? ");
            userAge = Convert.ToInt32(Console.ReadLine());

            maxHR = 220 - userAge;
            Console.WriteLine("Your maximum heart rate should be " + maxHR + " beats per minute.");

            hrZoneLowerLimit = Convert.ToInt32(maxHR * 0.50); // 50% of maxHR
            hrZoneUpperLimit = Convert.ToInt32(maxHR * 0.85); // 85% of maxHR

            Console.WriteLine("Your target HR Zone is " + hrZoneLowerLimit + " - " + hrZoneUpperLimit + " beats per minute");
            Console.ReadKey();
        }
    }
}
