using System;
using CarManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Data
{
    public class CarRepository
    {
        // new list containing all cars
        private static List<Car> carDB = new List<Car>();

        public static Car Create(Car car)
        {
            carDB.Add(car);
            return car;
        }
        
        // loop through carDB list, locate object by id, return that object
        public static Car ReadById(int id)
        {
            foreach (Car car in carDB)
            {
                if (id == car.id)
                    return car;        
            }

            return null;
        }
        
        // return/display entire list
        public static List<Car> ReadAll()
        {
            return carDB;
        }

        // Update(int id, Car car): void
        
        public static void Update(int id, Car newlyEditedCar)
        {
            int idToUpdate = id;
            Car updatedCar = newlyEditedCar;

            List<Car> newUpdatedList = new List<Car>();

            // takes in an id and car object
            foreach (Car car in carDB)
            {
                if (car.id == idToUpdate)
                {
                    car.id = updatedCar.id;
                    car.Name = updatedCar.Name;
                    car.Price = updatedCar.Price;
                    car.safetyRating = updatedCar.safetyRating;
                    car.modelYear = updatedCar.modelYear;
                    newUpdatedList.Add(updatedCar);
                }
                else
                {
                    Car carToNewList = new Car();
                    carToNewList.id = car.id;
                    carToNewList.Name = car.Name;
                    carToNewList.Price = car.Price;
                    carToNewList.safetyRating = car.safetyRating;
                    carToNewList.modelYear = car.modelYear;
                    newUpdatedList.Add(carToNewList);
                }
                carDB = newUpdatedList;
            }
        }
        
        public static void Delete(int id)
        {
            int idToRemove = id;
            List<Car> newListForRemoval = new List<Car>();           

            foreach (Car car in carDB)
            {
                // if the id does NOT match, copy the current lists' objects into new list                
                if (car.id != idToRemove)
                {
                    // copy into new list
                    Car carToNewList = new Car();
                    carToNewList.id = car.id;
                    carToNewList.Name = car.Name;
                    carToNewList.Price = car.Price;
                    carToNewList.safetyRating = car.safetyRating;
                    carToNewList.modelYear = car.modelYear;
                    newListForRemoval.Add(carToNewList);
                }
            }
            carDB = newListForRemoval;
        }
        
    }
}
