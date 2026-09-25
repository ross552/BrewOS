using BrewOS.Application.UseCases.PurchaseCoffee;
using BrewOS.Domain.Entities;
using BrewOS.Domain.ValueObjects;

namespace BrewOS.Tests.Application;

public class PurchaseCoffeeUseCaseTests
{
    [Fact]
    public void Execute_WithExistingCoffeeId_ReturnsPurchasedCoffeeDto()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.TwoHundred);
        machine.InsertCoin(Coin.TwoHundred); // 400
        var useCase = new PurchaseCoffeeUseCase(machine);

        // Act
        var result = useCase.Execute(Coffee.LatteId);

        // Assert
        Assert.Equal(Coffee.LatteId, result.Coffee.Id);
        Assert.Equal(Coffee.Latte.Name, result.Coffee.Name);
        Assert.Equal(Coffee.Latte.PriceInCents, result.Coffee.PriceInCents);
        Assert.Equal(100, result.ChangeTotalInCents);
        Assert.Equal(new[] { 100 }, result.ChangeInCents);
        Assert.Equal(0, machine.GetBalance());
    }

    [Fact]
    public void Execute_WithUnknownCoffeeId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var machine = new CoffeeMachine();
        machine.InsertCoin(Coin.TwoHundred);
        var useCase = new PurchaseCoffeeUseCase(machine);
        var unknownId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        // Act
        var exception = Assert.Throws<KeyNotFoundException>(() => useCase.Execute(unknownId));

        // Assert
        Assert.Contains(unknownId.ToString(), exception.Message);
        Assert.Equal(200, machine.GetBalance());
    }
}
