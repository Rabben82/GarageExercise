namespace GarageExercise.Entities;

public class AirPlane : Vehicle
{
    private static int instanceCounter;
    public int NumberOfEngines { get; set; }
    public AirPlane(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, int numberOfEngines) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Number Of Engines: {NumberOfEngines}";

    }
}