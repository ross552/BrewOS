using BrewOS.Application.DTOs;

namespace BrewOS.Application.Interfaces;

/// <summary>
/// Application contract for interacting with the virtual coffee machine.
/// Coordinates use cases and exposes DTO-based results to outer layers (API, tests).
/// </summary>
public interface ICoffeeMachineService
{
    /// <summary>
    /// Inserts a coin denomination into the machine.
    /// Invalid denominations are rejected by the Domain layer.
    /// </summary>
    /// <param name="valueInCents">Coin value in cents.</param>
    void InsertCoin(int valueInCents);

    /// <summary>
    /// Returns the current inserted balance in cents.
    /// </summary>
    int GetBalance();

    /// <summary>
    /// Returns the catalog of coffees available for purchase.
    /// </summary>
    IReadOnlyList<CoffeeDto> GetAvailableCoffees();

    /// <summary>
    /// Purchases a coffee by catalog identifier when the balance is sufficient.
    /// </summary>
    /// <param name="coffeeId">Identifier of the coffee to purchase.</param>
    /// <returns>Purchase outcome including dispensed coffee and change.</returns>
    PurchaseResultDto PurchaseCoffee(Guid coffeeId);
}
