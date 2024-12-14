using HabitTracker.Core.Enums;

namespace HabitTracker.Core.Entities;

/// <summary>
/// Represents a habit that can be tracked.
/// </summary>
public class Habit : EntityBase
{
    private readonly List<ProgressLog> _progressLogs = [];
    private int _frequency;
    private DateTime _startDate;

    /// <summary>
    /// Gets or sets the progress logs of the habit.
    /// </summary>
    public IReadOnlyList<ProgressLog> ProgressLogs => _progressLogs;

    /// <summary>
    /// Gets or sets the name of the habit.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the category of the habit.
    /// </summary>
    public HabitCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the unit of frequency for the habit.
    /// </summary>
    public FrequencyUnit FrequencyUnit { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the habit.
    /// </summary>
    public List<string> Tags { get; set; } = [];

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
    /// Gets or sets the number of frequency units for the habit.
    /// For example, a value of 2 with FrequencyUnit = Weekly means the habit occurs every 2 weeks.
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
    /// Initializes a new instance of the <see cref="Habit"/> class.
    /// </summary>
    /// <param name="name">The name of the habit.</param>
    /// <param name="startDate">The start date of the habit.</param>
    /// <param name="frequency">The frequency of the habit.</param>
    /// <param name="frequencyUnit">The frequency unit of the habit.</param>
    /// <param name="category">The category of the habit.</param>
    public Habit(string name, DateTime startDate, HabitCategory category = HabitCategory.Other, int frequency = 1, FrequencyUnit frequencyUnit = FrequencyUnit.Daily)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null or white space", nameof(name));
        }

        if (startDate > DateTime.UtcNow)
        {
            throw new ArgumentException("Start date cannot be in the future.", nameof(startDate));
        }

        Name = name;
        StartDate = startDate;
        Category = category;
        Frequency = frequency;
        FrequencyUnit = frequencyUnit;

        Tags = DefaultTagsProvider.GetTagsForCategory(category);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Habit"/> class.
    /// </summary>
    public Habit()
    {
        Name = string.Empty;
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

        DateTime normalizedStartDate = startDate.Date;
        DateTime normalizedEndDate = endDate.Date;

        int totalDays = (normalizedEndDate - normalizedStartDate).Days + 1;
        if (totalDays <= 0 || !_progressLogs.Any(log => log.Date >= normalizedStartDate && log.Date <= normalizedEndDate))
        {
            return 0;
        }

        int completedDays = _progressLogs
            .Where(log => log.Date >= normalizedStartDate && log.Date <= normalizedEndDate && log.IsCompleted)
            .Count();

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

        List<ProgressLog> logs = _progressLogs
            .Where(log => log.Date <= date && log.IsCompleted)
            .OrderByDescending(log => log.Date)
            .ToList();

        int streak = 0;
        DateTime? previousDate = null;

        foreach (ProgressLog log in logs)
        {
            if (previousDate.HasValue && (previousDate.Value - log.Date).Days > 1)
            {
                break; // Streak is broken
            }

            streak++;
            previousDate = log.Date;
        }

        return streak;
    }

    /// <summary>
    /// Gets all habits in the specified category.
    /// </summary>
    /// <param name="habits"></param>
    /// <param name="category">The category to filter by.</param>
    /// <returns>A list of habits in the specified category.</returns>
    public static List<Habit> GetHabitsByCategory(List<Habit> habits, HabitCategory category)
    {
        return habits.Where(h => h.Category == category).ToList();
    }

    /// <summary>
    /// Gets the next due date for the habit based on the frequency and frequency unit.
    /// </summary>
    /// <param name="lastCompletedDate">The date the habit was last completed.</param>
    /// <returns>The next due date for the habit.</returns>
    public DateTime GetNextDueDate(DateTime lastCompletedDate)
    {
        if (lastCompletedDate < StartDate)
        {
            throw new ArgumentException($"The last completed date ({lastCompletedDate:yyyy-MM-dd}) cannot be earlier than the start date ({StartDate:yyyy-MM-dd}).", nameof(lastCompletedDate));
        }

        return FrequencyUnit switch
        {
            FrequencyUnit.Daily => lastCompletedDate.AddDays(Frequency),
            FrequencyUnit.Weekly => lastCompletedDate.AddDays(Frequency * 7),
            FrequencyUnit.Monthly => lastCompletedDate.AddMonths(Frequency),
            _ => throw new NotImplementedException("Unsupported frequency unit.")
        };
    }

    /// <summary>
    /// Gets all habits with a specific tag.
    /// </summary>
    /// <param name="habits">The list of habits to search.</param>
    /// <param name="tag">The tag to filter by.</param>
    /// <returns>A list of habits with the specified tag.</returns>
    public static List<Habit> GetHabitsByTag(List<Habit> habits, string tag)
    {
        if (habits == null || habits.Count == 0)
        {
            return [];
        }

        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Tag cannot be null or empty.", nameof(tag));
        }

        return habits.Where(h => h.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Adds a tag to the habit.
    /// </summary>
    /// <param name="tag"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Tag cannot be null or empty.", nameof(tag));
        }
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
        {
            Tags.Add(tag);
        }
    }
}