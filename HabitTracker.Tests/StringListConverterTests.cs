using HabitTracker.Core;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the StringListConverter class.
/// </summary>
public class StringListConverterTests
{
    /// <summary>
    /// Tests that the constructor initializes the StringListConverter with valid values.
    /// </summary>
    [Fact]
    public void Constructor_ValidValues_Initializes()
    {
        // Arrange
        StringListConverter converter = new();

        // Act
        List<string> list = new() { "item1", "item2", "item3" };
        string result = (string)converter.ConvertToProvider(list)!;

        // Assert
        Assert.Equal("item1,item2,item3", result);
    }

    /// <summary>
    /// Tests that the constructor initializes the StringListConverter with valid values.
    /// </summary>
    [Fact]
    public void Constructor_ValidValues_Initializes2()
    {
        // Arrange
        StringListConverter converter = new();

        // Act
        string value = "item1,item2,item3";
        List<string> result = (List<string>)converter.ConvertFromProvider(value)!;

        // Assert
        Assert.Equal(new List<string> { "item1", "item2", "item3" }, result);
    }
}