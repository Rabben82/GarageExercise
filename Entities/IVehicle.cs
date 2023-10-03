namespace GarageExercise.Entities;

public interface IVehicle
{
    public string? Model { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Color { get; set; }
    public int NumberOfWheels { get; set; }
    public int ProductionYear { get; set; }
    public int InstanceCount { get; set; }
}