using BrewOS.Application.DTOs;
using BrewOS.Application.Mappings;
using BrewOS.Domain.Entities;

namespace BrewOS.Application.UseCases.PurchaseCoffee;

/// <summary>
/// Use case: purchase a coffee by catalog identifier.
/// Resolves the product, then delegates purchase rules to the Domain aggregate.
/// </summary>
public sealed class PurchaseCoffeeUseCase
{
    private readonly CoffeeMachine _coffeeMachine;

    /// <summary>
    /// Creates the use case bound to a specific machine instance.
    /// </summary>
    /// <param name="coffeeMachine">Domain coffee machine aggregate.</param>
    public PurchaseCoffeeUseCase(CoffeeMachine coffeeMachine)
    {
        _coffeeMachine = coffeeMachine ?? throw new ArgumentNullException(nameof(coffeeMachine));
    }

    /// <summary>
    /// Executes a purchase for the coffee identified by <paramref name="coffeeId"/>.
    /// </summary>
    /// <param name="coffeeId">Catalog identifier of the coffee to purchase.</param>
    /// <returns>Mapped purchase result DTO.</returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when <paramref name="coffeeId"/> does not match a catalog coffee.
    /// </exception>
    public PurchaseResultDto Execute(Guid coffeeId)
    {
        var coffee = Coffee.Catalog.FirstOrDefault(c => c.Id == coffeeId)
            ?? throw new KeyNotFoundException($"Coffee with id '{coffeeId}' was not found in the catalog.");

        var result = _coffeeMachine.PurchaseCoffee(coffee);
        return result.ToDto();
    }
}
