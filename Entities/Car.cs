namespace GarageExercise.Entities;

public class Car : Vehicle
{
    private static int instanceCounter;

    public Car(string model, string registrationNumber, string color, int numberOfWheels, int productionYear) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
    }
}