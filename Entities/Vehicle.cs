namespace GarageExercise.Entities;

public class Vehicle
{
    public string Model { get; set; }
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }
    public int ProductionYear { get; set; }

    private static int instanceCount;
    public int InstanceCount { get; set; }

    public Vehicle(string model, string registrationNumber, string color, int numberOfWheels, int productionYear)
    {
        Model = model;
        RegistrationNumber = registrationNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
        ProductionYear = productionYear;
        //InstanceCount = ;
    }

    public override string ToString()
    {
        return $"Model: {Model}, Registration NR: {RegistrationNumber}, Color: {Color}, NR Of Wheels {NumberOfWheels}, Production Year: {ProductionYear}";
    }
}