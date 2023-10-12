using System.Collections;
using GarageExercise.Entities;

namespace GarageExercise.Garage;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T[] vehicleArray;
    public Garage(int sizeGarage)
    {
        if (sizeGarage < GarageHelpers.MinGarageSize && sizeGarage > GarageHelpers.MaxGarageSize)
        {
            throw new ArgumentOutOfRangeException($"Not a valid garage size!{sizeGarage}");
        }
        vehicleArray = new T[sizeGarage];
    }
    //this method initializes the default cars at runtime
    public void Park(T vehicle)
    {
        //Check if full
        //Check if T is null
        //Ok?

        for (int i = 0; i < vehicleArray.Length; i++)
        {
            if (vehicleArray[i] == null)
            {
                vehicleArray[i] = vehicle;
                return;
            }
        }
        // Handle the case where the garage is full 
        throw new ArgumentException("Garage is full!");
    }
    //this method adds vehicles to an available parkingslot by user input
    public void Park(T vehicle, int index)
    {
        //Validate T
        vehicleArray[index] = vehicle;
    }
    public void Remove(T vehicle, int index)
    {
        //Validate T
        vehicleArray[index] = vehicle;
    }
    //I have the logic if i wanna show available parking slots or only showing parked cars in garage handler 
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var objects in vehicleArray)
        {
            yield return objects;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}