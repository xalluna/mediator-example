namespace MediatorTwo.Common;

public enum LogLevel
{
    Success,
    Warn,
    Error,
    Default
}

public static class Logger
{
    public static void Log(string label, string message, LogLevel level = LogLevel.Default)
    {
        Console.Write("[");

        Console.ForegroundColor = level switch
        {
            LogLevel.Default => ConsoleColor.Cyan,
            LogLevel.Success => ConsoleColor.Green,
            LogLevel.Warn => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
        
        Console.Write(label);
        Console.ResetColor();
        Console.Write("] ");
        Console.WriteLine(message);
    }
}