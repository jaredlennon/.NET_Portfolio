using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogGeneticsV2
{
    class Program
    {
        static void Main(string[] args)
        {
            int expectedSum = 100;

            Random r = new Random();
            int[] randoms = new int[5];

            // Generate 4 random values and get their sum
            int sum = 0;
            for (int i = 0; i < randoms.Length - 1; i++)
            {
                randoms[i] = r.Next(expectedSum);
                sum += randoms[i];
            }

            // Adjust the sum as if there were 5 random values
            int actualSum = sum * randoms.Length / (randoms.Length - 1);

            // Normalize 4 random values and get their sum
            sum = 0;
            for (int i = 0; i < randoms.Length - 1; i++)
            {
                randoms[i] = randoms[i] * expectedSum / actualSum;
                sum += randoms[i];
            }

            // Set the last value
            randoms[randoms.Length - 1] = expectedSum - sum;

            // Console.WriteLine("{0}", string.Join(", ", randoms)); // check random ints generated

            Console.Write("Hello! What is the name of your dog? ");
            string dogName = Console.ReadLine();

            Console.WriteLine("Well then, I have this highly reliable report on " + dogName + "'s prestigious background right here.");
            Console.WriteLine();

            Console.WriteLine(dogName + " is:");
            Console.WriteLine();
            Console.WriteLine("{0}% Labrador Retriever", randoms[0]);
            Console.WriteLine("{0}% German Shepherd", randoms[1]);
            Console.WriteLine("{0}% Rottweiler", randoms[2]);
            Console.WriteLine("{0}% French Bulldog", randoms[3]);
            Console.WriteLine("{0}% Poodle", randoms[4]);
            Console.WriteLine();
            Console.WriteLine("Wow, that's QUITE the dog!");
            Console.ReadKey();

        }

    }
}
