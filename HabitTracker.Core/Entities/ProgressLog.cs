using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Core;

/// <summary>
/// Represents a progress log for a habit.
/// </summary>
public class ProgressLog : EntityBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressLog"/> class with the specified date, completion status, and note.
    /// </summary>
    /// <param name="date">The date of the progress log entry.</param>
    /// <param name="isCompleted">Indicates whether the habit was completed on this date.</param>
    /// <param name="note">An optional note for the progress log entry.</param>
    /// <exception cref="ArgumentException">Thrown when the date is in the future.</exception>
    public ProgressLog(DateTime date, bool isCompleted, string? note = null)
    {
        if (date > DateTime.UtcNow)
        {
            throw new ArgumentException("The date cannot be in the future.", nameof(date));
        }
        Date = date;
        IsCompleted = isCompleted;
        Note = note;
    }

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

    /// <summary>
    /// Gets or sets the HabitId, which is the foreign key to the Habit entity.
    /// </summary>
    [ForeignKey("Habit")]
    public int HabitId { get; set; }
}