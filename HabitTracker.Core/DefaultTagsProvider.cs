using HabitTracker.Core.Enums;

namespace HabitTracker.Core;

/// <summary>
/// Provides default tags for different habit categories.
/// </summary>
public static class DefaultTagsProvider
{
    /// <summary>
    /// A dictionary containing default tags for each habit category.
    /// </summary>
    public static readonly Dictionary<HabitCategory, List<string>> DefaultTags = new()
        {
            { HabitCategory.Wellness, new List<string> { "Exercise", "Fitness", "Mental Health" } },
            { HabitCategory.Productivity, new List<string> { "Time Management", "Focus", "Deadlines" } },
            { HabitCategory.PersonalDevelopment, new List<string> { "Learning", "Growth", "SkillBuilding" } },
            { HabitCategory.Hobbies, new List<string> { "Recreation", "Relaxation", "Creativity" } },
            { HabitCategory.Relationships, new List<string> { "Family", "Friends", "Connection" } },
            { HabitCategory.Finance, new List<string> { "Budgeting", "Saving", "Investing" } },
            { HabitCategory.Spirituality, new List<string> { "Meditation", "Mindfulness", "Faith" } },
            { HabitCategory.Other, new List<string> { "Miscellaneous", "Uncategorized" } }
        };

    /// <summary>
    /// Gets default tags for a given HabitCategory.
    /// </summary>
    /// <param name="category">The habit category.</param>
    /// <returns>A list of default tags for the specified category.</returns>
    public static List<string> GetTagsForCategory(HabitCategory category)
    {
        return DefaultTags.TryGetValue(category, out List<string>? tags) ? tags : [];
    }
}