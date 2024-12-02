using HabitTracker.Core;

namespace HabitTracker.Tests;

/// <summary>
/// Provides unit tests for the DefaultTagsProvider class.
/// </summary>
public class DefaultTagsProviderTests
{
    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Wellness category.
    /// </summary>
    [Fact]
    public void HabitConstructor_WellnessCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Exercise", DateTime.UtcNow.AddDays(-1), HabitCategory.Wellness);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Exercise", tags);
        Assert.Contains("Fitness", tags);
        Assert.Contains("Mental Health", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Productivity category.
    /// </summary>
    [Fact]
    public void HabitConstructor_ProductivityCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Time Management", DateTime.UtcNow.AddDays(-1), HabitCategory.Productivity);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Time Management", tags);
        Assert.Contains("Focus", tags);
        Assert.Contains("Deadlines", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Personal Development category.
    /// </summary>
    [Fact]
    public void HabitConstructor_PersonalDevelopmentCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Learning", DateTime.UtcNow.AddDays(-1), HabitCategory.PersonalDevelopment);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Learning", tags);
        Assert.Contains("Growth", tags);
        Assert.Contains("SkillBuilding", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Hobbies category.
    /// </summary>
    [Fact]
    public void HabitConstructor_HobbiesCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Recreation", DateTime.UtcNow.AddDays(-1), HabitCategory.Hobbies);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Recreation", tags);
        Assert.Contains("Relaxation", tags);
        Assert.Contains("Creativity", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Relationships category.
    /// </summary>
    [Fact]
    public void HabitConstructor_RelationshipsCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Family", DateTime.UtcNow.AddDays(-1), HabitCategory.Relationships);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Family", tags);
        Assert.Contains("Friends", tags);
        Assert.Contains("Connection", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Finance category.
    /// </summary>
    [Fact]
    public void HabitConstructor_FinanceCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Budgeting", DateTime.UtcNow.AddDays(-1), HabitCategory.Finance);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Budgeting", tags);
        Assert.Contains("Saving", tags);
        Assert.Contains("Investing", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Spirituality category.
    /// </summary>
    [Fact]
    public void HabitConstructor_SpiritualityCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Meditation", DateTime.UtcNow.AddDays(-1), HabitCategory.Spirituality);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Meditation", tags);
        Assert.Contains("Mindfulness", tags);
        Assert.Contains("Faith", tags);
    }

    /// <summary>
    /// Tests that the Habit constructor assigns default tags for the Other category.
    /// </summary>
    [Fact]
    public void HabitConstructor_OtherCategory_AssignsDefaultTags()
    {
        // Arrange
        Habit habit = new("Miscellaneous", DateTime.UtcNow.AddDays(-1), HabitCategory.Other);

        // Act
        List<string> tags = habit.Tags;

        // Assert
        Assert.Contains("Miscellaneous", tags);
        Assert.Contains("Uncategorized", tags);
    }

    /// <summary>
    /// Tests that GetTagsForCategory returns the default tags for a valid category.
    /// </summary>
    [Fact]
    public void GetTagsForCategory_ValidCategory_ReturnsDefaultTags()
    {
        // Arrange
        HabitCategory category = HabitCategory.Wellness;

        // Act
        List<string> tags = DefaultTagsProvider.GetTagsForCategory(category);

        // Assert
        Assert.Contains("Exercise", tags);
        Assert.Contains("Fitness", tags);
        Assert.Contains("Mental Health", tags);
    }
}