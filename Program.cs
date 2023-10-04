using System.Threading.Channels;
using GarageExercise.Entities;

namespace GarageExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUi iUi = new UserInterface();
            GarageHandler garageHandler = new GarageHandler(iUi);
            UiManager manager = new UiManager(garageHandler, iUi);
            manager.Run();
        }
    }
}