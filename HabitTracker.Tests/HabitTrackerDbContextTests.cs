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
    [Fact]
    public void CanAddAndRetrieveHabit()
    {
        // Arrange
        DbContextOptions<HabitTrackerDbContext> options = CreateInMemoryOptions();

        using (HabitTrackerDbContext context = new(options))
        {
            Habit habit = new("Learn C#", DateTime.UtcNow, HabitCategory.PersonalDevelopment, 1, FrequencyUnit.Daily);
            context.Habits.Add(habit);
            context.SaveChanges();
        }

        // Act
        using (HabitTrackerDbContext context = new(options))
        {
            Habit? retrievedHabit = context.Habits.FirstOrDefault(h => h.Name == "Learn C#");

            // Assert
            Assert.NotNull(retrievedHabit);
            Assert.Equal("Learn C#", retrievedHabit.Name);
            Assert.Equal(HabitCategory.PersonalDevelopment, retrievedHabit.Category);
        }
    }
}