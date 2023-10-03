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
}