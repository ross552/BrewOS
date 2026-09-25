using BrewOS.Domain.Entities;
using BrewOS.Domain.ValueObjects;

namespace BrewOS.Domain.Results;

/// <summary>
/// Outcome of a successful coffee purchase.
/// Contains the dispensed product and the change coins returned to the customer.
/// </summary>
public sealed class PurchaseResult
{
    /// <summary>
    /// Coffee product that was purchased.
    /// </summary>
    public Coffee Coffee { get; }

    /// <summary>
    /// Change returned using supported coin denominations (greedy breakdown).
    /// Empty when the inserted balance exactly matches the price.
    /// </summary>
    public IReadOnlyList<Coin> Change { get; }

    /// <summary>
    /// Total change amount in cents (sum of <see cref="Change"/>).
    /// </summary>
    public int ChangeTotalInCents { get; }

    /// <summary>
    /// Creates a purchase result for a completed transaction.
    /// </summary>
    /// <param name="coffee">Dispensed coffee.</param>
    /// <param name="change">Change coins returned to the customer.</param>
    public PurchaseResult(Coffee coffee, IReadOnlyList<Coin> change)
    {
        ArgumentNullException.ThrowIfNull(coffee);
        ArgumentNullException.ThrowIfNull(change);

        Coffee = coffee;
        Change = change;
        ChangeTotalInCents = change.Sum(c => c.ValueInCents);
    }
}
