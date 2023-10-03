namespace GarageExercise.Entities;

public class Boat : Vehicle
{
    private static int instanceCounter;
    public double Length { get; set; }
    public Boat(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, double length) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        Length = length;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Length: {Length}";
    }
}