namespace HabitTracker.Core;

/// <summary>
/// Represents a habit that can be tracked.
/// </summary>
public class Habit : EntityBase
{
    /// <summary>
    /// Gets or sets the name of the habit.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the habit.
    /// </summary>
    public int Frequency { get; set; }

    /// <summary>
    /// Gets or sets the progress logs of the habit.
    /// </summary>
    public List<ProgressLog> ProgressLogs { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Habit"/> class.
    /// </summary>
    /// <param name="habitName">The name of the habit.</param>
    /// <exception cref="ArgumentException">Thrown when the habit name is null or white space.</exception>
    public Habit(string habitName)
    {
        if (string.IsNullOrWhiteSpace(habitName))
        {
            throw new ArgumentException($"{nameof(habitName)} cannot be null or white space", nameof(habitName));
        }
        Name = habitName;
    }
}