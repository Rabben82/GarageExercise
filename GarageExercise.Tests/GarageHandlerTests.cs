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
            garageHandler.Initialize(5); // Initialize with a garage size of 5

            // Act
            garageHandler.RemoveVehicleFromGarage(1); // Remove the first vehicle

            // Assert
            // You can add assertions here to check if the vehicle was removed successfully.
            // For example, you can check if the garage is now empty at slot 0:
            //var vehicles = garageHandler.DisplayParkedVehiclesFullInfo();
            //Assert.DoesNotContain("Available", vehicles); // Check if the first slot is not "Available"
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
            // You can add assertions here to check if the method behaves as expected.
            // For example, you can check if the appropriate error message is displayed.
            // You might need to adjust the code according to how your UI or error handling works.
            uiMock.Verify(ui => ui.ConsoleMessageWrite("You can't remove an available parking slot!\nPress any key to continue."), Times.Once);
        }

        [Fact]
        public void SearchByRegistrationNumber_FoundInGarage()
        {
            // Arrange
            var uiMock = new Mock<IUi>();
            var garageHandler = new GarageHandler(uiMock.Object);
            garageHandler.Initialize(5); // Initialize with a garage size of 5

            // Add vehicles with known registration numbers
            var vehicle1 = Bus.CreateDefaultBus();
            var vehicle2 = Car.CreateDefaultCar();
            var vehicle3 = Motorcycle.CreateDefaultMotorcycle();

            string expectedRegistrationNr = vehicle1.RegistrationNumber;
            string failReg = "abd123";

            // Act
            garageHandler.SearchByRegistrationNumber(expectedRegistrationNr);

            // Assert
            // Verify that the appropriate methods on the IUi mock were called
            uiMock.Verify(ui => ui.ConsoleMessageWriteLine("Registration Number Found In The Garage!"), Times.Once);
        }

        [Fact]
        public void SearchByRegistrationNumber_NotFoundInGarage()
        {
            // Arrange
            var uiMock = new Mock<IUi>();
            var garageHandler = new GarageHandler(uiMock.Object);
            garageHandler.Initialize(5); // Initialize with a garage size of 5

            // Act
            garageHandler.SearchByRegistrationNumber("xbc172");

            // Assert
            // Verify that the appropriate method on the IUi mock was called
            uiMock.Verify(ui => ui.ConsoleMessageWriteLine("The Registration Number (xbc172) Isn't Found In The Garage."), Times.Once);
        }
    }
}