namespace GarageExercise.Entities;

public class Bus : Vehicle
{
    private static int instanceCounter;
    public int NumberOfSeats { get; set; }
    public Bus(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, int numberOfSeats) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        NumberOfSeats = numberOfSeats;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Number Of Seats: {NumberOfSeats}";
    }
    //Default Bus that are being instantiated at runtime in the garage
    public static Bus CreateDefaultBus()
    {
        return new Bus("Scania", "EKT 055", "Silver", 8, 1994, 20);
    }
}