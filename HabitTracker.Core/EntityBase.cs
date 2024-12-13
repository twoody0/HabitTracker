using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Core;

/// <summary>
/// Represents the base entity with common properties.
/// </summary>
public abstract class EntityBase
{
    private DateTime _modifiedDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityBase"/> class.
    /// </summary>
    protected EntityBase()
    {
        CreatedDate = DateTime.UtcNow;
        ModifiedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }

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
            _modifiedDate = value;
        }
    }
}