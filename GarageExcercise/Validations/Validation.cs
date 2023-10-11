using System.Runtime.InteropServices.JavaScript;
using GarageExercise.Entities;
using GarageExercise.Enums;
using GarageExercise.Garage;
using GarageExercise.UI;

namespace GarageExercise.Validations;

public static class Validation
{
    private static int number;
    //this just parse a string to a number
    public static int CheckValidNumber(IUi ui, string prompt ="")
    {
        bool isValid = false;
        ui.ConsoleMessageWriteLine(prompt);

        do
        {
            string userInput = Console.ReadLine() ?? throw new ArgumentException("Error: Value can't be blank!");

            if (string.IsNullOrWhiteSpace(userInput))
            {
                ui.ConsoleMessageWrite("Error: Input cannot be blank.\nTry again: ");
            }
            else if (int.TryParse(userInput, out number))
            {
                isValid = true;
            }
            else
            {
                ui.ConsoleMessageWrite("Error: Invalid input. Please enter a valid number.\nTry again: ");
            }
        } while (!isValid);

        return number;
    }
    //this parse a string to a valid number, but checks if its valid between a given range
    public static int CheckValidNumber(IUi ui,int minValue, int maxValue, string prompt= "You have entered a value that's out of range")
    {
        bool isValid = false;

        do
        {
            string userInput = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(userInput))
            {
                ui.ConsoleMessageWrite($"Error: Input cannot be blank.\nTry again: ");
            }
            else if (int.TryParse(userInput, out number) && number >= minValue && number <= maxValue)
            {
                isValid = true;
            }
            else
            {
                ui.ConsoleMessageWrite($"{prompt}, needs to be between {minValue} - {maxValue}" +
                                       "\nTry again: ");
            }
        } while (!isValid);

        return number;
    }
    //sam as above but this method returns both an int and a bool
    public static (bool, int) CheckValidNumber(IUi ui, string prompt, int minValue, int maxValue)
    {
        bool isValid = false;

        do
        {
            string userInput = ui.UserInput();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                ui.ConsoleMessageWrite($"Error: Input cannot be blank.\nTry again: ");
            }
            else if (int.TryParse(userInput, out number) && number >= minValue && number <= maxValue)
            {
                isValid = true;
            }
            else
            {
                ui.ConsoleMessageWrite(prompt);
            }
        } while (!isValid);

        return (isValid, number);
    }
    public static string CheckValidStringLengthInput(IUi ui, int minValue, int maxValue, string prompt)
    {
        string modelName;
        bool isValid = false;
        do
        {
            modelName = ui.UserInput();
            if (modelName.Length < minValue || modelName.Length > maxValue)
            {
                ui.ConsoleMessageWrite($"It's not a valid {prompt} name, it should have {minValue} letters or moore but not bigger than {maxValue}" +
                                       "\nTry Again: ");
            }
            else
            {
                isValid = true;
            }

        } while (!isValid);

        return modelName;
    }
    public static string CheckRegistrationNumberInput(IUi ui)
    {
        string registrationNumber;
        do
        {
            registrationNumber = ui.UserInput();
            if (!IsValidRegistrationNumber(registrationNumber))
            {
                ui.ConsoleMessageWrite("It's not a valid registration number, it should look like this (abc 123)" +
                                       "\nTry Again: ");
            }
        } while (!IsValidRegistrationNumber(registrationNumber));

        return registrationNumber;
    }
    public static int CheckProductionYearInput(IUi ui)
    {
        int productionYear;
        do
        {
            productionYear = CheckValidNumber(ui);
            if (!IsValidProductionYear(productionYear))
            {
                ui.ConsoleMessageWrite($"It's not an valid production year, it should look like this (1990) and not be earlier than 1940 and later than {DateTime.Today.Year}!" +
                                       "\nTry again: ");
            }
        } while (!IsValidProductionYear(productionYear));

        return productionYear;
    }
    public static string CheckValidRegistrationNumber(UiManager manager, IUi ui, GarageHandler garageHandler)
    {
        var registrationNumber = Validation.CheckRegistrationNumberInput(ui);
        bool isRegDuplicate = garageHandler.IsRegNumberExisting(registrationNumber);

        if (isRegDuplicate)
        {
            ui.ConsoleMessageWriteLine($"Vehicle with regnr: {registrationNumber} already exists." +
                                                 $"\nCan't add new vehicle, press any key to return to menu!");
            ui.WaitForKeyPress();
            manager.Menu();
        }

        return registrationNumber;
    }
    public static bool IsValidRegistrationNumber(string registrationNumber)
    {

        bool isFirstThreeCharsLetters = registrationNumber
            .Take(3)
            .All(char.IsLetter);
        bool isLastThreeCharsNumber = registrationNumber
            .TakeLast(3)
            .All(char.IsDigit);

        return isFirstThreeCharsLetters && isLastThreeCharsNumber;
    }
    public static bool IsValidProductionYear(int productionYear)
    {
        

        bool isValidProductionYear = productionYear.ToString().Length == 4 && productionYear >= GarageHelpers.minProductionYear && productionYear <= GarageHelpers.maxProductionYear;
        return isValidProductionYear;
    }
    public static string CheckCarFuelTypeInput(IUi ui)
    {
        string carFuelType;

        do
        {
            carFuelType = ui.UserInput();
            if (!IsValidFuelType(carFuelType))
            {
                ui.ConsoleMessageWrite("You haven't entered a valid fuel type, valid types are (gasoline, diesel. methanol)" +
                                   "\nTry again: ");
            }

        } while (!IsValidFuelType(carFuelType));

        return carFuelType;
    }
    public static bool IsValidFuelType(string fuelType)
    {
        return Enum.TryParse(fuelType.ToLower(), out FuelTypes _);
    }
    public static bool IsParkingSlotInRange(int slotIndex, Garage<Vehicle> garage)
    {
        bool isParkingSlotInRange = slotIndex >= 0 && slotIndex < garage.ToArray().Length;

        return isParkingSlotInRange;
    }
    public static bool IsValueInRange(int validNumber, Garage<Vehicle> garage)
    {
        bool isValid = validNumber >= 1 && validNumber <= garage.ToArray().Length;

        return isValid;
    }
}