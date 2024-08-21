using pv178_project.Models;

namespace pv178_project.Utils;

public static class ConsoleUtils
{
    public static void SetCursorPosition(Position position)
    {
        Console.SetCursorPosition(position.X, position.Y);
    }

    public static void WriteAtPosition(string text, Position position)
    {
        SetCursorPosition(position);
        Console.Write(text);
    }

    public static void WriteAtPosition(string text, int x, int y)
    {
        WriteAtPosition(text, new Position(x, y));
    }

    public static void WriteInverted(string text)
    {
        InvertConsoleColors();
        Console.WriteLine(text);
        InvertConsoleColors();
    }

    private static void InvertConsoleColors()
    {
        (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
    }
}