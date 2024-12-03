namespace HabitTracker.ConsoleApp;

/// <summary>
/// The main entry point for the Habit Tracker application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method which initializes and runs the application.
    /// </summary>
    public static void Main()
    {
        using Application app = new();
        app.Run();
    }
}