namespace BrewOS.Application.DTOs;

/// <summary>
/// Data transfer object representing the outcome of a successful purchase.
/// </summary>
public sealed class PurchaseResultDto
{
    /// <summary>
    /// Coffee that was dispensed.
    /// </summary>
    public required CoffeeDto Coffee { get; init; }

    /// <summary>
    /// Change coins returned to the customer, expressed as denomination values in cents.
    /// </summary>
    public required IReadOnlyList<int> ChangeInCents { get; init; }

    /// <summary>
    /// Total change amount in cents.
    /// </summary>
    public int ChangeTotalInCents { get; init; }
}
