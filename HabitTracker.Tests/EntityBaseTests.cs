using HabitTracker.Core;

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
        EntityBase entity = new();
        DateTime afterCreation = DateTime.UtcNow;

        // Act

        // Assert
        Assert.InRange(entity.CreatedDate, beforeCreation, afterCreation);
        Assert.InRange(entity.ModifiedDate, beforeCreation, afterCreation);
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    /// <summary>
    /// Tests that setting an invalid ModifiedDate throws an exception.
    /// </summary>
    [Fact]
    public void Constructor_InvalidModifiedDate_ThrowsException()
    {
        // Arrange
        EntityBase entity = new();

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => entity.ModifiedDate = DateTime.UtcNow.AddDays(-1));
    }

    /// <summary>
    /// Tests that the modification history tracks modified dates correctly.
    /// </summary>
    [Fact]
    public void ModificationHistory_ValidValues_ShouldTrackModifiedDates()
    {
        // Arrange
        EntityBase entity = new();
        DateTime initialModifiedDate = entity.ModifiedDate;

        // Act
        DateTime newModifiedDate = initialModifiedDate.AddDays(1);
        entity.ModifiedDate = newModifiedDate;

        // Assert
        Assert.Equal(2, entity.ModificationHistory.Count);
        Assert.Contains(initialModifiedDate, entity.ModificationHistory);
    }
}