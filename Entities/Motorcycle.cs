namespace GarageExercise.Entities;

public class Motorcycle : Vehicle
{
    private static int instanceCounter;
    public string HorsePower { get; set; }
    public Motorcycle(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, string horsePower) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        HorsePower = horsePower;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Horse Power: {HorsePower}";
    }
    public static Motorcycle CreateDefaultMotorcycle()
    {
        return new Motorcycle("Suzuki", "BTY 947", "Blue", 2, 1987, "175cc");
    }
}