using BrewOS.Domain.Entities;
using BrewOS.Domain.Exceptions;
using BrewOS.Domain.ValueObjects;

namespace BrewOS.Tests.Domain;

public class CoffeeMachineTests
{
    [Fact]
    public void InsertCoin_WithValidCoin_IncreasesBalance()
    {
        // Arrange
        var machine = new CoffeeMachine();

        // Act
        machine.InsertCoin(Coin.OneHundred);
        machine.InsertCoin(Coin.Fifty);

        // Assert
        Assert.Equal(150, machine.GetBalance());
    }

    [Fact]
    public void InsertCoin_WithInvalidDenomination_RejectsCoinAndLeavesBalanceUnchanged()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.Fifty);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => machine.InsertCoin(1));

        // Assert
        Assert.Equal(50, machine.GetBalance());
        Assert.Contains("Unsupported coin", exception.Message);
    }

    [Fact]
    public void PurchaseCoffee_WhenBalanceIsSufficient_ReturnsCorrectCoffeeAndResetsBalance()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.TwoHundred);
        machine.InsertCoin(Coin.TwoHundred); // 400

        // Act
        var result = machine.PurchaseCoffee(Coffee.Latte); // 300

        // Assert
        Assert.Equal(Coffee.Latte, result.Coffee);
        Assert.Equal(Coffee.LatteId, result.Coffee.Id);
        Assert.Equal(0, machine.GetBalance());
    }

    [Fact]
    public void PurchaseCoffee_WhenOverpaying_ReturnsCorrectChange()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.TwoHundred);
        machine.InsertCoin(Coin.TwoHundred); // 400

        // Act
        var result = machine.PurchaseCoffee(Coffee.Cappuccino); // 350 → change 50

        // Assert
        Assert.Equal(Coffee.Cappuccino, result.Coffee);
        Assert.Equal(50, result.ChangeTotalInCents);
        Assert.Equal(new[] { 50 }, result.Change.Select(c => c.ValueInCents));
        Assert.Equal(0, machine.GetBalance());
    }

    [Fact]
    public void PurchaseCoffee_WhenBalanceIsInsufficient_ThrowsInsufficientBalanceException()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.OneHundred); // 100, Latte costs 300

        // Act
        var exception = Assert.Throws<InsufficientBalanceException>(
            () => machine.PurchaseCoffee(Coffee.Latte));

        // Assert
        Assert.Equal(100, exception.BalanceInCents);
        Assert.Equal(Coffee.Latte.PriceInCents, exception.RequiredAmountInCents);
        Assert.Equal(100, machine.GetBalance());
    }
}
