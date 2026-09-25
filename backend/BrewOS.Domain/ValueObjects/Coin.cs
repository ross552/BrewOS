namespace BrewOS.Domain.ValueObjects;

/// <summary>
/// Strongly typed coin denomination accepted by the coffee machine.
/// <para>
/// Business rule: only 5, 10, 20, 50, 100, and 200 cent coins are valid.
/// Coins of 1 or 2 cents are explicitly rejected.
/// </para>
/// </summary>
public sealed class Coin : IEquatable<Coin>
{
    /// <summary>
    /// Supported denominations in descending order (used by change calculation).
    /// </summary>
    public static IReadOnlyList<int> SupportedDenominations { get; } = new[]
    {
        200,
        100,
        50,
        20,
        10,
        5
    };

    /// <summary>5-cent coin.</summary>
    public static Coin Five { get; } = new(5);

    /// <summary>10-cent coin.</summary>
    public static Coin Ten { get; } = new(10);

    /// <summary>20-cent coin.</summary>
    public static Coin Twenty { get; } = new(20);

    /// <summary>50-cent coin.</summary>
    public static Coin Fifty { get; } = new(50);

    /// <summary>100-cent ($1) coin.</summary>
    public static Coin OneHundred { get; } = new(100);

    /// <summary>200-cent ($2) coin.</summary>
    public static Coin TwoHundred { get; } = new(200);

    /// <summary>
    /// Face value of the coin in cents.
    /// </summary>
    public int ValueInCents { get; }

    private Coin(int valueInCents)
    {
        ValueInCents = valueInCents;
    }

    /// <summary>
    /// Creates a coin for a supported denomination.
    /// </summary>
    /// <param name="valueInCents">Denomination in cents.</param>
    /// <returns>A strongly typed <see cref="Coin"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the denomination is not one of 5, 10, 20, 50, 100, or 200.
    /// Includes rejection of invalid 1- and 2-cent coins.
    /// </exception>
    public static Coin Create(int valueInCents)
    {
        if (!IsSupported(valueInCents))
        {
            throw new ArgumentOutOfRangeException(
                nameof(valueInCents),
                valueInCents,
                "Unsupported coin. Accepted denominations: 5, 10, 20, 50, 100, 200 cents.");
        }

        return valueInCents switch
        {
            5 => Five,
            10 => Ten,
            20 => Twenty,
            50 => Fifty,
            100 => OneHundred,
            200 => TwoHundred,
            _ => new Coin(valueInCents)
        };
    }

    /// <summary>
    /// Attempts to create a coin without throwing.
    /// </summary>
    /// <param name="valueInCents">Denomination in cents.</param>
    /// <param name="coin">The created coin when successful; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> when the denomination is supported.</returns>
    public static bool TryCreate(int valueInCents, out Coin? coin)
    {
        if (!IsSupported(valueInCents))
        {
            coin = null;
            return false;
        }

        coin = Create(valueInCents);
        return true;
    }

    /// <summary>
    /// Returns whether the denomination is accepted by the machine.
    /// </summary>
    public static bool IsSupported(int valueInCents) =>
        SupportedDenominations.Contains(valueInCents);

    public bool Equals(Coin? other) =>
        other is not null && ValueInCents == other.ValueInCents;

    public override bool Equals(object? obj) =>
        obj is Coin other && Equals(other);

    public override int GetHashCode() => ValueInCents.GetHashCode();

    public override string ToString() => $"{ValueInCents}c";

    public static bool operator ==(Coin? left, Coin? right) =>
        Equals(left, right);

    public static bool operator !=(Coin? left, Coin? right) =>
        !Equals(left, right);
}
