using System.Collections;
using GarageExercise.Entities;

namespace GarageExercise;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T[] vehicleArray;
    public Garage(int sizeGarage)
    {
        vehicleArray = new T[sizeGarage];
    }
    public void Park(T vehicle)
    {
        for (int i = 0; i < vehicleArray.Length; i++)
        {
            if (vehicleArray[i] == null) // Check if the slot is empty
            {
                vehicleArray[i] = vehicle; // Add the vehicle to the empty slot
                return; // Exit the method after adding the vehicle
            }
        }
        // Handle the case where the garage is full 
        throw new ArgumentException("Garage is full!");
    }

    public void Park(T vehicle, int index)
    {
        vehicleArray[index] = vehicle;
    }

    public void Remove(T vehicle, int index)
    {
        vehicleArray[index] = vehicle;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var objects in vehicleArray)
        {
            //returnera bara parkerade fordon!!!!
            yield return objects;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}