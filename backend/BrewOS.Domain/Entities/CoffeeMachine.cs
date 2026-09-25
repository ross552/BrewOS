using BrewOS.Domain.Exceptions;
using BrewOS.Domain.Results;
using BrewOS.Domain.Services;
using BrewOS.Domain.ValueObjects;

namespace BrewOS.Domain.Entities;

/// <summary>
/// Aggregate root that models a virtual coffee vending machine.
/// Encapsulates coin insertion, balance tracking, purchase, and reset rules.
/// </summary>
public sealed class CoffeeMachine
{
    private readonly ChangeCalculator _changeCalculator;
    private int _balanceInCents;

    /// <summary>
    /// Creates a coffee machine with a zero balance.
    /// </summary>
    /// <param name="changeCalculator">
    /// Optional calculator used to break remaining balance into coins after purchase.
    /// When omitted, a default <see cref="ChangeCalculator"/> is used.
    /// </param>
    public CoffeeMachine(ChangeCalculator? changeCalculator = null)
    {
        _changeCalculator = changeCalculator ?? new ChangeCalculator();
        _balanceInCents = 0;
    }

    /// <summary>
    /// Inserts a supported coin and increases the current balance.
    /// <para>
    /// Business rule: invalid coins (for example 1c or 2c) must be rejected
    /// before they can affect the balance. Validation is enforced by <see cref="Coin"/>.
    /// </para>
    /// </summary>
    /// <param name="coin">A strongly typed, supported coin.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="coin"/> is null.</exception>
    public void InsertCoin(Coin coin)
    {
        ArgumentNullException.ThrowIfNull(coin);

        // Defense in depth: even if a Coin instance were constructed incorrectly,
        // unsupported denominations must never credit the balance.
        if (!Coin.IsSupported(coin.ValueInCents))
        {
            throw new ArgumentOutOfRangeException(
                nameof(coin),
                coin.ValueInCents,
                "Unsupported coin. Accepted denominations: 5, 10, 20, 50, 100, 200 cents.");
        }

        _balanceInCents += coin.ValueInCents;
    }

    /// <summary>
    /// Inserts a coin by denomination value.
    /// Rejects unsupported values such as 1 and 2 cents.
    /// </summary>
    /// <param name="valueInCents">Coin denomination in cents.</param>
    public void InsertCoin(int valueInCents)
    {
        InsertCoin(Coin.Create(valueInCents));
    }

    /// <summary>
    /// Returns the current inserted balance in cents.
    /// </summary>
    public int GetBalance() => _balanceInCents;

    /// <summary>
    /// Purchases the selected coffee when the balance is sufficient.
    /// <para>
    /// Business rules:
    /// <list type="bullet">
    /// <item>Purchase is rejected when balance is less than the coffee price.</item>
    /// <item>On success, the customer receives the coffee and change for any overpayment.</item>
    /// <item>After a successful purchase, the machine balance is reset to zero.</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="coffee">Coffee product to dispense.</param>
    /// <returns>Purchase result containing the coffee and change coins.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="coffee"/> is null.</exception>
    /// <exception cref="InsufficientBalanceException">
    /// Thrown when <see cref="GetBalance"/> is less than <see cref="Coffee.PriceInCents"/>.
    /// </exception>
    public PurchaseResult PurchaseCoffee(Coffee coffee)
    {
        ArgumentNullException.ThrowIfNull(coffee);

        if (_balanceInCents < coffee.PriceInCents)
        {
            throw new InsufficientBalanceException(_balanceInCents, coffee.PriceInCents);
        }

        var changeAmount = _balanceInCents - coffee.PriceInCents;
        var change = _changeCalculator.Calculate(changeAmount);

        // Business rule: balance clears after a completed purchase.
        _balanceInCents = 0;

        return new PurchaseResult(coffee, change);
    }

    /// <summary>
    /// Clears the inserted balance without dispensing a product.
    /// Useful for cancel/abort flows.
    /// </summary>
    public void Reset()
    {
        _balanceInCents = 0;
    }
}
