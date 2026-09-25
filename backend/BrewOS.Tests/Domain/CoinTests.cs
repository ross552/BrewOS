using BrewOS.Domain.ValueObjects;

namespace BrewOS.Tests.Domain;

public class CoinTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(200)]
    public void Create_WithSupportedDenomination_ReturnsCoinWithMatchingValue(int valueInCents)
    {
        // Arrange & Act
        var coin = Coin.Create(valueInCents);

        // Assert
        Assert.Equal(valueInCents, coin.ValueInCents);
        Assert.True(Coin.IsSupported(valueInCents));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(999)]
    public void Create_WithUnsupportedDenomination_ThrowsArgumentOutOfRangeException(int valueInCents)
    {
        // Arrange & Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Coin.Create(valueInCents));

        // Assert
        Assert.Equal(nameof(valueInCents), exception.ParamName);
        Assert.False(Coin.IsSupported(valueInCents));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(999)]
    public void TryCreate_WithUnsupportedDenomination_ReturnsFalse(int valueInCents)
    {
        // Arrange & Act
        var created = Coin.TryCreate(valueInCents, out var coin);

        // Assert
        Assert.False(created);
        Assert.Null(coin);
    }
}
