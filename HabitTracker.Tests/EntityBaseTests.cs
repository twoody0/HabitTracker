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
}