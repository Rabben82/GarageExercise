namespace GarageExercise.Entities;

public interface IVehicle
{
  string Model { get; set; }
  string RegistrationNumber { get; set; }
  string Color { get; set; }
  int NumberOfWheels { get; set; }
  int ProductionYear { get; set; }
  int InstanceCount { get; set; }
}