using BrewOS.Domain.Entities;

namespace BrewOS.Application.UseCases.InsertCoin;

/// <summary>
/// Use case: insert a supported coin into the coffee machine.
/// Delegates validation and balance updates to the Domain aggregate.
/// </summary>
public sealed class InsertCoinUseCase
{
    private readonly CoffeeMachine _coffeeMachine;

    /// <summary>
    /// Creates the use case bound to a specific machine instance.
    /// </summary>
    /// <param name="coffeeMachine">Domain coffee machine aggregate.</param>
    public InsertCoinUseCase(CoffeeMachine coffeeMachine)
    {
        _coffeeMachine = coffeeMachine ?? throw new ArgumentNullException(nameof(coffeeMachine));
    }

    /// <summary>
    /// Executes coin insertion for the given denomination.
    /// </summary>
    /// <param name="valueInCents">Coin value in cents.</param>
    public void Execute(int valueInCents)
    {
        _coffeeMachine.InsertCoin(valueInCents);
    }
}
