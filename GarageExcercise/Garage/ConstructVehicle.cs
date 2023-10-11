using GarageExercise.Entities;
using GarageExercise.Enums;
using GarageExercise.UI;
using GarageExercise.Validations;

namespace GarageExercise.Garage;

internal class ConstructVehicle
{
    private readonly UiManager uiManager;
    private readonly GarageHandler garageHandler;
    private int numberOfWheels;
    public ConstructVehicle(UiManager uiManager, GarageHandler garageHandler)
    {
        this.uiManager = uiManager;
        this.garageHandler = garageHandler;
    }
    public void AddVehicleProperties(int validNr)
    {
        
        uiManager.ui.ConsoleMessageWrite("Enter Vehicle Model: ");
        var model = Validation.CheckValidStringLengthInput(uiManager.ui, GarageHelpers.minModelNameLetters, GarageHelpers.maxModelNameLetters, "model");
        uiManager.ui.ConsoleMessageWrite("Enter Vehicle Registration Number: ");
        var registrationNumber =Validation.CheckValidRegistrationNumber(uiManager, uiManager.ui, garageHandler);

        uiManager.ui.ConsoleMessageWrite("Enter Color Of Vehicle: ");
        var color = Validation.CheckValidStringLengthInput(uiManager.ui, GarageHelpers.minColorLetters, GarageHelpers.maxColorLetters, "color");
        if (validNr >= GarageHelpers.vehiclesWithWheelsIndexStart && validNr <= GarageHelpers.vehiclesWithWheelsIndexEnd)//if vehicles without wheels it skips and the user don't need to enter wheels property
        {
            uiManager.ui.ConsoleMessageWrite("Enter How Many Wheels The Vehicle Has: ");
            numberOfWheels = Validation.CheckValidNumber(uiManager.ui, GarageHelpers.minWheels, GarageHelpers.maxWheels, "You have entered an invalid number of wheels");
        }
        uiManager.ui.ConsoleMessageWrite("Enter The Production Year Of The Vehicle: ");
        var productionYear = Validation.CheckProductionYearInput(uiManager.ui);

        AddVehiclePropertiesMenu(validNr, model, registrationNumber, color, numberOfWheels, productionYear);
    }
    public void AddVehiclePropertiesMenu(int userSelection, string model, string registrationNumber, string color, int numberOfWheels, int productionYear)
    {
        switch (userSelection)
        {
            case (int)VehicleTypes.Car:
                uiManager.ui.ConsoleMessageWrite("Enter Fuel Type: ");
                var carFuelType = Validation.CheckCarFuelTypeInput(uiManager.ui);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Car(model, registrationNumber, color, numberOfWheels, productionYear, carFuelType));
                uiManager.ui.WaitForKeyPress();
                break;
            case (int)VehicleTypes.Bus:
                uiManager.ui.ConsoleMessageWrite("Enter Number Of Seats: ");
                var numberOfSeats = Validation.CheckValidNumber(uiManager.ui, GarageHelpers.minNrOfSeats, GarageHelpers.maxNrOfSeats);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Bus(model, registrationNumber, color, numberOfWheels, productionYear, numberOfSeats));
                uiManager.ui.WaitForKeyPress();
                break;
            case (int)VehicleTypes.Motorcycle:
                uiManager.ui.ConsoleMessageWrite("Enter Horse Power: ");
                var horsePower = Validation.CheckValidStringLengthInput(uiManager.ui, GarageHelpers.minHoursePowerLength, GarageHelpers.maxHoursePowerLength,
                    "length for horse power");
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Motorcycle(model, registrationNumber, color, numberOfWheels, productionYear, horsePower));
                uiManager.ui.WaitForKeyPress();
                break;
            case (int)VehicleTypes.Boat:
                uiManager.ui.ConsoleMessageWrite("Enter Length: ");
                var length = Validation.CheckValidNumber(uiManager.ui, GarageHelpers.minLength,GarageHelpers.maxLength, "You don't need to specify the length with decimal, it accepts whole numbers!");
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Boat(model, registrationNumber, color, numberOfWheels = 0, productionYear, length));
                uiManager.ui.WaitForKeyPress();
                break;
            case (int)VehicleTypes.Airplane:
                uiManager.ui.ConsoleMessageWrite("Enter Number Of Engines: ");
                var airPlaneLength = Validation.CheckValidNumber(uiManager.ui,GarageHelpers.minNrOfEngines,GarageHelpers.maxNrOfEngines);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new AirPlane(model, registrationNumber, color, numberOfWheels = 0, productionYear, airPlaneLength));
                uiManager.ui.WaitForKeyPress();
                break;
            default:
                throw new IndexOutOfRangeException("You Have Chosen A Value That Doesn't Exist.");
        }
    }
    
}