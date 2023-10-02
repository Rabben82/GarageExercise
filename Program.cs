using System.Threading.Channels;
using GarageExercise.Entities;

namespace GarageExercise
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            GarageHandler garageHandler = new GarageHandler();
            garageHandler.Run();



        }

        
    }
}