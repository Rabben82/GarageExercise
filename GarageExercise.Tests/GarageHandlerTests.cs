using GarageExercise.Entities;
using GarageExercise.Garage;
using GarageExercise.UI;
using Moq;

namespace GarageExercise.Tests
{
    public class GarageHandlerTests
    {
        [Fact]
        public void Remove_Vehicle_From_Garage()
        {
            // Arrange
            var uiMock = new Mock<IUi>();
            var vehicleMock = Bus.CreateDefaultBus();
            var garageHandler = new GarageHandler(uiMock.Object);
            garageHandler.Initialize(5); // Initialize with a garage size of 4

            // Act
            garageHandler.RemoveVehicleFromGarage(1); // Remove the first vehicle

            // Assert
            uiMock.Verify(ui => ui.ConsoleMessageWriteLine($"You Have Removed: {vehicleMock}"));
        }

        [Fact]
        public void RemoveVehicleFromGarage_CannotRemoveAvailableParkingSpace()
        {
            // Arrange
            var uiMock = new Mock<IUi>();
            var garageHandler = new GarageHandler(uiMock.Object);
            garageHandler.Initialize(5); // Initialize with a garage size of 4

            // Act
            garageHandler.RemoveVehicleFromGarage(5); // Try to remove from an AvailableParkingSpace slot

            // Assert
            uiMock.Verify(ui => ui.ConsoleMessageWrite("You can't remove an available parking slot!\nPress any key to continue."), Times.Once);
        }
    }
}