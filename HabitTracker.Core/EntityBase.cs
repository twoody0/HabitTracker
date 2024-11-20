﻿namespace HabitTracker.Core;

/// <summary>
/// Represents the base entity with common properties.
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase"/> class.
    /// </summary>
    public EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        ModifiedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the creation date of the entity.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Gets or sets the modified date of the entity.
    /// </summary>
    public DateTime ModifiedDate { get; set; }
}