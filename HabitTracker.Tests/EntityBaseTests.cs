using HabitTracker.Core;
using HabitTracker.Core.Entities;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the <see cref="EntityBase"/> class.
/// </summary>
public class EntityBaseTests
{
    /// <summary>
    /// Tests that the constructor of <see cref="EntityBase"/> initializes the object correctly.
    /// </summary>
    [Fact]
    public void Constructor_ValidObjects_Initializes()
    {
        // Arrange
        DateTime beforeCreation = DateTime.UtcNow;
        Habit entity = new("Coding", DateTime.UtcNow.AddDays(-5));
        DateTime afterCreation = DateTime.UtcNow;

        // Act

        // Assert
        Assert.InRange(entity.CreatedDate, beforeCreation, afterCreation);
        Assert.InRange(entity.ModifiedDate, beforeCreation, afterCreation);
        Assert.Equal(0, entity.Id);
    }

    /// <summary>
    /// Tests that setting an invalid ModifiedDate throws an exception.
    /// </summary>
    [Fact]
    public void Constructor_InvalidModifiedDate_ThrowsException()
    {
        // Arrange
        Habit entity = new("Coding", DateTime.UtcNow.AddDays(-5));

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => entity.ModifiedDate = DateTime.UtcNow.AddDays(-1));
    }
}