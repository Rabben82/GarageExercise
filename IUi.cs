namespace GarageExercise;

public interface IUi
{
    public void ConsoleMessageWrite(string message);
    public void ConsoleMessageWriteLine(string message);
    public int ReturnValidNumber();
    public void ClearConsole();
    public void WaitForKeyPress();
    public string UserInput();
}