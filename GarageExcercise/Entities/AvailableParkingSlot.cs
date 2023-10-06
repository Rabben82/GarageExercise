namespace GarageExercise.Entities
{
    internal class AvailableParkingSlot : Vehicle
    {
        private static int instanceCounter;
        public AvailableParkingSlot(string model, string registrationNumber, string color, int numberOfWheels, int productionYear) : base(model, registrationNumber, color, numberOfWheels, productionYear)
        {
            instanceCounter++;
            base.InstanceCount = instanceCounter;
        }
        public override string ToString()
        {
            return $"Available Parking Slot";
        }
        public static AvailableParkingSlot CreateAvailableParkingSlot()
        {
            return new AvailableParkingSlot("Available", "Available", "Available", 0, 0000);
        }
    }
}
