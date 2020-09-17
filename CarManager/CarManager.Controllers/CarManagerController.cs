using System;
using CarManager.View;
using CarManager.Models;
using CarManager.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Controllers
{
    public class CarManagerController
    {
        public static void Run()
        {
            int r = CarView.GetMenuChoice("Select an option: ");

            switch (r)
            {
                case 1:
                    CreateCar();
                    break;
                case 2:
                    DisplayCar();
                    break;
                case 3:
                    SearchCar();
                    break;
                case 4:
                    EditCar();
                    break;
                case 5:
                    RemoveCar();
                    break;
            }

            Console.ReadKey();
        }
        
        private static void CreateCar()
        {
            Car newCar = CarView.GetNewCarInfo();
            Car savedCar = CarRepository.Create(newCar);

            // check to make sure object was created - could also check for length of list
            if (savedCar != null)
            {
                Console.WriteLine("Car Saved!");
            }
            else Console.WriteLine("Car is NULL");
        }

        private static void SearchCar()
        {
            int carID = CarView.GetCarId(); // ask user for ID
            Car locatedCar = CarRepository.ReadById(carID); // search for car by ID in our list

            if (locatedCar != null)
            {
                CarView.DisplayCar(locatedCar);
            }
            else Console.WriteLine("Car not found.");
        }
  
        private static void DisplayCar() // list of all cars
        {
            List<Car> listToDisplay = CarRepository.ReadAll();

            // if there are any cars in our list, display them one after another
            if (listToDisplay.Any())
            {
                foreach (Car car in listToDisplay)
                {
                    CarView.DisplayCar(car);
                }
            }
            else Console.WriteLine("No cars saved in repository.");
        }
        
        private static void EditCar()
        {
            int carIDToEdit = CarView.GetCarId();
            Car locatedCar = CarRepository.ReadById(carIDToEdit);

            if (locatedCar != null)
            {
                Console.WriteLine();
                Console.WriteLine("Car to Edit: ");
                CarView.DisplayCar(locatedCar);
                Car newlyEditedCar = CarView.EditCarInfo(locatedCar);
                CarRepository.Update(carIDToEdit, newlyEditedCar);
                Console.WriteLine("Car saved!");
            }
            else Console.WriteLine("Car not found.");            

        }
        
        private static void RemoveCar()
        {            
            int carIDToRemove = CarView.GetCarId();
            Car locatedCar = CarRepository.ReadById(carIDToRemove);
            
            if (locatedCar != null)
            { 
                bool confirmRemoval = CarView.ConfirmRemoveCar(locatedCar);

                if (confirmRemoval == true)
                {
                    CarRepository.Delete(carIDToRemove);
                }

                else Console.WriteLine("Car will not be removed.");
            }

            else Console.WriteLine("Car not found.");
            
        }
        
        public static bool ReturnToMenu()
        {
            return CarView.ReturnToMainMenu();
        }

    }
}
