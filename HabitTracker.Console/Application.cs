using HabitTracker.Core.Data;
using HabitTracker.Core.Entities;
using HabitTracker.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.ConsoleApp;

/// <summary>
/// Represents the main application for the Habit Tracker.
/// </summary>
public class Application : IDisposable
{
    private readonly HabitTrackerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Application"/> class.
    /// </summary>
    public Application()
    {
        DbContextOptions<HabitTrackerDbContext> options = new DbContextOptionsBuilder<HabitTrackerDbContext>()
            .UseSqlite("Data Source=habittracker.db")
            .Options;

        _context = new HabitTrackerDbContext(options);
    }

    /// <summary>
    /// Runs the application.
    /// </summary>
    public void Run()
    {
        Console.WriteLine("Welcome to the Habit Tracker!");
        DisplayMenu();
    }

    /// <summary>
    /// Displays the main menu of the application.
    /// </summary>
    private void DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine($"{Environment.NewLine}Select an option:");
            Console.WriteLine("1. View all habits");
            Console.WriteLine("2. Add a new habit");
            Console.WriteLine("3. Exit");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    ViewAllHabits();
                    break;

                case "2":
                    AddHabit();
                    break;

                case "3":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays all habits.
    /// </summary>
    private void ViewAllHabits()
    {
        List<Habit> habits = _context.Habits.ToList();

        if (habits.Count == 0)
        {
            Console.WriteLine("No habits found.");
        }
        else
        {
            Console.WriteLine("Here are your habits:");
            foreach (Habit habit in habits)
            {
                Console.WriteLine($"- {habit.Name} (Category: {habit.Category}, Frequency: {habit.Frequency} {habit.FrequencyUnit})");
            }
        }
    }

    /// <summary>
    /// Adds a new habit.
    /// </summary>
    private void AddHabit()
    {
        Console.Write("Enter the habit name: ");
        string habitName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter the category (e.g., Health, Work, Fitness): ");
        string categoryInput = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter the frequency (e.g., 1, 2): ");
        int frequency = int.TryParse(Console.ReadLine(), out int result) ? result : 1;

        Console.Write("Enter the frequency unit (Daily, Weekly, Monthly): ");
        string frequencyUnitInput = Console.ReadLine() ?? string.Empty;

        if (Enum.TryParse(categoryInput, true, out HabitCategory category) &&
            Enum.TryParse(frequencyUnitInput, true, out FrequencyUnit frequencyUnit))
        {
            Habit habit = new(habitName, DateTime.UtcNow, category, frequency, frequencyUnit);
            _context.Habits.Add(habit);
            _context.SaveChanges();

            Console.WriteLine("Habit added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid category or frequency unit. Please try again.");
        }
    }

    /// <summary>
    /// Disposes the resources used by the application.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}