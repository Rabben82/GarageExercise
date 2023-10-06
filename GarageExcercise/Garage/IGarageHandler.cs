using GarageExercise.Entities;

namespace GarageExercise.Garage;

public interface IGarageHandler
{
    void AddAvailableParkingSlots(Vehicle emptyVehicle);
    IEnumerable<string> DisplayVehicleByTypeAndAmount();
    IEnumerable<string> DisplayParkedVehiclesFullInfo();
    void RemoveVehicle();
    void DisplayParkedVehiclesWithIndex();
    void RemoveVehicleFromGarage(int selectedSlot);
    void AddVehicleByUserInput(Vehicle vehicle);
    void SearchByRegistrationNumber(string registrationNumber);
    void SearchByProperties(string[] properties);
    List<Vehicle> GetMatchingVehicles(string[] properties);
}