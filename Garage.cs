using System.Drawing;
using GarageExercise.Entities;

namespace GarageExercise;

public class Garage<T> where T : Vehicle
{
    private readonly Vehicle[] vehicleArray;
    public Garage(int sizeGarage)
    {
        if (sizeGarage > 3)
        {
            vehicleArray = new Vehicle[sizeGarage];
        }
        else
        {
            UserInterface.ConsoleMessage("The Garage need's to hold moore than 3 parking-spots");
        }
    }

    public IEnumerable<Vehicle> VehiclesInGarage()
    {
        foreach (var objects in vehicleArray)
        {
            yield return objects;
        }
    }

    public void AddVehicleToGarage(Vehicle vehicle)
    {
        bool added = false; // Initialize a flag to track whether the vehicle has been added

        for (int i = 0; i < vehicleArray.Length; i++)
        {
            if (vehicleArray[i] == null) // Check if the slot is empty
            {
                vehicleArray[i] = vehicle; // Add the vehicle to the empty slot
                added = true; // Set the flag to indicate that the vehicle has been added
                break; // Exit the loop after adding the vehicle
            }
        }

        if (!added)
        {
            // Handle the case where the vehicle couldn't be added (e.g., garage is full)
            Console.WriteLine("The garage is full. Cannot add the vehicle.");
        }

    }
    public void FillRemainingSlotsWithEmptyVehicles(Vehicle vehicle)
    {
        for (int i = 0; i < vehicleArray.Length; i++)
        {
            if (vehicleArray[i] == null)
            {
                vehicleArray[i] = vehicle; // Initialize with an empty vehicle
            }
        }
    }
}