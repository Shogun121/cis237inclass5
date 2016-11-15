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
            //*************************
            //Add a new car to the database
            //*************************

            //Make an instance of a new car
            Car newCarToAdd = new Car();
            //Assign properties to the parts of the model.
            newCarToAdd.id = "88888";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "GT-R";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2016";
            newCarToAdd.type = "Car";

            try
            {
                //Add the new car to the Cars Collection
                carsRCooleyEnties.Cars.Add(newCarToAdd);

                //Persist the collection to the database.
                //This call will actually do the work of saving the changes to the database.
                carsRCooleyEnties.SaveChanges();
            }
            catch(Exception e)
            {
                //Remove the new car from the Cars Collection since we can't save it.
                carsRCooleyEnties.Cars.Remove(newCarToAdd);

                //This catch might get thrown for other reasons than a primary key error.
                //Here I am assuming that.
                Console.WriteLine("Can't add the record. Already have one with that primary key");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new car. Going to fetch and print to verify");
            carToFind = carsRCooleyEnties.Cars.Find("88888");
            Console.WriteLine(carToFind.id+ " "+ carToFind.make+" "+ carToFind.model+" ");

            //***********************
            //Update a record
            //***********************

            //Get out the car we want to update
            Car carToFindForUpdate = carsRCooleyEnties.Cars.Find("88888");

            //Output the car to find before the update.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("About to do an update on the following car");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model + " ");

            Console.WriteLine("Doing the update now?");

            //Update some of the properties of the new car we found. We don't have to update all of the fields if we don't want to.
            carToFindForUpdate.make = "Nissssssan";
            carToFindForUpdate.model = "Gt-ARRRRRRRRGGGGGGGGGHHHH";
            carToFindForUpdate.horsepower = 1845;
            carToFindForUpdate.cylinders = 16;

            //Save the changes to the database. Since when we pulled out the one to update
            //all we were really doing was getting a reference to the one in the collection we wanted to
            //update. There is no need to 'put' the car back into the Cars collection.
            //It is still there. All we have to do is save the changes.

            carsRCooleyEnties.SaveChanges();

            //Get the car out now that it has been updated to verify the update worked.
            carToFindForUpdate = carsRCooleyEnties.Cars.Find("88888");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model + " ");

            //OUtput the updated Car
            Console.WriteLine("Outputing the updated car");

            //***********************
            //How to Delete a record
            //***********************

            //Get a car out of the database that we would like to delete.
            Car carToFindForDelete = carsRCooleyEnties.Cars.Find("88888");

            //Remove the car
            carsRCooleyEnties.Cars.Remove(carToFindForDelete);

            //Save the changes to the database.
            carsRCooleyEnties.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deleted the added car. Looking to see if it is still in the DB");

            //Check to see if the car was deleted
            try
            {
                //This statement will execute just fine. It's when we go to access the property
                //id on null that the exception will get thrown.
                carToFindForDelete = carsRCooleyEnties.Cars.Find("88888");
                Console.WriteLine(carToFindForDelete.id + " " + carToFind.make + " " + carToFind.model + " ");
            }
            catch(Exception e)
            {
                Console.WriteLine("The model you are looking for does not exist" + e.ToString()+ " "+ e.StackTrace);
            }

            //Another way to check to see if the record has been deleted
            if(carToFindForDelete==null)
            {
                Console.WriteLine("The model you are looking for does not exist");
            }
        }
    }
}
