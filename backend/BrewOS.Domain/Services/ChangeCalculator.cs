using BrewOS.Domain.ValueObjects;

namespace BrewOS.Domain.Services;

/// <summary>
/// Calculates change using a greedy algorithm over supported coin denominations.
/// <para>
/// Business rule example: change of 150 cents yields 100 + 50.
/// Because all supported denominations are canonical for this currency set,
/// the greedy choice produces an optimal coin count.
/// </para>
/// </summary>
public sealed class ChangeCalculator
{
    /// <summary>
    /// Breaks an amount into the fewest coins using denominations
    /// 200, 100, 50, 20, 10, and 5 (largest first).
    /// </summary>
    /// <param name="amountInCents">Change amount in cents; must be non-negative and divisible by 5.</param>
    /// <returns>Coins making up the change, ordered from largest denomination to smallest.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="amountInCents"/> is negative or not payable with supported coins
    /// (not divisible by the smallest denomination of 5).
    /// </exception>
    public IReadOnlyList<Coin> Calculate(int amountInCents)
    {
        if (amountInCents < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amountInCents), amountInCents, "Change amount cannot be negative.");
        }

        if (amountInCents % 5 != 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amountInCents),
                amountInCents,
                "Change must be divisible by 5 cents because that is the smallest accepted denomination.");
        }

        if (amountInCents == 0)
        {
            return Array.Empty<Coin>();
        }

        var remaining = amountInCents;
        var coins = new List<Coin>();

        foreach (var denomination in Coin.SupportedDenominations)
        {
            while (remaining >= denomination)
            {
                coins.Add(Coin.Create(denomination));
                remaining -= denomination;
            }
        }

        return coins.AsReadOnly();
    }
}
