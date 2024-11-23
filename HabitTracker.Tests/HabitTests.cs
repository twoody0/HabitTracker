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
        ProgressLog progressLog = new()
        {
            Date = DateTime.UtcNow,
            IsCompleted = true,
            Note = "Learning C#"
        };

        // Act
        habit.AddProgressLog(progressLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        Assert.Equal(progressLog, habit.ProgressLogs[0]);
    }

    /// <summary>
    /// Tests that an existing progress log is updated in the habit.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_ExistingLog_UpdatesLog()
    {
        // Arrange
        Habit habit = new("Coding");
        DateTime originalDate = DateTime.UtcNow;
        ProgressLog progressLog = new()
        {
            Date = originalDate,
            IsCompleted = true,
            Note = "Learning C#"
        };
        habit.AddProgressLog(progressLog);

        ProgressLog updatedLog = new()
        {
            Date = originalDate,
            IsCompleted = false,
            Note = "Learning C# and .NET"
        };

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
    /// Tests that a non-existing progress log is not updated in the habit.
    /// </summary>
    [Fact]
    public void UpdateProgressLog_NonExistingLog_DoesNotUpdate()
    {
        // Arrange
        Habit habit = new("Coding");
        ProgressLog progressLog = new()
        {
            Date = DateTime.UtcNow,
            IsCompleted = true,
            Note = "Learning C#"
        };
        habit.AddProgressLog(progressLog);
        ProgressLog updatedLog = new()
        {
            Date = DateTime.UtcNow.AddDays(-1),
            IsCompleted = false,
            Note = "Learning C# and .NET"
        };
        // Act
        habit.UpdateProgressLog(updatedLog);

        // Assert
        Assert.Single(habit.ProgressLogs);
        Assert.Equal(progressLog, habit.ProgressLogs[0]);
    }
}