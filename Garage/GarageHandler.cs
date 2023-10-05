using GarageExercise.Entities;
using GarageExercise.UI;
using GarageExercise.Validations;

namespace GarageExercise.Garage;

public class GarageHandler
{
    private Garage<Vehicle> garage = default!;
    private readonly IUi ui;

    public GarageHandler(IUi ui)
    {
        var list = new List<Vehicle>();
        this.ui = ui;
    }
    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);
        //this initializes some default cars into the garage
        var bus = Bus.CreateDefaultBus();
        var airPlane = AirPlane.CreateDefaultAirplane();
        var car = Car.CreateDefaultCar();
        var motorcycle = Motorcycle.CreateDefaultMotorcycle();
        var availableParkingSlot = AvailableParkingSlot.CreateAvailableParkingSlot();

        garage.Park(bus);
        garage.Park(airPlane);
        garage.Park(car);
        garage.Park(motorcycle);
        //This initializes the rest of the array with vehicle objects displaying its available to park there
        AddAvailableParkingSlots(availableParkingSlot);
    }
    public void AddAvailableParkingSlots(Vehicle emptyVehicle)
    {
        foreach (var items in garage)
        {
            if (items == null)
                garage.Park(emptyVehicle); // Add an empty vehicle to the empty slot showing the user its available to park
        }
    }
    public IEnumerable<string> ShowVehicleTypeAmount()
    {
        var vehicleAmount = garage
            .GroupBy(vehicle => vehicle.GetType().Name)
            .Select(group => new
            {
                TypeName = group.Key,
                Count = group.Count()
            })
            .ToList();

        foreach (var count in vehicleAmount)
        {
            //if (count.TypeName != "AvailableParkingSlot")
                yield return $"{count.TypeName}: {count.Count}";
        }
    }
    public IEnumerable<string> DisplayParkedVehicles()
    {
        return from item in garage where item.Model != "Available" select $"{item}";
    }
    public void RemoveVehicle()
    {
        DisplayParkedVehiclesWithIndex();
        int selectedSlot = Validation.CheckUserSelection("\nChoose the parking slot number of the vehicle you want to remove:\n\nYour Choice: ");
        RemoveVehicleFromGarage(selectedSlot);
    }
    private void DisplayParkedVehiclesWithIndex()
    {
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
            var availableParkingSlot = AvailableParkingSlot.CreateAvailableParkingSlot();
            var removedVehicle = garage
                .Where((vehicle, index) => index == slotIndex)
                .FirstOrDefault();
            //replace the removed vehicle with available parking slot
            garage.Remove(availableParkingSlot, slotIndex);

            ui.ConsoleMessageWriteLine($"You Have Removed: {removedVehicle}");
        }
        else
        {
            ui.ConsoleMessageWriteLine("Invalid parking slot number. No vehicle was removed.\nPress any key to continue!");
        }
    }
    public void AddVehicleByUserInput(Vehicle vehicle)
    {
        DisplayParkedVehiclesWithIndex();
        var validNumber = Validation.CheckUserSelection("\nChoose an available parking slot number for the vehicle!\nYour Choice: ");

        if (IsValueInRange(validNumber))
        {
            garage.Park(vehicle, validNumber - 1);// Convert to 0-based index
            ui.ConsoleMessageWriteLine($"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}\nPress any key to continue.");
        }
        else
        {
            ui.ConsoleMessageWriteLine("Invalid parking slot number. The vehicle was not parked.");
        }
    }
    public void SearchByRegistrationNumber(string registrationNumber)
    {
        var matchingRegistrationNumber = garage
            .Where(vehicle => vehicle.RegistrationNumber.ToLower().Replace(" ", "") == registrationNumber.ToLower().Replace(" ", ""));

        if (matchingRegistrationNumber.Any())
        {
            ui.ConsoleMessageWriteLine("Registration Number Found In The Garage!");

            foreach (var vehicle in matchingRegistrationNumber)
            {
                ui.ConsoleMessageWriteLine(vehicle);
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
                    string.IsNullOrEmpty(property) && vehicle.NumberOfWheels == 0 || // Handle empty properties as 0 wheels
                    vehicle.Model.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.RegistrationNumber.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.Color.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    int.TryParse(property, out int parsedValue) &&
                     (vehicle.NumberOfWheels == parsedValue || vehicle.ProductionYear == parsedValue) ||
                    string.IsNullOrEmpty(property) && vehicle.ProductionYear == 0)) // Handle empty properties as 0 production year
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
            ui.ConsoleMessageWriteLine($"Found this vehicle based on your search!\n{bestMatch}");
        }
        else
        {
            ui.ConsoleMessageWriteLine("No match found!");
        }
    }
    private bool IsParkingSlotInRange(int slotIndex)
    {
        bool isParkingSlotInRange = slotIndex >= 0 && slotIndex < garage.ToArray().Length;

        return isParkingSlotInRange;
    }
    private bool IsValueInRange(int validNumber)
    {
        bool isValid = validNumber >= 1 && validNumber <= garage.ToArray().Length;

        return isValid;
    }
}