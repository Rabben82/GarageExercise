using GarageExercise.Entities;
using GarageExercise.Garage;
using GarageExercise.UI;
using Moq;

namespace GarageExercise.Tests
{
    public class GarageHandlerTest
    {
        [Fact]
        public void SearchByRegistrationNumber_WhenMatchFound_ShouldDisplayVehicles()
        {
            // Arrange
            // Create a mock for IUi
            var mockUi = new Mock<IUi>();
            var registrationNumberToSearch = "ABC 123";

            // Create a list of vehicles with a matching registration number
            var matchingVehicle = Car.CreateDefaultCar();
            var nonMatchingVehicle = Bus.CreateDefaultBus();
            var garage = new Garage<Vehicle>(8); // Use Garage<Vehicle> instead of List<Vehicle>
            garage.Park(matchingVehicle);
            garage.Park(nonMatchingVehicle);

            // Create an instance of GarageHandler with the mock UI and set the garage field
            var garageHandler = new GarageHandler(mockUi.Object);
            typeof(GarageHandler)
                .GetField("garage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(garageHandler, garage);

            // Act
            garageHandler.SearchByRegistrationNumber(registrationNumberToSearch);

            // Assert
            mockUi.Verify(ui => ui.ConsoleMessageWriteLine(matchingVehicle.ToString()), Times.Never());
            mockUi.Verify(ui => ui.ConsoleMessageWriteLine(nonMatchingVehicle.ToString()), Times.Never());
        }

        [Fact]
        public void SearchByRegistrationNumber_WhenNoMatchFound_ShouldDisplayNotFoundMessage()
        {
            // Arrange
            // Create a mock for IUi
            var mockUi = new Mock<IUi>();
            var registrationNumberToSearch = "XYZ789";

            // Create a list of vehicles with no matching registration numbers
            var garage = new Garage<Vehicle>(8); // Use Garage<Vehicle> instead of List<Vehicle>

            // Create an instance of GarageHandler with the mock UI and set the garage field
            var garageHandler = new GarageHandler(mockUi.Object);
            typeof(GarageHandler)
                .GetField("garage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(garageHandler, garage);

            // Act
            garageHandler.SearchByRegistrationNumber(registrationNumberToSearch);

            // Assert
            mockUi.Verify(ui => ui.ConsoleMessageWriteLine("The Registration Number (XYZ789) Isn't Found In The Garage."), Times.Once());
        }
    }
}