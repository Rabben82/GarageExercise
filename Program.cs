using System.Threading.Channels;
using GarageExercise.Entities;

namespace GarageExercise
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            IUi iUi = new UserInterface();
            Garage<Vehicle> garage = new Garage<Vehicle>(iUi);
            GarageHandler garageHandler = new GarageHandler(iUi);
            Manager manager = new Manager(garageHandler, iUi);
            manager.Run();
        }
    }
}