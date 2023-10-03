namespace GarageExercise;

public static class UserInterface
{
    public static void ConsoleMessageWrite(string message) => Console.Write(message);
    public static void ConsoleMessageWriteLine(string message) => Console.WriteLine(message);
    public static int ReturnValidNumber() => int.TryParse(Console.ReadLine(), out int number) ? number : throw new ArgumentException("It's not a valid number");
    public static void ClearConsole() => Console.Clear();
    public static void WaitForKeyPress() => Console.ReadKey();

    public static string UserInput() => (Console.ReadLine() ?? throw new ArgumentException("Can't Be Null Or Whitespace")).Trim();
}