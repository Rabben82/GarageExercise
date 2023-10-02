namespace GarageExercise.Vehicles;

public class Car : Vehicle, IVehicle
{
    public int NrOfEngines { get; set; }
    public string HorsePower { get; set; }
    public string FuelType { get; set; }
    public string NumberOfSeats { get; set; }
    public double Length { get; set; }
    public Car(string model, string registrationNumber, string color, int numberOfWheels, int age) : base(model, registrationNumber, color, numberOfWheels, age)
    {
    }

}