namespace BrewOS.Api.Contracts.Requests;

/// <summary>
/// Request body for inserting a coin into the machine.
/// </summary>
public sealed class InsertCoinRequest
{
    /// <summary>
    /// Coin denomination in cents. Supported values: 5, 10, 20, 50, 100, 200.
    /// </summary>
    public int ValueInCents { get; init; }
}
