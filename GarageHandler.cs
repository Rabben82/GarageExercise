using GarageExercise.Entities;

namespace GarageExercise;

public class GarageHandler
{
    public Garage<Vehicle>? garage;
    private int vehicleIndex;

    private readonly IUi ui;
    //private Vehicle vehicle = new Vehicle();
    public GarageHandler(IUi ui)
    {
        this.ui = ui;
    }
    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);

        Bus bus = new Bus("Scania", "EKY 055", "Silver", 8, 1994, 20);
        Car car = new Car("Seat", "XTW 487", "White", 4, 2011, "Gasoline");
        Car carTwo = new Car("Volkswagen", "YRT 217", "red", 4, 1998,"Diesel");
        Motorcycle motorcycle = new Motorcycle("Suzuki", "BTY 947", "Blue", 2, 1987, "175cc");

        AvailableParkingSlot availableParkingSlot = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

        AddVehicleToGarage(bus);
        AddVehicleToGarage(car);
        AddVehicleToGarage(carTwo);
        AddVehicleToGarage(motorcycle);

        FillRemainingSlotsWithEmptyVehicles(availableParkingSlot);
    }

    public IEnumerable<string> GetTypeOfVehicleInGarage()
    {
        //Retrieves the vehicle types and how many of that type is in the garage
        var typeCount = CalculateTypeCounts();

        //Displays The Result
        foreach (var kvp in typeCount)
        {
            string name = kvp.Key;
            int count = kvp.Value;

            if (name != "AvailableParkingSlot")
            {
                yield return $"Vehicle Type: {name}, Parked: {count}";
            }
        }
    }
    private Dictionary<string, int> CalculateTypeCounts()
    {
        var retrieveVehicle = garage.VehiclesInGarage();
        var typeCount = new Dictionary<string, int>(); //use dictionary to store "Key", the type of Vehicle and how many that has been instantiated "Value"

        foreach (var items in retrieveVehicle) 
        {
            string name = items.GetType().Name; //Stores the types in the name variable

            if (!typeCount.ContainsKey(name)) //If there are same type already i dont wanna display it, so it skips it but still increments it so i know how many of that type has been instantiated
            {
                typeCount[name] = items.InstanceCount; 
            }
            else
            {
                typeCount[name]++; //Retrieves the current count value associated with the name key from the typeCount dictionary.Increments that count value by 1.
            }
        }

        return typeCount;
    }

    public IEnumerable<string> ListParkedCars()
    {
        var retrieveVehicle = garage.VehiclesInGarage();

        foreach (var item in retrieveVehicle)
        {

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
            ui.ConsoleMessageWrite("The garage is full. Cannot add the vehicle.");
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

            ui.ConsoleMessageWriteLine($"{vehicleIndex}. {objects}");
        }

        ui.ConsoleMessageWrite("\nChoose the parking slot number of the vehicle in the list you wanna remove!" +
                                          "\n\nYour Choice: ");
        var validNumber = ui.ReturnValidNumber();
        var removedVehicle = garage.vehicleArray[validNumber - 1];

        garage.vehicleArray[validNumber - 1] = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

        ui.ConsoleMessageWriteLine($"You Have Removed: {removedVehicle}");
    }

    

    public void AddVehicle(Vehicle vehicle)
    {
        ui.ClearConsole();

        var sum = garage.VehiclesInGarage();
        vehicleIndex = 0;

        foreach (var objects in sum)
        {
            vehicleIndex++;

            ui.ConsoleMessageWriteLine($"{vehicleIndex}. {objects}");
        }

        ui.ConsoleMessageWrite("\nChoose the available parking slot number you wanna park the vehicle!" +
                                          "\n\nYour Choice: ");
        var validNumber = ui.ReturnValidNumber();

        garage.vehicleArray[validNumber -1] = vehicle;

        ui.ConsoleMessageWrite($"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}");

    }
    public void SearchByRegistrationNumber(string registrationNumber)
    {
        var matchingVehicles = garage.vehicleArray
            .Where(vehicle => vehicle.RegistrationNumber == registrationNumber.ToLower());

        if (matchingVehicles.Any())
        {
            ui.ConsoleMessageWriteLine("Registration Number Found In The Garage:");

            foreach (var vehicle in matchingVehicles)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
        }
        else
        {
            ui.ConsoleMessageWriteLine($"The Registration Number ({registrationNumber}) Isn't Found In The Garage.");
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
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingRegistrationNumber && hasMatchingColor && hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingColor && hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingModel && hasMatchingWheels && hasMatchingProductionYear)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingWheels && hasMatchingProductionYear)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingType)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingModel)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingRegistrationNumber)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingColor)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingWheels)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
            else if (hasMatchingProductionYear)
            {
                ui.ConsoleMessageWriteLine(vehicle.ToString());
            }
        }

    }
}