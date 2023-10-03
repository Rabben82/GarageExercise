using GarageExercise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageExercise
{
    internal class Manager
    {
        private GarageHandler garageHandler;
        public Manager(GarageHandler handler)
        {
            garageHandler = handler;
        }

        private int validNumber;

        public void Run()
        {
            UserInterface.ConsoleMessageWrite("Hi and welcome to the Garage!" +
                                         "\nHow many vehicles do you wanna store in the garage?" +
                                         "\n\nAmount: ");

            validNumber = UserInterface.ReturnValidNumber();
            garageHandler.Initialize(validNumber);

            Menu();

        }
        private void Menu()
        {
            do
            {
                UserInterface.ClearConsole();
                UserInterface.ConsoleMessageWrite("What do you wanna do today?\n" +
                                             "\n1. List vehicle types and see how many of each type are currently parked." +
                                             "\n2. List Parked Cars." +
                                             "\n3. Remove Vehicle." +
                                             "\n4. Add Vehicle." +
                                             "\n5. Search Vehicle By Registration Number." +
                                             "\n6. Search Vehicle Based On Different Properties." +
                                             "\n\nEnter Your Choice: ");
                validNumber = UserInterface.ReturnValidNumber();
                MenuChoices();

            } while (true);
        }

        private void MenuChoices()
        {
            switch (validNumber)
            {
                case 1:
                    UserInterface.ClearConsole();
                    UserInterface.ConsoleMessageWrite("Number Of Parked Vehicles Based On Type!\n\n");
                    var numberOfCars = garageHandler.GetNrOfCarsInGarage();
                    PrintResultToConsole(numberOfCars);
                    UserInterface.WaitForKeyPress();
                    break;
                case 2:
                    UserInterface.ClearConsole();
                    UserInterface.ConsoleMessageWrite("Parked Cars Full Specification!\n\n");
                    var listCars = garageHandler.ListCarsWithoutDuplicates();
                    PrintResultToConsole(listCars);
                    UserInterface.WaitForKeyPress();
                    break;
                case 3:
                    UserInterface.ClearConsole();
                    UserInterface.ConsoleMessageWrite("Which Vehicle Do You Wanna Remove?\n\n");
                    garageHandler.RemoveVehicle();
                    UserInterface.WaitForKeyPress();
                    break;
                case 4:
                    UserInterface.ClearConsole();
                    AddVehicleMenu();
                    UserInterface.WaitForKeyPress();
                    break;
                case 5:
                    UserInterface.ClearConsole();
                    UserInterface.ConsoleMessageWrite("Enter The Registration Number You Wanna Search For." +
                                                      "\nEnter: ");
                    var userInput = UserInterface.UserInput();
                    garageHandler.SearchByRegistrationNumber(userInput);
                    UserInterface.WaitForKeyPress();
                    break;
                case 6:
                    UserInterface.ClearConsole();
                    UserInterface.ConsoleMessageWriteLine("Enter Some Properties You Wanna Search For, example(all vehicles with black color and 4 wheels)");
                    var prop = UserInterface.UserInput();
                    string[] splitString = prop.Split(" ");
                    garageHandler.SearchByProperties(splitString);
                    UserInterface.WaitForKeyPress();
                    break;
                default:
                    Console.WriteLine("Not on the list");
                    break;
            }
        }
        public void AddVehicleMenu()
        {
            UserInterface.ConsoleMessageWrite("What Type Of Vehicle Is To Be Added?\n" +
                                         "\n1. Car" +
                                         "\n2. Bus" +
                                         "\n3. Motorcycle" +
                                         "\n4. Boat" +
                                         "\n5. Airplane" +
                                         "\n\nEnter Your Choice: ");

            var validNr = UserInterface.ReturnValidNumber();

            AddVehicleMenuChoices(validNr);

        }
        private void AddVehicleMenuChoices(int validNr)
        {
            switch (validNr)
            {
                case 1:
                    AddVehicleProperties(validNr);
                    break;
                case 2:
                    AddVehicleProperties(validNr);
                    break;
                case 3:
                    AddVehicleProperties(validNr);
                    break;
                case 4:
                    AddVehicleProperties(validNr);
                    break;
                case 5:
                    AddVehicleProperties(validNr);
                    break;
                default:
                    throw new IndexOutOfRangeException("You Have Entered A Value That's Out Of Range!");
                    
            }
        }
        public void AddVehicleProperties(int userSelectionA)
        {
            UserInterface.ConsoleMessageWrite("Enter Vehicle Model: ");
            var model = UserInterface.UserInput();
            UserInterface.ConsoleMessageWrite("Enter Vehicle Registration Number: ");
            var registrationNumber = UserInterface.UserInput();
            UserInterface.ConsoleMessageWrite("Enter Color Of Vehicle: ");
            var color = UserInterface.UserInput();
            UserInterface.ConsoleMessageWrite("Enter How Many Wheels The Vehicle Has: ");
            var numberOfWheels = UserInterface.ReturnValidNumber();
            UserInterface.ConsoleMessageWrite("Enter The Production Year Of The Vehicle: ");
            var productionYear = UserInterface.ReturnValidNumber();

            switch (userSelectionA)
            {
                case 1:
                    UserInterface.ConsoleMessageWrite("Enter Fuel Type: ");
                    var carFuelType = UserInterface.UserInput();
                    Car newCar = new Car(model, registrationNumber, color, numberOfWheels, productionYear, carFuelType);
                    garageHandler.AddVehicle(newCar);
                    break;
                case 2:
                    UserInterface.ConsoleMessageWrite("Enter Number Of Seats: ");
                    var numberOfSeats = UserInterface.ReturnValidNumber();
                    Bus newBus = new Bus(model, registrationNumber, color, numberOfWheels, productionYear, numberOfSeats);
                    garageHandler.AddVehicle(newBus);
                    break;
                case 3:
                    UserInterface.ConsoleMessageWrite("Enter Horse Power: ");
                    var horsePower = UserInterface.UserInput();
                    Motorcycle newMotorcycle = new Motorcycle(model, registrationNumber, color, numberOfWheels, productionYear, horsePower);
                    garageHandler.AddVehicle(newMotorcycle);
                    break;
                case 4:
                    UserInterface.ConsoleMessageWrite("Enter Length: ");
                    var length = UserInterface.ReturnValidNumber();
                    Boat newBoat = new Boat(model, registrationNumber, color, numberOfWheels, productionYear, length);
                    garageHandler.AddVehicle(newBoat);
                    break;
                case 5:
                    UserInterface.ConsoleMessageWrite("Enter Number Of Engines: ");
                    var airPlaneLength = UserInterface.ReturnValidNumber();
                    AirPlane newAirPlane = new AirPlane(model, registrationNumber, color, numberOfWheels, productionYear,airPlaneLength);
                    garageHandler.AddVehicle(newAirPlane);
                    break;
                default:
                    throw new IndexOutOfRangeException("You Have Chosen A Value That Doesn't Exist.");
            }
        }
        public void PrintResultToConsole(IEnumerable<string> result)
        {
            foreach (var objects in result)
            {
                UserInterface.ConsoleMessageWriteLine(objects);
            }
        }
    }
}
