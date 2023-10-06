using GarageExercise.Entities;

namespace GarageExercise.UI;

public interface IUi
{
    public void ConsoleMessageWrite(string message);
    public void ConsoleMessageWriteLine(string message);
    public void ConsoleMessageWriteLine(Vehicle vehicle);
    public void ClearConsole();
    public void WaitForKeyPress();
    public string UserInput();
}