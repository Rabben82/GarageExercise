namespace GarageExercise;

public static class UserInterface
{
    public static void ConsoleMessage(string message) => Console.WriteLine(message);
    public static int ReturnValidNumber() => int.TryParse(Console.ReadLine(), out int number) ? number : throw new ArgumentException("It's not a valid number");
    public static void ClearConsole() => Console.Clear();
}