namespace GarageExercise.Entities;

public class Boat : Vehicle
{
    private static int instanceCounter;
    public int Length { get; set; }
    public Boat(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, int length) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        Length = length;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Length: {Length}";
    }
    public static Boat CreateDefaultBoat()
    {
        return new Boat("Maxi77", "EFG 765", "Blue",0,1975,25);
    }
}