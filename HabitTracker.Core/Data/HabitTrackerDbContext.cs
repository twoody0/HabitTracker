using HabitTracker.Core.Entities;
using HabitTracker.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Core.Data;

/// <summary>
/// Represents the database context for the Habit Tracker application.
/// </summary>
public class HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the collection of habits.
    /// </summary>
    public DbSet<Habit> Habits { get; set; }

    /// <summary>
    /// Gets or sets the collection of progress logs.
    /// </summary>
    public DbSet<ProgressLog> ProgressLogs { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HabitTrackerDbContext"/> class.
    /// </summary>
    public HabitTrackerDbContext() : this(new DbContextOptionsBuilder<HabitTrackerDbContext>()
        .UseSqlite("Data Source=habittracker.db")
        .Options)
    {
    }

    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed without Tags
        modelBuilder.Entity<Habit>().HasData(
            new
            {
                Id = 1,
                Name = "Daily Exercise",
                Category = HabitCategory.PersonalDevelopment,
                FrequencyUnit = FrequencyUnit.Daily,
                Tags = "",
                StartDate = new DateTime(2024, 1, 1),
                Frequency = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            },
            new
            {
                Id = 2,
                Name = "Read Books",
                Category = HabitCategory.PersonalDevelopment,
                FrequencyUnit = FrequencyUnit.Weekly,
                Tags = "",
                StartDate = new DateTime(2024, 1, 1),
                Frequency = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<ProgressLog>().HasData(
            new
            {
                Id = 1,
                HabitId = 1,
                Date = new DateTime(2024, 1, 2),
                IsCompleted = true,
                Note = "Ran 3 miles",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            },
            new
            {
                Id = 2,
                HabitId = 2,
                Date = new DateTime(2024, 1, 3),
                IsCompleted = true,
                Note = "Finished 2 chapters",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            }
        );
    }
}