namespace HabitTracker.Core;

/// <summary>
/// Represents a progress log for a habit.
/// </summary>
public class ProgressLog
{
    /// <summary>
    /// Gets the unique identifier for the progress log.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the date of the progress log entry.
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the habit was completed on this date.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets an optional note for the progress log entry.
    /// </summary>
    public string? Note { get; set; }
}