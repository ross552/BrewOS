namespace BrewOS.Domain.Entities;

/// <summary>
/// Represents a purchasable coffee product offered by the virtual machine.
/// Prices are expressed in cents to avoid floating-point currency errors.
/// </summary>
public sealed class Coffee
{
    /// <summary>
    /// Well-known identifier for Cappuccino (350 cents).
    /// </summary>
    public static readonly Guid CappuccinoId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    /// <summary>
    /// Well-known identifier for Latte (300 cents).
    /// </summary>
    public static readonly Guid LatteId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    /// <summary>
    /// Well-known identifier for Decaf (400 cents).
    /// </summary>
    public static readonly Guid DecafId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    /// <summary>
    /// Cappuccino — 350 cents ($3.50).
    /// </summary>
    public static Coffee Cappuccino { get; } = new(CappuccinoId, "Cappuccino", 350);

    /// <summary>
    /// Latte — 300 cents ($3.00).
    /// </summary>
    public static Coffee Latte { get; } = new(LatteId, "Latte", 300);

    /// <summary>
    /// Decaf — 400 cents ($4.00).
    /// </summary>
    public static Coffee Decaf { get; } = new(DecafId, "Decaf", 400);

    /// <summary>
    /// Catalog of all predefined coffee types supported by BrewOS.
    /// </summary>
    public static IReadOnlyList<Coffee> Catalog { get; } = new[]
    {
        Cappuccino,
        Latte,
        Decaf
    };

    /// <summary>
    /// Unique identifier of the coffee product.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Display name of the coffee product.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Unit price in cents. All monetary values in the domain use integer cents.
    /// </summary>
    public int PriceInCents { get; }

    /// <summary>
    /// Creates a coffee product. Prefer the predefined static instances for the vending catalog.
    /// </summary>
    /// <param name="id">Unique product identifier.</param>
    /// <param name="name">Non-empty product name.</param>
    /// <param name="priceInCents">Price in cents; must be positive.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is blank.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="priceInCents"/> is not positive.</exception>
    public Coffee(Guid id, string name, int priceInCents)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Coffee name is required.", nameof(name));
        }

        if (priceInCents <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(priceInCents), priceInCents, "Coffee price must be greater than zero.");
        }

        Id = id;
        Name = name.Trim();
        PriceInCents = priceInCents;
    }
}
