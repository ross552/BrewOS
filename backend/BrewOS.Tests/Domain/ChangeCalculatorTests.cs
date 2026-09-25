using BrewOS.Domain.Services;
using BrewOS.Domain.ValueObjects;

namespace BrewOS.Tests.Domain;

public class ChangeCalculatorTests
{
    private readonly ChangeCalculator _sut = new();

    [Fact]
    public void Calculate_WhenAmountIs150_Returns100And50()
    {
        // Arrange
        const int amountInCents = 150;

        // Act
        var change = _sut.Calculate(amountInCents);

        // Assert
        Assert.Equal(new[] { 100, 50 }, change.Select(c => c.ValueInCents));
        Assert.Equal(amountInCents, change.Sum(c => c.ValueInCents));
    }

    [Fact]
    public void Calculate_WhenAmountIs275_Returns200_50_20_And5()
    {
        // Arrange
        const int amountInCents = 275;

        // Act
        var change = _sut.Calculate(amountInCents);

        // Assert
        Assert.Equal(new[] { 200, 50, 20, 5 }, change.Select(c => c.ValueInCents));
        Assert.Equal(amountInCents, change.Sum(c => c.ValueInCents));
    }

    [Fact]
    public void Calculate_WhenAmountIsZero_ReturnsEmptyCollection()
    {
        // Arrange & Act
        var change = _sut.Calculate(0);

        // Assert
        Assert.Empty(change);
    }

    [Fact]
    public void Calculate_UsesGreedyLargestDenominationsFirst()
    {
        // Arrange
        const int amountInCents = 385;

        // Act
        var change = _sut.Calculate(amountInCents);

        // Assert
        Assert.Equal(new[] { 200, 100, 50, 20, 10, 5 }, change.Select(c => c.ValueInCents));
        Assert.All(change, coin => Assert.True(Coin.IsSupported(coin.ValueInCents)));
    }
}
