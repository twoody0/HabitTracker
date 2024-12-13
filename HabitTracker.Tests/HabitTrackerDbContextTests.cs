using HabitTracker.Core.Data;
using HabitTracker.Core.Entities;
using HabitTracker.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Tests;

/// <summary>
/// Contains unit tests for the HabitTrackerDbContext class.
/// </summary>
public class HabitTrackerDbContextTests
{
    private static DbContextOptions<HabitTrackerDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<HabitTrackerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    /// <summary>
    /// Tests if a habit can be added and retrieved from the database.
    /// </summary>
    [Theory]
    [InlineData("Learn C#", HabitCategory.PersonalDevelopment)]
    [InlineData("Exercise", HabitCategory.Wellness)]
    public void CanAddAndRetrieveHabit(string name, HabitCategory category)
    {
        // Arrange
        DbContextOptions<HabitTrackerDbContext> options = CreateInMemoryOptions();

        using (HabitTrackerDbContext context = new(options))
        {
            Habit habit = new(name, DateTime.UtcNow, category);
            context.Habits.Add(habit);
            context.SaveChanges();
        }

        // Act
        using (HabitTrackerDbContext context = new(options))
        {
            Habit? retrievedHabit = context.Habits.FirstOrDefault(h => h.Name == name);

            // Assert
            Assert.NotNull(retrievedHabit);
            Assert.Equal(name, retrievedHabit!.Name);
            Assert.Equal(category, retrievedHabit.Category);
            Assert.True(retrievedHabit.Id > 0);
        }
    }
}