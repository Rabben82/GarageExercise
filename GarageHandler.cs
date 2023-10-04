using GarageExercise.Entities;

namespace GarageExercise;

public class GarageHandler
{
    private Garage<Vehicle> garage = default!;
    private readonly IUi ui;

    public GarageHandler(IUi ui)
    {
        var list = new List<Vehicle>();
        this.ui = ui;
    }
    //initializes the garage with some "default" values
    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);

        Bus bus = new Bus("Scania", "EKT 055", "Silver", 8, 1994, 20);
        AirPlane car = new AirPlane("Boeing", "XTW 487", "White", 4, 2011, 2);
        Car carTwo = new Car("Volkswagen", "YRT 217", "red", 4, 1998, "Diesel");
        Motorcycle motorcycle = new Motorcycle("Suzuki", "BTY 947", "Blue", 2, 1987, "175cc");

        AvailableParkingSlot availableParkingSlot = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);

        AddVehicleToGarage(bus);
        AddVehicleToGarage(car);
        AddVehicleToGarage(carTwo);
        AddVehicleToGarage(motorcycle);
        //This initializes the rest of the array with vehicle objects displaying its available to park
        AvailableParkingSlots(availableParkingSlot);
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
        var typeCount = new Dictionary<string, int>(); //use dictionary to store "Key", the type of Vehicle and how many that has been instantiated "Value"

        foreach (var items in garage)
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
    public IEnumerable<string> DisplayParkedCars()
    {
        //foreach (var item in garage)
        //{
        //    if (item.Model != "Available")
        //    {
        //        yield return $"{item}";
        //    }
        //}
        return from item in garage where item.Model != "Available" select $"{item}";
    }
    public void AddVehicleToGarage(Vehicle vehicle)
    {
        garage.Park(vehicle);
    }
    public void AvailableParkingSlots(Vehicle emptyVehicle)
    {
        foreach (var items in garage)
        {
            if (items == null) // Check if the slot is empty
                garage.Park(emptyVehicle); // Add an empty vehicle to the empty slot using the Park method
        }
    }
    public void RemoveVehicle()
    {
        DisplayParkedVehicles();
        int selectedSlot = ui.GetUserSelection("\nChoose the parking slot number of the vehicle you want to remove:\n\nYour Choice: ");
        RemoveVehicleFromGarage(selectedSlot);
    }
    private void DisplayParkedVehicles()
    {
        //var vehicles = garage..;
        int vehicleIndex = 0;

        foreach (var vehicle in garage)
        {
            vehicleIndex++;
            ui.ConsoleMessageWriteLine($"{vehicleIndex}. {vehicle}");
        }
    }
    private void RemoveVehicleFromGarage(int selectedSlot)
    {
        int slotIndex = selectedSlot - 1; // Convert to 0-based index

        if (IsParkingSlotInRange(slotIndex))
        {
            var vehicle = new AvailableParkingSlot("Available", "Available", "Available", 0, 0);
            var removedVehicle = garage
                .Where((vehicle, index) => index == slotIndex)
                .FirstOrDefault();

            garage.Remove(vehicle, slotIndex);

            ui.ConsoleMessageWriteLine($"You Have Removed: {removedVehicle}");
        }
        else
        {
            ui.ConsoleMessageWriteLine("Invalid parking slot number. No vehicle was removed.");
        }
    }

    private bool IsParkingSlotInRange(int slotIndex)
    {
        bool isParkingSlotInRange = slotIndex >= 0 && slotIndex < garage.ToArray().Length;

        return isParkingSlotInRange;
    }
    public void AddVehicle(Vehicle vehicle)
    {
        DisplayParkedVehicles();
        var validNumber = ui.GetUserSelection("\nChoose an available parking slot number for the vehicle!\nYour Choice: ");

        if (IsValueInRange(validNumber))
        {
            garage.Park(vehicle, validNumber - 1);
            ui.ConsoleMessageWriteLine($"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}");
        }
        else
        {
            ui.ConsoleMessageWriteLine("Invalid parking slot number. The vehicle was not parked.");
        }
    }
    private bool IsValueInRange(int validNumber)
    {
        bool isValid = validNumber >= 1 && validNumber <= garage.ToArray().Length;

        return isValid;
    }

    public void SearchByRegistrationNumber(string registrationNumber)
    {
        var matchingVehicles = garage
            .Where(vehicle => vehicle.RegistrationNumber.ToLower().Replace(" ", "") == registrationNumber.ToLower().Replace(" ", ""));

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
        Vehicle bestMatch = null;  // Initialize a variable to store the best match
        int maxMatches = 0;        // Initialize the count of maximum matches

        foreach (var vehicle in garage)
        {
            int matchCount = 0;  // Initialize the count of matches for this vehicle

            // Check for matches in all properties
            if (properties.Any(property =>
                    (string.IsNullOrEmpty(property) && vehicle.NumberOfWheels == 0) || // Handle empty properties as 0 wheels
                    vehicle.Model.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.RegistrationNumber.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.Color.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    (int.TryParse(property, out int parsedValue) &&
                     (vehicle.NumberOfWheels == parsedValue || vehicle.ProductionYear == parsedValue)) ||
                    (string.IsNullOrEmpty(property) && vehicle.ProductionYear == 0))) // Handle empty properties as 0 production year
            {
                matchCount++;
            }

            // Check if the current vehicle is the best match so far
            if (matchCount > maxMatches)
            {
                bestMatch = vehicle;
                maxMatches = matchCount;
            }
        }

        // Display the best matching vehicle
        if (bestMatch != null)
        {
            ui.ConsoleMessageWriteLine(bestMatch.ToString());
        }
        else
        {
            Console.WriteLine("No match found!");
        }
    }
}