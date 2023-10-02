namespace GarageExercise;

public interface IVehicle
{
    public int NrOfEngines { get; set; }
    public string HorsePower { get; set; }
    public string FuelType { get; set; }
    public string NumberOfSeats { get; set;}
    public double Length { get; set; }
}