namespace BrewOS.Api.Contracts.Requests;

/// <summary>
/// Request body for purchasing a coffee from the machine.
/// </summary>
public sealed class PurchaseCoffeeRequest
{
    /// <summary>
    /// Catalog identifier of the coffee to purchase.
    /// </summary>
    public Guid CoffeeId { get; init; }
}
