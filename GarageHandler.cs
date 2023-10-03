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

        Bus bus = new Bus("Mercedes", "abc1202", "grey", 6, 1978);
        Car car = new Car("Wolksvagen", "kla1394", "red", 4, 1994);
        Car carTwo = new Car("Wolksvagen", "kla1394", "red", 4, 1994);
        Motorcycle motorcycle = new Motorcycle("Suzuki", "agt1323", "purple", 3, 1987);

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

        UserInterface.ConsoleMessageWrite("\nChoose the available parking slot number you wanna park the car!" +
                                          "\n\nYour Choice: ");
        var validNumber = UserInterface.ReturnValidNumber();

        garage.vehicleArray[validNumber -1] = vehicle;

        UserInterface.ConsoleMessageWrite($"\nYou Have Added:\n{vehicle}\nTo Parking-Slot: {validNumber}");

    }
    //public void SearchByRegistrationNumber()
    //{
    //    garage.vehicleArray.Where(n => Equals())
    //}
}