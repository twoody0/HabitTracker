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
}