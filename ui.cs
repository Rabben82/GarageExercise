namespace GarageExercise;

public class UserInterface : IUi
{
    public void ConsoleMessageWrite(string message) => Console.Write(message);
    public void ConsoleMessageWriteLine(string message) => Console.WriteLine(message);
    public int ReturnValidNumber() => int.TryParse(Console.ReadLine(), out int number) ? number : throw new ArgumentException("It's not a valid number");
    public void ClearConsole() => Console.Clear();
    public void WaitForKeyPress() => Console.ReadKey();
    public string UserInput() => (Console.ReadLine() ?? throw new ArgumentException("Can't Be Null Or Whitespace")).Trim();
}