using HabitTracker.Core;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the ProgressLog class.
/// </summary>
public class ProgressLogTests
{
    /// <summary>
    /// Tests that the constructor initializes the ProgressLog with valid values.
    /// </summary>
    [Fact]
    public void Constructor_ValidValues_Initializes()
    {
        // Arrange
        DateTime date = DateTime.UtcNow;
        bool isCompleted = true;
        string note = "Test note";

        // Act
        ProgressLog progressLog = new(date, isCompleted, note);

        // Assert
        Assert.Equal(date, progressLog.Date);
        Assert.Equal(isCompleted, progressLog.IsCompleted);
        Assert.Equal(note, progressLog.Note);
    }
}