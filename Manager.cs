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
        private readonly GarageHandler garageHandler;
        private readonly IUi ui;
        private bool isRunning;
        public Manager(GarageHandler handler, IUi ui)
        {
            garageHandler = handler;
            this.ui = ui;
        }

        public void Run()
        {
            ui.ConsoleMessageWrite("Hi and welcome to the Garage!" +
                                         "\nHow many vehicles do you wanna store in the garage?" +
                                         "\n\nAmount: ");

            var validNumber = ui.ReturnValidNumber();
            garageHandler.Initialize(validNumber);

            Menu();

        }
        private void Menu()
        {
            do
            {
                isRunning = true;

                ui.ClearConsole();
                ui.ConsoleMessageWrite("What do you wanna do today?\n" +
                                             "\n1. List vehicle types and see how many of each type are currently parked." +
                                             "\n2. List Parked Cars." +
                                             "\n3. Remove Vehicle." +
                                             "\n4. Add Vehicle." +
                                             "\n5. Search Vehicle By Registration Number." +
                                             "\n6. Search Vehicle Based On Different Properties." +
                                             "\n7. Quit!" +
                                             "\n\nEnter Your Choice: ");
                var validNumber = ui.ReturnValidNumber();
                MenuChoices(validNumber);

            } while (isRunning);
        }

        private void MenuChoices(int validNumber)
        {
            switch (validNumber)
            {
                case 1:
                    PrintResultToConsole("Number Of Parked Vehicles Based On Type!\n", garageHandler.GetTypeOfVehicleInGarage());
                    break;
                case 2:
                    PrintResultToConsole("Parked Cars Full Specification!\n", garageHandler.ListParkedCars());
                    break;
                case 3:
                    ui.ClearConsole();
                    garageHandler.RemoveVehicle();
                    ui.WaitForKeyPress();
                    break;
                case 4:
                    ui.ClearConsole();
                    AddVehicleMenu();
                    break;
                case 5:
                    ui.ConsoleMessageWrite("Enter The Registration Number You Want to Search For.\nEnter: ");
                    garageHandler.SearchByRegistrationNumber(ui.UserInput());
                    break;
                case 6:
                    ui.ConsoleMessageWriteLine("Enter Some Properties You Want to Search For (e.g., 'black 4 wheels'):");
                    garageHandler.SearchByProperties(ui.UserInput().Split(' '));
                    break;
                case 7:
                    isRunning = false;
                    ui.ConsoleMessageWriteLine("Goodbye And Welcome Back!");
                    break;
                default:
                    ui.ConsoleMessageWriteLine("The Value You Have Entered Is Not Represented In The Menu" +
                                               "\nPress Any Button To Try Again!");
                    ui.WaitForKeyPress();
                    break;

            }
        }
        public void AddVehicleMenu()
        {
            ui.ConsoleMessageWrite("What Type Of Vehicle Is To Be Added?\n" +
                                         "\n1. Car" +
                                         "\n2. Bus" +
                                         "\n3. Motorcycle" +
                                         "\n4. Boat" +
                                         "\n5. Airplane" +
                                         "\n\nEnter Your Choice: ");

            var validNr = ui.ReturnValidNumber();
            if (validNr is >= 1 and <= 5)
            {
                AddVehicleProperties(validNr);
            }
            else
            {
                ui.ConsoleMessageWriteLine("You Have Entered A Value That's Out Of Range!" +
                                           "\nPress Any Key To Return To Menu!");
                ui.WaitForKeyPress();
            }
        }
        public void AddVehicleProperties(int userSelectionA)
        {
            ui.ConsoleMessageWrite("Enter Vehicle Model: ");
            var model = ui.UserInput();
            ui.ConsoleMessageWrite("Enter Vehicle Registration Number: ");
            var registrationNumber = ui.UserInput();
            ui.ConsoleMessageWrite("Enter Color Of Vehicle: ");
            var color = ui.UserInput();
            ui.ConsoleMessageWrite("Enter How Many Wheels The Vehicle Has: ");
            var numberOfWheels = ui.ReturnValidNumber();
            ui.ConsoleMessageWrite("Enter The Production Year Of The Vehicle: ");
            var productionYear = ui.ReturnValidNumber();

            switch (userSelectionA)
            {
                case 1:
                    ui.ConsoleMessageWrite("Enter Fuel Type: ");
                    var carFuelType = ui.UserInput();
                    garageHandler.AddVehicle(new Car(model, registrationNumber, color, numberOfWheels, productionYear, carFuelType));
                    break;
                case 2:
                    ui.ConsoleMessageWrite("Enter Number Of Seats: ");
                    var numberOfSeats = ui.ReturnValidNumber();
                    garageHandler.AddVehicle(new Bus(model, registrationNumber, color, numberOfWheels, productionYear, numberOfSeats));
                    break;
                case 3:
                    ui.ConsoleMessageWrite("Enter Horse Power: ");
                    var horsePower = ui.UserInput();
                    garageHandler.AddVehicle(new Motorcycle(model, registrationNumber, color, numberOfWheels, productionYear, horsePower));
                    break;
                case 4:
                    ui.ConsoleMessageWrite("Enter Length: ");
                    var length = ui.ReturnValidNumber();
                    garageHandler.AddVehicle(new Boat(model, registrationNumber, color, numberOfWheels, productionYear, length));
                    break;
                case 5:
                    ui.ConsoleMessageWrite("Enter Number Of Engines: ");
                    var airPlaneLength = ui.ReturnValidNumber();
                    garageHandler.AddVehicle(new AirPlane(model, registrationNumber, color, numberOfWheels, productionYear, airPlaneLength));
                    break;
                default:
                    throw new IndexOutOfRangeException("You Have Chosen A Value That Doesn't Exist.");
            }
        }
        public void PrintResultToConsole(string prompt, IEnumerable<string> result)
        {
            ui.ClearConsole();
            ui.ConsoleMessageWriteLine(prompt);

            foreach (var objects in result)
            {
                ui.ConsoleMessageWriteLine(objects);
            }

            ui.WaitForKeyPress();
        }
    }
}
