using GarageExercise.Enums;
using GarageExercise.UI;

namespace GarageExercise.Validations;

public static class Validation
{
    private static int number;
    public static int CheckValidNumber()
    {
        bool isValid = false;

        do
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.Write("Error: Input cannot be blank.\nTry again: ");
            }
            else if (int.TryParse(userInput, out number))
            {
                isValid = true;
            }
            else
            {
                Console.Write("Error: Invalid input. Please enter a valid number.\nTry again: ");
            }
        } while (!isValid);

        return number;
    }
    public static int CheckUserSelection(string prompt)
    {
        Console.Write(prompt);
        return CheckValidNumber();
    }
    public static string CheckModelNameInput(IUi ui)
    {
        string modelName;
        do
        {
            modelName = ui.UserInput();
            if (!IsValidModelName(modelName))
            {
                ui.ConsoleMessageWrite("It's not a valid model name, it should have 3 letters or moore but not bigger than 20" +
                                       "\nTry Again: ");
            }

        } while (!IsValidModelName(modelName));

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
    public static string CheckColorInput(IUi ui)
    {
        string color;
        do
        {
            color = ui.UserInput();
            if (!IsValidColor(color))
            {
                ui.ConsoleMessageWrite("It's not a valid color, it needs to be bigger than 2 letters but not longer than 25." +
                                       "\nTry again: ");
            }

        } while (!IsValidColor(color));

        return color;
    }
    public static int CheckNumberOfWheelsInput(IUi ui)
    {
        int numberOfWheels;
        do
        {
            numberOfWheels = CheckValidNumber();
            if (!IsValidNumberOfWheels(numberOfWheels))
            {
                ui.ConsoleMessageWrite("You have entered an invalid number of wheels, it can't be bigger than 20" +
                                       "\nTry again: ");
            }
        } while (!IsValidNumberOfWheels(numberOfWheels));

        return numberOfWheels;
    }
    public static int CheckProductionYearInput(IUi ui)
    {
        int productionYear;
        do
        {
            productionYear = CheckValidNumber();
            if (!IsValidProductionYear(productionYear))
            {
                ui.ConsoleMessageWrite("It's not an valid production year, it should look like this (1990)" +
                                       "\nTry again: ");
            }
        } while (!IsValidProductionYear(productionYear));

        return productionYear;
    }
    public static bool IsValidModelName(string model)
    {
        bool isValidName = model.Length >= 3 && model.Length < 20;

        return isValidName;
    }
    public static bool IsValidRegistrationNumber(string value)
    {

        bool isFirstThreeCharsLetters = value
            .Take(3)
            .All(char.IsLetter);
        bool isLastThreeCharsNumber = value
            .TakeLast(3)
            .All(char.IsDigit);

        return isFirstThreeCharsLetters && isLastThreeCharsNumber;
    }
    public static bool IsValidColor(string color)
    {
        bool isValidColor = color.Length >= 2 && color.Length < 25;
        return isValidColor;
    }
    public static bool IsValidNumberOfWheels(int numberOfWheels)
    {
        bool isValidWheels = numberOfWheels < 20;
        return isValidWheels;
    }
    public static bool IsValidProductionYear(int productionYear)
    {
        bool isValidProductionYear = productionYear.ToString().Length == 4;
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
        List<FuelTypes> fuelTypes = Enum.GetValues(typeof(FuelTypes)).Cast<FuelTypes>().ToList(); //cast enum to list

        return fuelTypes.Any(enumFuelType =>
            enumFuelType.ToString().Equals(fuelType, StringComparison.OrdinalIgnoreCase));
    }
    
}