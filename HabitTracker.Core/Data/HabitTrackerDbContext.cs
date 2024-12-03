using HabitTracker.Core.Entities;
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
        modelBuilder.Entity<Habit>()
            .HasMany(h => h.ProgressLogs)
            .WithOne()
            .HasForeignKey(pl => pl.HabitId);

        base.OnModelCreating(modelBuilder);
    }
}