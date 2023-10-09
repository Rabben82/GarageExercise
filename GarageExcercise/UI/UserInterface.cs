using GarageExercise.Entities;
using GarageExercise.Validations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GarageExercise.UI;

public class UserInterface : IUi
{
    public void ConsoleMessageWrite(string message) => Console.Write(message);
    public void ConsoleMessageWriteLine(string message) => Console.WriteLine(message);
    public void ConsoleMessageWriteLine(Vehicle vehicle) => Console.WriteLine(vehicle);
    public void ClearConsole() => Console.Clear();
    public void WaitForKeyPress() => Console.ReadKey();
    public string UserInput() => (Console.ReadLine() ?? throw new NullReferenceException("Error: Value is null!"));
    
}