namespace GarageExercise.Entities;

public class Car : Vehicle
{
    private static int instanceCounter;
    public string FuelType { get; set; }
    public Car(string model, string registrationNumber, string color, int numberOfWheels, int productionYear, string fuelType) : base(model, registrationNumber, color, numberOfWheels, productionYear)
    {
        instanceCounter++;
        base.InstanceCount = instanceCounter;
        FuelType = fuelType;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Fuel Type: {FuelType}";
    }
    public static Car CreateDefaultCar()
    {
        return new Car("Volkswagen", "ABC 123", "red", 4, 1998, "Diesel");
    }
}