using GarageExercise.Vehicles;
using System.Drawing;

namespace GarageExercise;

public class Garage<T> where T : Vehicle
{
    private readonly Vehicle[] vehicleArray;
    private Vehicle sum;
    public Garage(int sizeGarage)
    {
        vehicleArray = new Vehicle[sizeGarage];
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
}