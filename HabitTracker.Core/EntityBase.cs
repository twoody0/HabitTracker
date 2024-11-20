namespace HabitTracker.Core;

/// <summary>
/// Represents the base entity with common properties.
/// </summary>
public class EntityBase
{
    private DateTime _modifiedDate;
    private readonly List<DateTime> _modificationHistory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase"/> class.
    /// </summary>
    public EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        _modificationHistory = [];
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
    public DateTime ModifiedDate
    {
        get => _modifiedDate;
        set
        {
            if (value < CreatedDate)
            {
                throw new ArgumentException("The modified date cannot be earlier than the created date.");
            }
            _modificationHistory.Add(value);
            _modifiedDate = value;
        }
    }

    /// <summary>
    /// Gets the history of modification dates.
    /// </summary>
    public IReadOnlyList<DateTime> ModificationHistory => _modificationHistory.AsReadOnly();
}