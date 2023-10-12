using GarageExercise.Garage;
using GarageExercise.Validations;

namespace GarageExercise.UI;

public class UiManager
{
    public readonly GarageHandler garageHandler;
    public readonly IUi ui;
    private readonly ConstructVehicle constructVehicle;
    private bool isRunning;
    
    public UiManager(GarageHandler handler, IUi ui)
    {
        garageHandler = handler;
        this.ui = ui;
        constructVehicle = new ConstructVehicle(this, garageHandler);
    }
    public void Run()
    {
        var isValidGarageSize = (false, 0);

        do
        {
            ui.ClearConsole();

            ui.ConsoleMessageWrite("Hi and welcome to the Garage!" +
                                   "\nHow many vehicles do you wanna store in the garage?" +
                                   "\n\nAmount: ");

            isValidGarageSize = Validation.CheckValidNumber(ui, "Invalid garage size. Please enter a number between " +
                                                            $"{GarageHelpers.MinGarageSize} and {GarageHelpers.MaxGarageSize}.\nTry again: ", GarageHelpers.MinGarageSize, GarageHelpers.MaxGarageSize);

            garageHandler.Initialize(sizeOfGarage: isValidGarageSize.Item2);

        } while (!isValidGarageSize.Item1);

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
            var validNumber = Validation.CheckValidNumber(ui);
            MenuChoices(validNumber);

        } while (isRunning);
    }
    private void MenuChoices(int validNumber)
    {
        switch (validNumber)
        {
            case 1:
                PrintResultToConsole("Number Of Parked Vehicles Based On Type!\n"
                    , garageHandler.DisplayVehicleByTypeAndAmount());
                break;
            case 2:
                PrintResultToConsole("Parked Cars Full Specification!\n"
                    , garageHandler.DisplayParkedVehiclesFullInfo());
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
                Environment.Exit(0);
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

        var validNr = Validation.CheckValidNumber(ui, GarageHelpers.MinMenuValue, GarageHelpers.MaxMenuValue); ;

        constructVehicle.AddVehicleProperties(validNr);
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
