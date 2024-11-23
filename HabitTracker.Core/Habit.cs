namespace HabitTracker.Core;

/// <summary>
/// Represents a habit that can be tracked.
/// </summary>
public class Habit : EntityBase
{
    private readonly List<ProgressLog> _progressLogs = [];
    private int _frequency;

    /// <summary>
    /// Gets or sets the name of the habit.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the habit.
    /// </summary>
    public int Frequency
    {
        get => _frequency;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("The frequency must be greater than zero.", nameof(value));
            }
            _frequency = value;
        }
    }

    /// <summary>
    /// Gets or sets the progress logs of the habit.
    /// </summary>
    public IReadOnlyList<ProgressLog> ProgressLogs => _progressLogs;

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

    /// <summary>
    /// Adds a progress log to the habit.
    /// </summary>
    /// <param name="progressLog">The progress log to add.</param>
    public void AddProgressLog(ProgressLog progressLog)
    {
        _progressLogs.Add(progressLog);
    }

    /// <summary>
    /// Updates an existing progress log of the habit.
    /// </summary>
    /// <param name="progressLog">The progress log to update.</param>
    public void UpdateProgressLog(ProgressLog progressLog)
    {
        ProgressLog? log = _progressLogs.FirstOrDefault(item => item.Date == progressLog.Date);

        if (log is null)
        {
            throw new InvalidOperationException($"No progress log found for date: {progressLog.Date}");
        }

        log.IsCompleted = progressLog.IsCompleted;
        log.Note = progressLog.Note;
        log.ModifiedDate = DateTime.UtcNow;
    }
}