using System.Drawing;
using GarageExercise.Entities;

namespace GarageExercise;

public class Garage<T> where T : Vehicle
{
    public readonly Vehicle[] vehicleArray;
    public Garage(int sizeGarage)
    {
        if (sizeGarage > 3)
        {
            vehicleArray = new Vehicle[sizeGarage];
        }
        else
        {
            UserInterface.ConsoleMessageWrite("The Garage need's to hold moore than 3 parking-spots");
        }
    }

    public IEnumerable<Vehicle> VehiclesInGarage()
    {
        foreach (var objects in vehicleArray)
        {
            yield return objects;
        }
    }
}