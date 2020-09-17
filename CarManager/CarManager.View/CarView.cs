using System;
using CarManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.View
{
    public class CarView
    {
        public static int GetMenuChoice(string prompt)
        {
            string menuChoice;
            int result;

            while (true)
            {
                Console.WriteLine("Menu: \n1 - Create\n2 - Display \n3 - Search \n4 - Edit \n5 - Remove");
                Console.Write(prompt);
                menuChoice = Console.ReadLine();

                if(int.TryParse(menuChoice, out result))
                {
                    return result;
                }
                Console.WriteLine("This is not a valid input.");
                Console.WriteLine();
            }
        }
        
        public static Car GetNewCarInfo()
        {
            Car car1 = new Car();
            Console.WriteLine();

            // asks the user input, output, input, output ... then put into an object
            Console.WriteLine("Please provide the following information: ");
            Console.Write("Car ID: ");
            car1.id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Car Name: ");
            car1.Name = Convert.ToString(Console.ReadLine());
            Console.Write("Car Price: ");
            car1.Price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Car Safety Rating: ");
            car1.safetyRating = float.Parse(Console.ReadLine());
            Console.Write("Car Model Year: ");
            car1.modelYear = Convert.ToInt32(Console.ReadLine());

            return car1;
        }

        public static int GetCarId()
        {
            string carID;
            int searchResult;

            while(true)
            {
                Console.WriteLine();
                Console.Write("Please provide the ID of the car: ");
                carID = Console.ReadLine();

                if (int.TryParse(carID, out searchResult))
                {
                    return searchResult;
                }
                Console.WriteLine("This is not a valid input.");

            }

        }

        public static void DisplayCar (Car car)
        {
            Console.WriteLine();
            Console.WriteLine("Car ID: {0}", car.id);
            Console.WriteLine("Name: {0}", car.Name);
            Console.WriteLine("Price: {0:C}", car.Price);
            Console.WriteLine("Safety Rating: {0:0.0}", car.safetyRating);
            Console.WriteLine("Model Year: {0}", car.modelYear);
        }

        public static Car EditCarInfo (Car car)
        {
            Car carToEdit = car;
            Console.WriteLine();

            // asks the user input, output, input, output ... then put into an object
            carToEdit.id = car.id;
            Console.WriteLine("Please provide the following updated information: ");
            Console.Write("Car Name: ");
            carToEdit.Name = Convert.ToString(Console.ReadLine());
            Console.Write("Car Price: ");
            carToEdit.Price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Car Safety Rating: ");
            carToEdit.safetyRating = float.Parse(Console.ReadLine());
            Console.Write("Car Model Year: ");
            carToEdit.modelYear = Convert.ToInt32(Console.ReadLine());

            return carToEdit;
        }

        public static bool ConfirmRemoveCar(Car car)
        {
            Console.Write("Are you sure you want to remove car {0} (y/n)?: ", car.id);
            string confirmDelete = Console.ReadLine();
            string answerAdjusted = confirmDelete.ToLower();
            // Console.WriteLine();

            if (answerAdjusted == "y")
                return true;
            else
                return false;
        }

        public static bool ReturnToMainMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Return to menu (y/n)? ");
                string returnToMenuAnswer = Console.ReadLine();
                string answerAdjusted = returnToMenuAnswer.ToLower();

                Console.WriteLine();

                if (answerAdjusted == "y")
                    return true;
                if (answerAdjusted == "n")
                    Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return false;
            }

        }
    }
}
