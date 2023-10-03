using GarageExercise.Entities;

namespace GarageExercise;

public class GarageHandler
{
    public Garage<Vehicle> garage;
    private int vehicleIndex;
    //private Vehicle vehicle = new Vehicle();

    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);

        Bus bus = new Bus("Mercedes", "abc1202", "grey", 6, 1978, 20);
        Car car = new Car("wolksvagen", "kla1394", "red", 4, 1994, "Diesel");
        Car carTwo = new Car("wolksvagen", "kla1394", "red", 4, 1994,"Gasoline");
        Motorcycle motorcycle = new Motorcycle("Suzuki", "agt1323", "purple", 3, 1987, "125cc");

        AvailableParkingSlot availableParkingSlot = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

        AddVehicleToGarage(bus);
        AddVehicleToGarage(car);
        AddVehicleToGarage(carTwo);
        AddVehicleToGarage(motorcycle);

        FillRemainingSlotsWithEmptyVehicles(availableParkingSlot);
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

            if (name != "AvailableParkingSlot")
            {
                // Construct the itemText based on the 'getNames' parameter.
                yield return $"Vehicle Type: {name}, Parked: {count}";
            }
        }
    }

    public IEnumerable<string> ListCarsWithoutDuplicates()
    {
        var retrieveVehicle = garage.VehiclesInGarage();
        //var printedVehicleTypes = new HashSet<string>();

        foreach (var item in retrieveVehicle)
        {
            //string vehicleType = item.GetType().Name;

            if (item.Model != "Available")
            {
               
                yield return $"{item}";
            }
        }
    }
    public void AddVehicleToGarage(Vehicle vehicle)
    {
        bool added = false; // Initialize a flag to track whether the vehicle has been added

        for (int i = 0; i < garage.vehicleArray.Length; i++)
        {
            if (garage.vehicleArray[i] == null) // Check if the slot is empty
            {
                garage.vehicleArray[i] = vehicle; // Add the vehicle to the empty slot
                added = true; // Set the flag to indicate that the vehicle has been added
                break; // Exit the loop after adding the vehicle
            }
        }

        if (!added)
        {
            // Handle the case where the vehicle couldn't be added (e.g., garage is full)
            UserInterface.ConsoleMessageWrite("The garage is full. Cannot add the vehicle.");
        }
    }
    public void FillRemainingSlotsWithEmptyVehicles(Vehicle vehicle)
    {
        for (int i = 0; i < garage.vehicleArray.Length; i++)
        {
            if (garage.vehicleArray[i] == null)
            {
                garage.vehicleArray[i] = vehicle; // Initialize with an empty vehicle
            }
        }
    }

    public void RemoveVehicle()
    {
        var sum = garage.VehiclesInGarage();
        vehicleIndex = 0;

        foreach (var objects in sum)
        {
            vehicleIndex++;

            UserInterface.ConsoleMessageWriteLine($"{vehicleIndex}. {objects}");
        }

        UserInterface.ConsoleMessageWrite("\nChoose the parking slot number of the vehicle in the list you wanna remove!" +
                                          "\n\nYour Choice: ");
        var validNumber = UserInterface.ReturnValidNumber();

        garage.vehicleArray[validNumber - 1] = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

    }

    

    public void AddVehicle(Vehicle vehicle)
    {
        UserInterface.ClearConsole();

        var sum = garage.VehiclesInGarage();
        vehicleIndex = 0;

        foreach (var objects in sum)
        {
            vehicleIndex++;

            UserInterface.ConsoleMessageWriteLine($"{vehicleIndex}. {objects}");
        }

        UserInterface.ConsoleMessageWrite("\nChoose the available parking slot number you wanna park the vehicle!" +
                                          "\n\nYour Choice: ");
        var validNumber = UserInterface.ReturnValidNumber();

        garage.vehicleArray[validNumber -1] = vehicle;

        UserInterface.ConsoleMessageWrite($"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}");

    }
    public void SearchByRegistrationNumber(string registrationNumber)
    {
        var matchingVehicles = garage.vehicleArray
            .Where(vehicle => vehicle.RegistrationNumber == registrationNumber.ToLower());

        if (matchingVehicles.Any())
        {
            UserInterface.ConsoleMessageWriteLine("Registration Number Found In The Garage:");

            foreach (var vehicle in matchingVehicles)
            {
                UserInterface.ConsoleMessageWriteLine(vehicle.ToString());
            }
        }
        else
        {
            UserInterface.ConsoleMessageWriteLine($"The Registration Number ({registrationNumber}) Isn't Found In The Garage.");
        }

    }

    public void SearchByProperties(string[] properties)
    {

        foreach (var vehicle in garage.vehicleArray)
        {
            bool hasMatchingType = properties
                .Any(property =>
                    vehicle.GetType().Name.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase));

            bool hasMatchingModel = properties
                .Any(property =>
                    vehicle.Model.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase));

            bool hasMatchingRegistrationNumber = properties
                .Any(property =>
                    vehicle.RegistrationNumber.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase));

            bool hasMatchingColor = properties
                .Any(property =>
                    vehicle.Color.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase));

            bool hasMatchingWheels = properties
                .Any(property =>
                    int.TryParse(property, out int parsedValue) && // Try to parse as an integer
                    vehicle.NumberOfWheels == parsedValue);

            bool hasMatchingProductionYear = properties
                .Any(property => int.TryParse(property, out int parsedValue) &&
                                 vehicle.ProductionYear == parsedValue);

            if (hasMatchingModel && hasMatchingRegistrationNumber && hasMatchingColor && hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingRegistrationNumber && hasMatchingColor && hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingColor && hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingWheels && hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingType)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingModel)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingRegistrationNumber)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingColor)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingWheels)
            {
                Console.WriteLine(vehicle);
            }
            else if (hasMatchingProductionYear)
            {
                Console.WriteLine(vehicle);
            }
        }

    }
}