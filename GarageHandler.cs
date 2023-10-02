using GarageExercise.Entities;

namespace GarageExercise;

public class GarageHandler
{
    public Garage<Vehicle> garage;
    //private bool getName = true;
    private int validNumber;

    public void Run()
    {
        UserInterface.ConsoleMessage("Hi and welcome to the Garage!" +
                                     "\nHow many cars do you wanna store in the garage?");

        validNumber = UserInterface.ReturnValidNumber();
        Initialize(validNumber);

        UserInterface.ClearConsole();

        UserInterface.ConsoleMessage("What do you wanna do today?\n" +
                                     "\n1. List vehicle types and see how many of each type are currently parked." +
                                     "\n2. List Parked Cars.");

        validNumber = UserInterface.ReturnValidNumber();

        Menu();
    }

    private void Menu()
    {
        switch (validNumber)
        {
            case 1:
                UserInterface.ClearConsole();
                UserInterface.ConsoleMessage("Number Of Parked Vehicles Based On Type!\n");

                var numberOfCars = GetNrOfCarsInGarage();
                PrintResultToConsole(numberOfCars);
                break;
            case 2:
                UserInterface.ClearConsole();
                UserInterface.ConsoleMessage("Parked Cars Full Specification!\n");

                var listCars = ListCarsWithoutDuplicates();
                PrintResultToConsole(listCars);
                break;
            default:
                Console.WriteLine("Not on the list");
                break;
        }
    }

    public void PrintResultToConsole(IEnumerable<string> result)
    {
        foreach (var objects in result)
        {
            UserInterface.ConsoleMessage(objects);
        }
    }

    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);
        Bus bus = new Bus("Mercedes", "abc1202", "grey", 6, 1978);
        Car car = new Car("Wolksvagen", "kla1394", "red", 4, 1994);
        Car carTwo = new Car("Wolksvagen", "kla1394", "red", 4, 1994);
        Motorcycle motorcycle = new Motorcycle("Suzuki", "agt1323", "purple", 3, 1987);
        AvailableParkingSlot availableParkingSlot = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

        garage.AddVehicleToGarage(bus);
        garage.AddVehicleToGarage(car);
        garage.AddVehicleToGarage(carTwo);
        garage.AddVehicleToGarage(motorcycle);

        garage.FillRemainingSlotsWithEmptyVehicles(availableParkingSlot);
    }
    public IEnumerable<string> GetNrOfCarsInGarage()
    {
        var retrieveVehicle = garage.VehiclesInGarage();
        var nameCounts = new Dictionary<string, int>(); // Store name and instance count pairs.

        foreach (var items in retrieveVehicle)
        {
            string name = items.GetType().Name;

            // Increment the count for the name in the dictionary.
            if (!nameCounts.ContainsKey(name))
            {
                nameCounts[name] = items.InstanceCount;
            }
            else
            {
                nameCounts[name]++;
            }

        }

        foreach (var kvp in nameCounts)
        {
            string name = kvp.Key;
            int count = kvp.Value;

            if (name == "AvailableParkingSlot")
            {
                yield return $"\nAvailable parking slot's, {count}";
                break;
            }
            // Construct the itemText based on the 'getNames' parameter.

            yield return $"Vehicle Type: {name}, Parked: {count}";
        }
    }

    public IEnumerable<string> ListCarsWithoutDuplicates()
    {
        var retrieveVehicle = garage.VehiclesInGarage();
        var printedVehicleTypes = new HashSet<string>();

        foreach (var item in retrieveVehicle)
        {
            string vehicleType = item.GetType().Name;

            if (!printedVehicleTypes.Contains(vehicleType))
            {
                printedVehicleTypes.Add(vehicleType);

                // Print the vehicle type and InstanceCount (assuming it's an integer property).
                yield return $"{item}";
            }
        }
    }
}