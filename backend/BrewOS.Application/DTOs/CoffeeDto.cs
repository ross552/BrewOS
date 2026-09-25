namespace BrewOS.Application.DTOs;

/// <summary>
/// Data transfer object representing a coffee product for outer layers.
/// </summary>
public sealed class CoffeeDto
{
    /// <summary>
    /// Unique identifier of the coffee product.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Display name of the coffee product.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Unit price in cents.
    /// </summary>
    public int PriceInCents { get; init; }
}
