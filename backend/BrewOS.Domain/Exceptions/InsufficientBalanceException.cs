namespace BrewOS.Domain.Exceptions;

/// <summary>
/// Raised when a purchase is attempted with a balance lower than the coffee price.
/// <para>
/// Business rule: the user cannot purchase coffee without enough inserted funds.
/// </para>
/// </summary>
public sealed class InsufficientBalanceException : Exception
{
    /// <summary>
    /// Current machine balance in cents at the time of the failed purchase.
    /// </summary>
    public int BalanceInCents { get; }

    /// <summary>
    /// Required coffee price in cents.
    /// </summary>
    public int RequiredAmountInCents { get; }

    /// <summary>
    /// Creates an exception describing a shortfall between balance and price.
    /// </summary>
    public InsufficientBalanceException(int balanceInCents, int requiredAmountInCents)
        : base($"Insufficient balance. Available: {balanceInCents} cents; required: {requiredAmountInCents} cents.")
    {
        BalanceInCents = balanceInCents;
        RequiredAmountInCents = requiredAmountInCents;
    }
}
