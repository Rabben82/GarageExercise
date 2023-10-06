﻿using GarageExercise.Entities;
using GarageExercise.UI;
using GarageExercise.Validations;

namespace GarageExercise.Garage;

internal class ConstructVehicle
{
    private readonly UiManager uiManager;

    public ConstructVehicle(UiManager uiManager)
    {
        this.uiManager = uiManager;
    }
    public void AddVehicleProperties(int validNr)
    {
        uiManager.ui.ConsoleMessageWrite("Enter Vehicle Model: ");
        var model = Validation.CheckValidStringLengthInput(uiManager.ui, GarageHelpers.minModelNameLetters, GarageHelpers.maxModelNameLetters, "model");
        uiManager.ui.ConsoleMessageWrite("Enter Vehicle Registration Number: ");
        var registrationNumber = Validation.CheckRegistrationNumberInput(uiManager.ui);
        uiManager.ui.ConsoleMessageWrite("Enter Color Of Vehicle: ");
        var color = Validation.CheckValidStringLengthInput(uiManager.ui, GarageHelpers.minColorLetters, GarageHelpers.maxColorLetters, "color");
        uiManager.ui.ConsoleMessageWrite("Enter How Many Wheels The Vehicle Has: ");
        var numberOfWheels = Validation.CheckValidNumber(uiManager.ui, GarageHelpers.minWheels, GarageHelpers.maxWheels, "You have entered an invalid number of wheels");
        uiManager.ui.ConsoleMessageWrite("Enter The Production Year Of The Vehicle: ");
        var productionYear = Validation.CheckProductionYearInput(uiManager.ui);

        AddVehiclePropertiesMenu(validNr, model, registrationNumber, color, numberOfWheels, productionYear);
    }
    public void AddVehiclePropertiesMenu(int userSelection, string model, string registrationNumber, string color, int numberOfWheels, int productionYear)
    {
        switch (userSelection)
        {
            case 1:
                uiManager.ui.ConsoleMessageWrite("Enter Fuel Type: ");
                var carFuelType = Validation.CheckCarFuelTypeInput(uiManager.ui);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Car(model, registrationNumber, color, numberOfWheels, productionYear, carFuelType));
                uiManager.ui.WaitForKeyPress();
                break;
            case 2:
                uiManager.ui.ConsoleMessageWrite("Enter Number Of Seats: ");
                var numberOfSeats = Validation.CheckValidNumber(uiManager.ui);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Bus(model, registrationNumber, color, numberOfWheels, productionYear, numberOfSeats));
                uiManager.ui.WaitForKeyPress();
                break;
            case 3:
                uiManager.ui.ConsoleMessageWrite("Enter Horse Power: ");
                var horsePower = uiManager.ui.UserInput();
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Motorcycle(model, registrationNumber, color, numberOfWheels, productionYear, horsePower));
                uiManager.ui.WaitForKeyPress();
                break;
            case 4:
                uiManager.ui.ConsoleMessageWrite("Enter Length: ");
                var length = Validation.CheckValidNumber(uiManager.ui);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new Boat(model, registrationNumber, color, numberOfWheels, productionYear, length));
                uiManager.ui.WaitForKeyPress();
                break;
            case 5:
                uiManager.ui.ConsoleMessageWrite("Enter Number Of Engines: ");
                var airPlaneLength = Validation.CheckValidNumber(uiManager.ui);
                uiManager.ui.ClearConsole();
                uiManager.garageHandler.AddVehicleByUserInput(new AirPlane(model, registrationNumber, color, numberOfWheels, productionYear, airPlaneLength));
                uiManager.ui.WaitForKeyPress();
                break;
            default:
                throw new IndexOutOfRangeException("You Have Chosen A Value That Doesn't Exist.");
        }
    }
}