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
    /// Gets or sets the category of the habit.
    /// </summary>
    public string Category { get; set; }

    private DateTime _startDate;

    /// <summary>
    /// Gets or sets the start date of the habit.
    /// </summary>
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value > DateTime.UtcNow)
            {
                throw new ArgumentException("Start date cannot be in the future.", nameof(value));
            }
            _startDate = value.Date;
        }
    }

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
    /// <param name="startDate"></param>
    /// <exception cref="ArgumentException">Thrown when the habit name is null or white space.</exception>
    /// <param name="category"></param>
    public Habit(string habitName, DateTime startDate, string category)
    {
        if (string.IsNullOrWhiteSpace(habitName))
        {
            throw new ArgumentException($"{nameof(habitName)} cannot be null or white space", nameof(habitName));
        }

        if (startDate > DateTime.UtcNow)
        {
            throw new ArgumentException("Start date cannot be in the future.", nameof(startDate));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException($"{nameof(category)} cannot be null or white space", nameof(category));
        }

        Name = habitName;
        StartDate = startDate;
        Category = category;
    }

    /// <summary>
    /// Adds a progress log to the habit.
    /// </summary>
    /// <param name="progressLog">The progress log to add.</param>
    public void AddProgressLog(ProgressLog progressLog)
    {
        if (progressLog.Date < StartDate)
        {
            throw new ArgumentException("Progress log date cannot be before the habit's start date.");
        }
        if (progressLog.Date > DateTime.UtcNow)
        {
            throw new ArgumentException("Progress log date cannot be in the future.");
        }

        ArgumentNullException.ThrowIfNull(progressLog);
        _progressLogs.Add(progressLog);
    }

    /// <summary>
    /// Removes a progress log from the habit.
    /// </summary>
    /// <param name="date">The date of the progress log to remove.</param>
    /// <exception cref="InvalidOperationException">Thrown when no progress log is found for the specified date.</exception>
    public void RemoveProgressLog(DateTime date)
    {
        ProgressLog? log = _progressLogs.FirstOrDefault(item => item.Date == date);
        if (log is null)
        {
            throw new InvalidOperationException($"No progress log found for date: {date}");
        }
        _progressLogs.Remove(log);
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

    /// <summary>
    /// Gets the completion rate of the habit within a specified date range.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>The completion rate as a double.</returns>
    /// <exception cref="ArgumentException">Thrown when the start date is later than the end date.</exception>
    public double GetCompletionRate(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("The start date cannot be later than the end date.", nameof(startDate));
        }

        startDate = startDate.Date;
        endDate = endDate.Date;

        int totalDays = (endDate - startDate).Days + 1;
        if (totalDays == 0)
        {
            return 0;
        }

        int completedDays = _progressLogs.Count(log => log.Date.Date >= startDate && log.Date.Date <= endDate && log.IsCompleted);
        return (double)completedDays / totalDays * 100;
    }

    /// <summary>
    /// Gets the current streak of completed habit logs up to the specified date.
    /// </summary>
    /// <param name="date">The date up to which the streak is calculated.</param>
    /// <returns>The number of consecutive days the habit was completed up to the specified date.</returns>
    public int GetStreak(DateTime date)
    {
        date = date.Date;
        int streak = 0;
        int currentStreak = 0;
        ProgressLog? previousLog = null;
        foreach (ProgressLog log in _progressLogs.Where(log => log.Date.Date <= date).OrderByDescending(log => log.Date))
        {
            if (previousLog is not null && (previousLog.Date - log.Date).Days > 1)
            {
                break;
            }
            if (log.IsCompleted)
            {
                currentStreak++;
            }
            else
            {
                streak = Math.Max(streak, currentStreak);
                currentStreak = 0;
            }
            previousLog = log;
        }
        return Math.Max(streak, currentStreak);
    }

    /// <summary>
    /// Gets all habits in the specified category.
    /// </summary>
    /// <param name="habits"></param>
    /// <param name="category">The category to filter by.</param>
    /// <returns>A list of habits in the specified category.</returns>
    public static List<Habit> GetHabitsByCategory(List<Habit> habits, string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Category cannot be null or white space.", nameof(category));
        }

        return habits.Where(h => h.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}