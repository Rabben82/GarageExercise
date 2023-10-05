using GarageExercise.Entities;
using GarageExercise.Garage;
using GarageExercise.Validations;

namespace GarageExercise.UI
{
    internal class UiManager
    {
        private readonly GarageHandler garageHandler;
        private readonly IUi ui;
        private bool isRunning;
        private const int MinGarageSize = 3;
        private const int MaxGarageSize = 15;
        public UiManager(GarageHandler handler, IUi ui)
        {
            garageHandler = handler;
            this.ui = ui;
        }
        public void Run()
        {
            bool isValidGarageSize = false;

            do
            {
                ui.ClearConsole();

                ui.ConsoleMessageWrite("Hi and welcome to the Garage!" +
                                       "\nHow many vehicles do you wanna store in the garage?" +
                                       "\n\nAmount: ");

                var validNumber = Validation.CheckValidNumber();

                if (validNumber > MinGarageSize &&  validNumber < MaxGarageSize)
                {
                    isValidGarageSize = true;
                    garageHandler.Initialize(validNumber);
                }
                else
                {
                    ui.ConsoleMessageWriteLine("The Garage need's to hold moore than 3 parking-spots, but can't be bigger than 15.\nPress any key to try again!");
                    ui.WaitForKeyPress();
                }
            } while (!isValidGarageSize);

            Menu();
        }
        public void Menu()
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
                var validNumber = Validation.CheckValidNumber();
                MenuChoices(validNumber);

            } while (isRunning);
        }
        private void MenuChoices(int validNumber)
        {
            switch (validNumber)
            {
                case 1:
                    PrintResultToConsole("Number Of Parked Vehicles Based On Type!\n", garageHandler.ShowVehicleTypeAmount());
                    break;
                case 2:
                    PrintResultToConsole("Parked Cars Full Specification!\n", garageHandler.DisplayParkedVehicles());
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
                    ui.ClearConsole();
                    ui.ConsoleMessageWrite("Enter The Registration Number You Want to Search For.\nEnter: ");
                    var registrationNumber = Validation.CheckRegistrationNumberInput(ui);
                    garageHandler.SearchByRegistrationNumber(registrationNumber);
                    ui.WaitForKeyPress();
                    break;
                case 6:
                    ui.ClearConsole();
                    ui.ConsoleMessageWriteLine("Enter Some Properties You Want to Search For (e.g., 'black 4 wheels'):");
                    garageHandler.SearchByProperties(ui.UserInput().Split(' '));
                    ui.WaitForKeyPress();
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

            var validNr = Validation.CheckValidNumber();
            if (validNr is >= 1 and <= 5)
            {
                AddVehicleProperties(validNr);
            }
            else
            {
                ui.ConsoleMessageWriteLine("You Have Entered A Value That's Out Of Range!" +
                                           "\nPress any key to continue!");
                ui.WaitForKeyPress();
            }
        }
        public void AddVehicleProperties(int userSelectionA)
        {
            ui.ConsoleMessageWrite("Enter Vehicle Model: ");
            var model = Validation.CheckModelNameInput(ui);
            ui.ConsoleMessageWrite("Enter Vehicle Registration Number: ");
            var registrationNumber = Validation.CheckRegistrationNumberInput(ui);
            ui.ConsoleMessageWrite("Enter Color Of Vehicle: ");
            var color = Validation.CheckColorInput(ui);
            ui.ConsoleMessageWrite("Enter How Many Wheels The Vehicle Has: ");
            var numberOfWheels = Validation.CheckNumberOfWheelsInput(ui);
            ui.ConsoleMessageWrite("Enter The Production Year Of The Vehicle: ");
            var productionYear = Validation.CheckProductionYearInput(ui);

            switch (userSelectionA)
            {
                case 1:
                    ui.ConsoleMessageWrite("Enter Fuel Type: ");
                    var carFuelType = Validation.CheckCarFuelTypeInput(ui);
                    ui.ClearConsole();
                    garageHandler.AddVehicleByUserInput(new Car(model, registrationNumber, color, numberOfWheels, productionYear, carFuelType));
                    ui.WaitForKeyPress();
                    break;
                case 2:
                    ui.ConsoleMessageWrite("Enter Number Of Seats: ");
                    var numberOfSeats = Validation.CheckValidNumber();
                    ui.ClearConsole();
                    garageHandler.AddVehicleByUserInput(new Bus(model, registrationNumber, color, numberOfWheels, productionYear, numberOfSeats));
                    ui.WaitForKeyPress();
                    break;
                case 3:
                    ui.ConsoleMessageWrite("Enter Horse Power: ");
                    var horsePower = ui.UserInput();
                    ui.ClearConsole();
                    garageHandler.AddVehicleByUserInput(new Motorcycle(model, registrationNumber, color, numberOfWheels, productionYear, horsePower));
                    ui.WaitForKeyPress();
                    break;
                case 4:
                    ui.ConsoleMessageWrite("Enter Length: ");
                    var length = Validation.CheckValidNumber();
                    ui.ClearConsole();
                    garageHandler.AddVehicleByUserInput(new Boat(model, registrationNumber, color, numberOfWheels, productionYear, length));
                    ui.WaitForKeyPress();
                    break;
                case 5:
                    ui.ConsoleMessageWrite("Enter Number Of Engines: ");
                    var airPlaneLength = Validation.CheckValidNumber();
                    ui.ClearConsole();
                    garageHandler.AddVehicleByUserInput(new AirPlane(model, registrationNumber, color, numberOfWheels, productionYear, airPlaneLength));
                    ui.WaitForKeyPress();
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
