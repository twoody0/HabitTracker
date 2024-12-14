using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HabitTracker.Core;

/// <summary>
/// Used to convert a list of strings to a single string and vice versa.
/// </summary>
public class StringListConverter : ValueConverter<List<string>, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringListConverter"/> class.
    /// </summary>
    public StringListConverter() : base(
        v => string.Join(',', v),
        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
    { }
}