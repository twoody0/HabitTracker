using HabitTracker.Core;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the Habit class.
/// </summary>
public class HabitTests
{
    /// <summary>
    /// Tests that the constructor initializes the Habit object with a valid name and sets the StartDate.
    /// </summary>
    [Fact]
    public void Constructor_ValidName_Initializes()
    {
        // Arrange
        string habitName = "Test Habit";
        DateTime startDate = DateTime.UtcNow.Date;

        // Act
        Habit habit = new(habitName, startDate);

        // Assert
        Assert.Equal(habitName, habit.Name);
        Assert.Equal(startDate, habit.StartDate);
    }

    /// <summary>
    /// Tests that the constructor throws an ArgumentException when the name is null.
    /// </summary>
    [Fact]
    public void Constructor_InvalidName_ThrowsException()
    {
        // Arrange
        DateTime startDate = DateTime.UtcNow.Date;

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => new Habit(null!, startDate));
        Assert.Throws<ArgumentException>(() => new Habit(" ", startDate));
    }

    /// <summary>
    /// Tests that the constructor sets the category to 'Other' when no category is provided.
    /// </summary>
    [Fact]
    public void Constructor_NoCategoryProvided_SetsCategoryToOther()
    {
        // Arrange
        string habitName = "Miscellaneous Habit";
        DateTime startDate = DateTime.UtcNow.AddDays(-5);

        // Act
        Habit habit = new(habitName, startDate);

        // Assert
        Assert.Equal(HabitCategory.Other, habit.Category);
    }

    /// <summary>
    /// Tests that the default frequency unit is set to daily.
    /// </summary>
    [Fact]
    public void Constructor_DefaultFrequencyUnit_IsDaily()
    {
        // Arrange
        string habitName = "Exercise";
        DateTime startDate = DateTime.UtcNow.Date;

        // Act
        Habit habit = new(habitName, startDate);

        // Assert
        Assert.Equal(1, habit.Frequency);
        Assert.Equal(FrequencyUnit.Daily, habit.FrequencyUnit);
    }

    /// <summary>
    /// Tests that a valid progress log is added to the habit.
    /// </summary>
    [Fact]
    public void AddProgressLog_ValidProgressLog_AddsLog()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        ProgressLog progressLog = new(DateTime.UtcNow, true, "Learning C#");

        // Act
        habit.AddProgressLog(progressLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        Assert.Equal(progressLog, habit.ProgressLogs[0]);
    }

    /// <summary>
    /// Tests that adding a null progress log throws an NullReferenceException.
    /// </summary>
    [Fact]
    public void AddProgressLog_NullLog_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act

        // Assert
        Assert.Throws<NullReferenceException>(() => habit.AddProgressLog(null!));
    }

    /// <summary>
    /// Tests that an existing progress log is updated in the habit with new values.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_WithNewValues_UpdatesLog()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime originalDate = DateTime.UtcNow;
        ProgressLog progressLog = new(originalDate, true, "Learning C#");
        habit.AddProgressLog(progressLog);

        ProgressLog updatedLog = new(originalDate, false, "Learning C# and .NET");

        // Act
        habit.UpdateProgressLog(updatedLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        ProgressLog result = habit.ProgressLogs[0];
        Assert.Equal(updatedLog.Date, result.Date);
        Assert.Equal(updatedLog.IsCompleted, result.IsCompleted);
        Assert.Equal(updatedLog.Note, result.Note);
        Assert.True(result.ModifiedDate > originalDate);
    }

    /// <summary>
    /// Tests that an existing progress log is updated in the habit with modified existing log.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_WithModifiedExistingLog_UpdatesLog()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime originalDate = DateTime.UtcNow;
        ProgressLog progressLog = new(originalDate, true, "Learning C#");
        habit.AddProgressLog(progressLog);

        // Act
        progressLog.IsCompleted = false;
        progressLog.Note = "Learning C# and .NET";
        habit.UpdateProgressLog(progressLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        ProgressLog result = habit.ProgressLogs[0];
        Assert.Equal(progressLog.Date, result.Date);
        Assert.Equal(progressLog.IsCompleted, result.IsCompleted);
        Assert.Equal(progressLog.Note, result.Note);
        Assert.True(result.ModifiedDate > originalDate);
    }

    /// <summary>
    /// Tests that a non-existing progress log is not updated in the habit and throws an InvalidOperationException.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_NonExistingLog_DoesNotUpdate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        ProgressLog progressLog = new(DateTime.UtcNow, true, "Learning C#");

        // Act
        habit.AddProgressLog(progressLog);
        ProgressLog updatedLog = new(DateTime.UtcNow, false, "Learning C# and .NET")
        {
            Date = DateTime.UtcNow.AddDays(-1),
        };

        // Assert
        Assert.Throws<InvalidOperationException>(() => habit.UpdateProgressLog(updatedLog));
        Assert.Single(habit.ProgressLogs);
        Assert.Equal(progressLog, habit.ProgressLogs[0]);
    }

    /// <summary>
    /// Tests that the Frequency property is set correctly.
    /// </summary>
    [Fact]
    public void Frequency_ValidFrequency_SetsFrequency()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        int frequency = 3;

        // Act
        habit.Frequency = frequency;

        // Assert
        Assert.Equal(frequency, habit.Frequency);
    }

    /// <summary>
    /// Tests that setting the Frequency property to zero throws an ArgumentException.
    /// </summary>
    [Fact]
    public void Frequency_ZeroFrequency_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => habit.Frequency = 0);
    }

    /// <summary>
    /// Tests that the ProgressLogs property returns an empty list when no progress logs are added.
    /// </summary>
    [Fact]
    public void ProgressLogs_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act
        IReadOnlyList<ProgressLog> progressLogs = habit.ProgressLogs;

        // Assert
        Assert.Empty(progressLogs);
    }

    /// <summary>
    /// Tests that the ProgressLogs property returns the added logs.
    /// </summary>
    [Fact]
    public void ProgressLogs_AddedLogs_ReturnsLogs()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        ProgressLog progressLog1 = new(DateTime.UtcNow, true, "Learning C#");
        ProgressLog progressLog2 = new(DateTime.UtcNow.AddDays(-1), false, "Learning .NET");
        habit.AddProgressLog(progressLog1);
        habit.AddProgressLog(progressLog2);
        // Act
        IReadOnlyList<ProgressLog> progressLogs = habit.ProgressLogs;
        // Assert
        Assert.Equal(2, progressLogs.Count);
        Assert.Equal(progressLog1, progressLogs[0]);
        Assert.Equal(progressLog2, progressLogs[1]);
    }

    /// <summary>
    /// Tests that the GetCompletionRate method returns zero when there are no progress logs.
    /// </summary>
    [Fact]
    public void GetCompletionRate_NoLogs_ReturnsZero()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act
        double completionRate = habit.GetCompletionRate(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow);

        // Assert
        Assert.Equal(0, completionRate);
    }

    /// <summary>
    /// Tests that the GetCompletionRate method throws an ArgumentException when the start date is later than the end date.
    /// </summary>
    [Fact]
    public void GetCompletionRate_StartDateLaterThanEndDate_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => habit.GetCompletionRate(DateTime.UtcNow.AddDays(1), DateTime.UtcNow));
    }

    /// <summary>
    /// Tests that the GetCompletionRate method returns one when all progress logs are completed.
    /// </summary>
    [Fact]
    public void GetCompletionRate_AllLogsCompleted_ReturnsOneHundred()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;
        habit.AddProgressLog(new(today, true));
        habit.AddProgressLog(new(today.AddDays(-1), true));
        habit.AddProgressLog(new(today.AddDays(-2), true));

        // Act
        double completionRate = habit.GetCompletionRate(today.AddDays(-2), today);

        // Assert
        Assert.Equal(100, completionRate, 2);
    }

    /// <summary>
    /// Tests that the GetCompletionRate method returns zero when all progress logs are not completed.
    /// </summary>
    [Fact]
    public void GetCompletionRate_AllLogsNotCompleted_ReturnsZero()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;
        habit.AddProgressLog(new(today, false));
        habit.AddProgressLog(new(today.AddDays(-1), false));
        habit.AddProgressLog(new(today.AddDays(-2), false));

        // Act
        double completionRate = habit.GetCompletionRate(today.AddDays(-2), today);

        // Assert
        Assert.Equal(0, completionRate);
    }

    /// <summary>
    /// Tests that the GetCompletionRate method returns the correct rate when there are mixed progress logs.
    /// </summary>
    [Fact]
    public void GetCompletionRate_MixedLogs_ReturnsCorrectRate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        DateTime today = DateTime.UtcNow.Date;

        habit.AddProgressLog(new(today, true));
        habit.AddProgressLog(new(today.AddDays(-1), false));
        habit.AddProgressLog(new(today.AddDays(-2), true));

        double actualRate = 2.0 / 3.0 * 100;

        // Act
        double completionRate = habit.GetCompletionRate(today.AddDays(-2), today);

        // Assert
        Assert.Equal(actualRate, completionRate, 2);
    }

    /// <summary>
    /// Tests that the GetStreak method returns zero when there are no progress logs.
    /// </summary>
    [Fact]
    public void GetStreak_NoLogs_ReturnsZero()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act
        int streak = habit.GetStreak(DateTime.UtcNow);

        // Assert
        Assert.Equal(0, streak);
    }

    /// <summary>
    /// Tests that the GetStreak method returns the correct streak when all logs are completed.
    /// </summary>
    [Fact]
    public void GetStreak_AllLogsCompleted_ReturnsStreak()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;

        habit.AddProgressLog(new(today, true));
        habit.AddProgressLog(new(today.AddDays(-1), true));
        habit.AddProgressLog(new(today.AddDays(-2), true));

        // Act
        int streak = habit.GetStreak(today);

        // Assert
        Assert.Equal(3, streak);
    }

    /// <summary>
    /// Tests that the GetStreak method returns zero when all logs are not completed.
    /// </summary>
    [Fact]
    public void GetStreak_AllLogsNotCompleted_ReturnsZero()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;

        habit.AddProgressLog(new(today, false));
        habit.AddProgressLog(new(today.AddDays(-1), false));
        habit.AddProgressLog(new(today.AddDays(-2), false));

        // Act
        int streak = habit.GetStreak(today);

        // Assert
        Assert.Equal(0, streak);
    }

    /// <summary>
    /// Tests that the GetStreak method returns the correct streak when there are mixed progress logs.
    /// </summary>
    [Fact]
    public void GetStreak_MixedLogs_ReturnsStreak()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;

        habit.AddProgressLog(new(today, true));
        habit.AddProgressLog(new(today.AddDays(-1), false));
        habit.AddProgressLog(new(today.AddDays(-2), true));

        // Act
        int streak = habit.GetStreak(today);

        // Assert
        Assert.Equal(1, streak);
    }

    /// <summary>
    /// Tests that the GetStreak method returns the correct streak when logs are completed in the past.
    /// </summary>
    [Fact]
    public void GetStreak_LogsCompleted_ReturnsStreak()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime today = DateTime.UtcNow.Date;

        habit.AddProgressLog(new(today, true));
        habit.AddProgressLog(new(today.AddDays(-1), true));
        habit.AddProgressLog(new(today.AddDays(-2), true));
        habit.AddProgressLog(new(today.AddDays(-3), true));

        // Act
        int streak = habit.GetStreak(today);

        // Assert
        Assert.Equal(4, streak);
    }

    /// <summary>
    /// Tests that the StartDate property is set correctly.
    /// </summary>
    [Fact]
    public void StartDate_ValidStartDate_SetsStartDate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5).Date);
        DateTime newStartDate = DateTime.UtcNow.AddDays(-10).Date;

        // Act
        habit.StartDate = newStartDate;

        // Assert
        Assert.Equal(newStartDate, habit.StartDate);
    }

    /// <summary>
    /// Tests that setting the StartDate property to a future date throws an ArgumentException.
    /// </summary>
    [Fact]
    public void StartDate_FutureDate_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime futureDate = DateTime.UtcNow.AddDays(1);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => habit.StartDate = futureDate);
    }

    /// <summary>
    /// Tests that the GetHabitsByCategory method returns the correct habits for a given category.
    /// </summary>
    [Fact]
    public void GetHabitsByCategory_ValidCategory_ReturnsHabits()
    {
        // Arrange
        HabitCategory category = HabitCategory.Hobbies;
        Habit habit1 = new("Coding", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);
        Habit habit2 = new("Reading", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);
        Habit habit3 = new("Exercise", DateTime.UtcNow.AddDays(-5), HabitCategory.Wellness);

        // Act
        List<Habit> habits = Habit.GetHabitsByCategory([habit1, habit2, habit3], category);

        // Assert
        Assert.Equal(2, habits.Count);
        Assert.Contains(habit1, habits);
        Assert.Contains(habit2, habits);
    }

    /// <summary>
    /// Tests that the GetNextDueDate method returns the correct next due date for a daily frequency.
    /// </summary>
    [Fact]
    public void GetNextDueDate_DailyFrequency_ReturnsNextDueDate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime lastCompletedDate = DateTime.UtcNow.AddDays(-2);

        // Act
        DateTime nextDueDate = habit.GetNextDueDate(lastCompletedDate);

        // Assert
        Assert.Equal(lastCompletedDate.AddDays(1), nextDueDate);
    }

    /// <summary>
    /// Tests that the GetNextDueDate method returns the correct next due date for a weekly frequency.
    /// </summary>
    [Fact]
    public void GetNextDueDate_WeeklyFrequency_ReturnsNextDueDate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-15), default, 1, FrequencyUnit.Weekly);
        DateTime lastCompletedDate = DateTime.UtcNow.AddDays(-14);

        // Act
        DateTime nextDueDate = habit.GetNextDueDate(lastCompletedDate);

        // Assert
        Assert.Equal(lastCompletedDate.AddDays(7), nextDueDate);
    }

    /// <summary>
    /// Tests that the GetNextDueDate method returns the correct next due date for a monthly frequency.
    /// </summary>
    [Fact]
    public void GetNextDueDate_MonthlyFrequency_ReturnsNextDueDate()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddMonths(-5), default, 1, FrequencyUnit.Monthly);
        DateTime lastCompletedDate = DateTime.UtcNow.AddMonths(-1);

        // Act
        DateTime nextDueDate = habit.GetNextDueDate(lastCompletedDate);

        // Assert
        Assert.Equal(lastCompletedDate.AddMonths(1), nextDueDate);
    }

    /// <summary>
    /// Tests that the GetNextDueDate method throws an ArgumentException when the last completed date is earlier than the start date.
    /// </summary>
    [Fact]
    public void GetNextDueDate_LastCompletedDateEarlierThanStartDate_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime lastCompletedDate = DateTime.UtcNow.AddDays(-10);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => habit.GetNextDueDate(lastCompletedDate));
    }

    /// <summary>
    /// Tests that the GetHabitsByTag method returns the correct habits for a given tag.
    /// </summary>
    [Fact]
    public void GetHabitsByTag_ValidTag_ReturnsHabits()
    {
        // Arrange
        Habit habit1 = new("Coding", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);
        habit1.AddTag("Programming");
        Habit habit2 = new("Morning Yoga", DateTime.UtcNow.AddDays(-5), HabitCategory.Wellness);
        Habit habit3 = new("Exercise", DateTime.UtcNow.AddDays(-5), HabitCategory.Wellness);

        // Act
        List<Habit> habits = Habit.GetHabitsByTag([habit1, habit2, habit3], "Programming");

        // Assert
        Assert.Single(habits);
        Assert.Contains(habit1, habits);
    }

    /// <summary>
    /// Tests that the GetHabitsByTag method throws an ArgumentException when an invalid tag is provided.
    /// </summary>
    [Fact]
    public void GetHabitsByTag_InvalidTag_ThrowsException()
    {
        // Arrange
        Habit habit1 = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => Habit.GetHabitsByTag([habit1], null!));
        Assert.Throws<ArgumentException>(() => Habit.GetHabitsByTag([habit1], " "));
    }

    /// <summary>
    /// Tests that adding a valid tag to the habit adds the tag.
    /// </summary>
    [Fact]
    public void AddTag_ValidTag_AddsTag()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);

        // Act
        habit.AddTag("Programming");

        // Assert
        Assert.Contains("Programming", habit.Tags);
    }

    /// <summary>
    /// Tests that adding a duplicate tag does not add the tag again.
    /// </summary>
    [Fact]
    public void AddTag_DuplicateTag_DoesNotAddTag()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);
        habit.AddTag("Programming");

        // Act
        habit.AddTag("Programming");

        // Assert
        Assert.Contains("Programming", habit.Tags);
    }

    /// <summary>
    /// Tests that adding an invalid tag throws an ArgumentException.
    /// </summary>
    [Fact]
    public void AddTag_InvalidTag_ThrowsException()
    {
        // Arrange
        Habit habit = new("Coding", DateTime.UtcNow.AddDays(-5), HabitCategory.Hobbies);
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => habit.AddTag(null!));
        Assert.Throws<ArgumentException>(() => habit.AddTag(" "));
    }
}