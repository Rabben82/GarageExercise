using GarageExercise.Entities;
using GarageExercise.UI;
using GarageExercise.Validations;

namespace GarageExercise.Garage;

public class GarageHandler : IGarageHandler
{
    private Garage<Vehicle> garage = default!;
    private readonly IUi ui;

    public GarageHandler(IUi ui)
    {
        //var list = new List<Vehicle>();
        this.ui = ui;
    }

    public void Initialize(int sizeOfGarage)
    {
        garage = new Garage<Vehicle>(sizeOfGarage);
        //this initializes some default cars into the garage
        InitializeDefaultVehicles();
        //This initializes the rest of the array with vehicle objects displaying its available to park there
        var availableParkingSlot = AvailableParkingSlot.CreateAvailableParkingSlot();
        AddAvailableParkingSlots(availableParkingSlot);
    }

    private void InitializeDefaultVehicles()
    {
        var bus = Bus.CreateDefaultBus();
        var airPlane = AirPlane.CreateDefaultAirplane();
        var car = Car.CreateDefaultCar();
        var motorcycle = Motorcycle.CreateDefaultMotorcycle();

        garage.Park(bus);
        garage.Park(airPlane);
        garage.Park(car);
        garage.Park(motorcycle);
    }

    public void AddAvailableParkingSlots(Vehicle emptyVehicle)
    {
        foreach (var items in garage)
        {
            if (items == null)
                garage.Park(
                    emptyVehicle); // Add an empty vehicle to the empty slot showing the user its available to park
        }
    }

    public IEnumerable<string> DisplayVehicleByTypeAndAmount()
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

    public IEnumerable<string> DisplayParkedVehiclesFullInfo()
    {
        return from item in garage where item.Model != "Available" select $"{item}";
    }

    public void RemoveVehicle()
    {
        DisplayParkedVehiclesWithIndex();
        int selectedSlot = Validation.CheckValidNumber(ui,
            "\nChoose the parking slot number of the vehicle you want to remove:\n\nYour Choice: ");
        RemoveVehicleFromGarage(selectedSlot);
    }

    public void DisplayParkedVehiclesWithIndex()
    {
        int vehicleIndex = 0;

        foreach (var vehicle in garage)
        {
            vehicleIndex++;
            ui.ConsoleMessageWriteLine($"{vehicleIndex}. {vehicle}");
        }
    }

    public void RemoveVehicleFromGarage(int selectedSlot)
    {
        int slotIndex = selectedSlot - 1; // Convert to 0-based index

        if (Validation.IsParkingSlotInRange(slotIndex, garage))
        {
            var availableParkingSlot = AvailableParkingSlot.CreateAvailableParkingSlot();
            var removedVehicle = garage
                .Where((vehicle, index) => index == slotIndex)
                .FirstOrDefault()!;
            if (removedVehicle.GetType().Name == "AvailableParkingSlot")
            {
                ui.ConsoleMessageWrite("You can't remove an available parking slot!\nPress any key to continue.");
                return;
            }

            //replace the removed vehicle with available parking slot
            garage.Remove(availableParkingSlot, slotIndex);

            ui.ConsoleMessageWriteLine($"You Have Removed: {removedVehicle}");
        }
        else
        {
            ui.ConsoleMessageWriteLine(
                "Invalid parking slot number. No vehicle was removed.\nPress any key to continue!");
        }
    }

    public void AddVehicleByUserInput(Vehicle vehicle)
    {
        DisplayParkedVehiclesWithIndex();
        var validNumber = Validation.CheckValidNumber(ui,
            "\nChoose an available parking slot number for the vehicle!\nYour Choice: ");
        var isFreeParkingSpot = garage.ElementAtOrDefault(validNumber - 1)?.GetType().Name == "AvailableParkingSlot";

        if (Validation.IsValueInRange(validNumber, garage) && isFreeParkingSpot)
        {
            garage.Park(vehicle, validNumber - 1); // Convert to 0-based index
            ui.ConsoleMessageWriteLine(
                $"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}\nPress any key to continue.");
        }
        else
        {
            ui.ConsoleMessageWriteLine(
                "Invalid parking slot number or not a free parking slot. The vehicle was not parked.");
        }
    }

    public void SearchByRegistrationNumber(string registrationNumber)
    {
        var matchingRegistrationNumber = garage
            .Where(vehicle => vehicle != null &&
                              vehicle.RegistrationNumber.ToLower().Replace(" ", "") ==
                              registrationNumber.ToLower().Replace(" ", ""));

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
        var matchingVehicles = GetMatchingVehicles(properties);

        // Display the matching vehicles
        if (matchingVehicles.Count > 0)
        {
            ui.ConsoleMessageWriteLine($"One or more property matches this/these vehicles!");
            foreach (var matchedVehicle in matchingVehicles)
            {
                if (matchedVehicle.GetType().Name == "AvailableParkingSlot")
                {
                    ui.ConsoleMessageWriteLine($"You've entered an empty value, and that matches: {matchedVehicle}");
                }
                else
                {
                    ui.ConsoleMessageWriteLine(matchedVehicle);
                }
            }
        }
        else
        {
            ui.ConsoleMessageWriteLine("No match found!");
        }
    }

    public List<Vehicle> GetMatchingVehicles(string[] properties)
    {
        List<Vehicle> matchingVehicles = new List<Vehicle>();

        foreach (var vehicle in garage)
        {
            // Check for matches in all properties
            if (properties.Any(property =>
                    string.IsNullOrEmpty(property) && vehicle.NumberOfWheels == 0 ||
                    vehicle.GetType().Name.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.Model.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.RegistrationNumber.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    vehicle.Color.Equals(property.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    int.TryParse(property, out int parsedValue) &&
                    (vehicle.NumberOfWheels == parsedValue || vehicle.ProductionYear == parsedValue) ||
                    string.IsNullOrEmpty(property) && vehicle.ProductionYear == 0))
            {
                matchingVehicles.Add(vehicle);
            }
        }

        return matchingVehicles;
    }

    public bool IsRegNumberExisting(string regNumber)
    {
        return garage.Any(
            item => string.Equals(
                item.RegistrationNumber.Replace(" ", "").Trim(),
                regNumber.Replace(" ", "").Trim(),
                StringComparison.OrdinalIgnoreCase
            )
        ); ;
    }
}