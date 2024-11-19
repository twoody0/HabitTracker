namespace HabitTracker.Core;

/// <summary>
/// Represents the base entity with common properties.
/// </summary>
public class EntityBase
{
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