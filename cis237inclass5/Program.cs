using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237inclass5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Make an instance of the entities class.
            CarsRCooleyEntities carsRCooleyEnties = new CarsRCooleyEntities();

            //****************************************
            //List out all of the cars in the table.
            //****************************************
            Console.WriteLine("Print the list");

            //"Cars" is a single database table
            foreach (Car car in carsRCooleyEnties.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);

           
            }
            //******************************************
            //Find a specific one by the primary key.
            //******************************************

            //Pull out a car from the table based on te id which is is the primary key.
            //IF the record doesn't exist in the database, it will return null, so
            //check what you get back and see if it is null. If so, it doesn't exist.
            Car foundCar = carsRCooleyEnties.Cars.Find("V0LCD1814");
            //Select * from Cars where id = "V0LCD1814"
            //Print it out
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Print out a found car using the Find Method");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);

            //******************************************************************
            //Find a specific one by any property
            //******************************************************************

            //Call the Where Method on the table 'Cars' and pass in a lambda expression
            //for the criteria we are looking for. There is nothing special about the word car
            // in the part that reads: cart=> carid => "V0...". It could be any characters we want
            //it to be. It is just a variable name for the current car we are considering in the expression.
            //This will automagically loop through all the Cars, and run the expression against each of them.
            //When the result is finally true, it will return that car.
            Car carToFind = carsRCooleyEnties.Cars.Where(
                car => car.id == "V0LCD1814"    //Lambda expression(mini method)
                ).First();
            //Select * from Cars where id = "V0LCD1814"
            Car otherCartoFind = carsRCooleyEnties.Cars.Where(
                car => car.model == "Challenger"
                ).First();
            //Select * from Cars where model = "Challenger"
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find two specific cars");
            Console.WriteLine(carToFind.id+ " "+ carToFind.make+ " "+ carToFind.model);
            Console.WriteLine(otherCartoFind.id + " " + otherCartoFind.make + " " + otherCartoFind.model);

            //**************************************************
            //Get out multiple cars
            //**************************************************
            List<Car> queryCars = carsRCooleyEnties.Cars.Where(
                car => car.cylinders == 8
                ).ToList();
            //Select * from Cars where cylinders = "8"
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find all cars with 8 cylinders");

            foreach(Car car in queryCars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }
            
        }
    }
}
