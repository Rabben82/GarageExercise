using GarageExercise.Vehicles;

namespace GarageExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage<Vehicle> garage = new Garage<Vehicle>(3);
            Bus bus = new Bus("Mercedes", "abc1202", "grey", 6, 1978);
            Car car = new Car("Wolksvagen", "kla1394", "red", 4, 1994);
            Motorcycle motorcycle = new Motorcycle("Nissan", "bas39493", "yellow", 2, 2001);

            garage.AddVehicleToGarage(bus);
            garage.AddVehicleToGarage(car);
            garage.AddVehicleToGarage(motorcycle);
            garage.AddVehicleToGarage(motorcycle);

            var retrieveVehicle = garage.VehiclesInGarage();


            foreach (var items in retrieveVehicle)
            {
                Console.WriteLine(items);
            }

        }
    }
}