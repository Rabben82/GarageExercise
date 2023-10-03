using System.Drawing;
using GarageExercise.Entities;

namespace GarageExercise;

public class Garage<T> where T : Vehicle
{
    public readonly Vehicle[] vehicleArray = Array.Empty<Vehicle>();
    private readonly IUi? ui;

    public Garage(IUi ui)
    {
        this.ui = ui;
    }
    public Garage(int sizeGarage)
    {
        
        if (sizeGarage > 3)
        {
            vehicleArray = new Vehicle[sizeGarage];
        }
        else
        {
            ui?.ConsoleMessageWrite("The Garage need's to hold moore than 3 parking-spots");
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