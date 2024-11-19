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
    /// <param name="name">The name of the entity.</param>
    /// <param name="createdDate">The creation date of the entity.</param>
    [Theory]
    [InlineData("2021-01-01", "2024-01-01")]
    public void Constructor_ValidObjects_Initialzes(DateTime createdDate, DateTime modifiedDate)
    {
        // Arrange
        EntityBase entity = new();

        // Act

        // Assert
        Assert.Equal(createdDate, entity.CreatedDate);
        Assert.Equal(modifiedDate, entity.ModifiedDate);
        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    /// <summary>
    /// Tests that the constructor of <see cref="EntityBase"/> throws an exception when the name is null or empty.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="date">The creation date of the entity.</param>
    [Theory]
    [InlineData(null, "2021-01-01")]
    [InlineData("", "2021-01-01")]
    public void Constructor_NullOrEmptyName_ThrowsException(string? name, DateTime date)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => new EntityBase(name!, date));
    }
}