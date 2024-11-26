using HabitTracker.Core;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the Habit class.
/// </summary>
public class HabitTests
{
    /// <summary>
    /// Tests that the constructor initializes the Habit object with a valid name.
    /// </summary>
    [Fact]
    public void Constructor_ValidName_Initializes()
    {
        // Arrange
        string habitName = "Test Habit";

        // Act
        Habit habit = new(habitName);

        // Assert
        Assert.Equal(habitName, habit.Name);
    }

    /// <summary>
    /// Tests that the constructor throws an ArgumentException when the name is null.
    /// </summary>
    [Fact]
    public void Constructor_NullName_ThrowsException()
    {
        // Arrange
        string? habitName = null;

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => new Habit(habitName!));
    }

    /// <summary>
    /// Tests that the constructor throws an ArgumentException when the name is whitespace.
    /// </summary>
    [Fact]
    public void Constructor_WhiteSpaceName_ThrowsException()
    {
        // Arrange
        string habitName = " ";

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => new Habit(habitName));
    }

    /// <summary>
    /// Tests that a valid progress log is added to the habit.
    /// </summary>
    [Fact]
    public void AddProgressLog_ValidProgressLog_AddsLog()
    {
        // Arrange
        Habit habit = new("Coding");
        ProgressLog progressLog = new(DateTime.UtcNow, true, "Learning C#");

        // Act
        habit.AddProgressLog(progressLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        Assert.Equal(progressLog, habit.ProgressLogs[0]);
    }

    /// <summary>
    /// Tests that an existing progress log is updated in the habit with new values.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_WithNewValues_UpdatesLog()
    {
        // Arrange
        Habit habit = new("Coding");
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
        Habit habit = new("Coding");
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
        Habit habit = new("Coding");
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
        Habit habit = new("Coding");
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
        Habit habit = new("Coding");

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
        Habit habit = new("Coding");

        // Act
        IReadOnlyList<ProgressLog> progressLogs = habit.ProgressLogs;

        // Assert
        Assert.Empty(progressLogs);
    }
}