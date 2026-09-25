using BrewOS.Application.DTOs;
using BrewOS.Application.Interfaces;
using BrewOS.Application.UseCases.GetCoffeeMenu;
using BrewOS.Application.UseCases.InsertCoin;
using BrewOS.Application.UseCases.PurchaseCoffee;
using BrewOS.Domain.Entities;

namespace BrewOS.Application.Services;

/// <summary>
/// Application service that coordinates coffee-machine use cases.
/// Acts as a facade for outer layers while keeping Domain rules inside Domain objects.
/// </summary>
public sealed class CoffeeMachineService : ICoffeeMachineService
{
    private readonly CoffeeMachine _coffeeMachine;
    private readonly InsertCoinUseCase _insertCoinUseCase;
    private readonly GetCoffeeMenuUseCase _getCoffeeMenuUseCase;
    private readonly PurchaseCoffeeUseCase _purchaseCoffeeUseCase;

    /// <summary>
    /// Creates a coffee machine application service with a dedicated Domain aggregate instance.
    /// </summary>
    public CoffeeMachineService()
        : this(new CoffeeMachine())
    {
    }

    /// <summary>
    /// Creates a coffee machine application service using the supplied Domain aggregate.
    /// Useful for tests that need a shared or preconfigured machine.
    /// </summary>
    /// <param name="coffeeMachine">Domain coffee machine aggregate.</param>
    public CoffeeMachineService(CoffeeMachine coffeeMachine)
    {
        _coffeeMachine = coffeeMachine ?? throw new ArgumentNullException(nameof(coffeeMachine));
        _insertCoinUseCase = new InsertCoinUseCase(_coffeeMachine);
        _getCoffeeMenuUseCase = new GetCoffeeMenuUseCase();
        _purchaseCoffeeUseCase = new PurchaseCoffeeUseCase(_coffeeMachine);
    }

    /// <inheritdoc />
    public void InsertCoin(int valueInCents) =>
        _insertCoinUseCase.Execute(valueInCents);

    /// <inheritdoc />
    public int GetBalance() =>
        _coffeeMachine.GetBalance();

    /// <inheritdoc />
    public IReadOnlyList<CoffeeDto> GetAvailableCoffees() =>
        _getCoffeeMenuUseCase.Execute();

    /// <inheritdoc />
    public PurchaseResultDto PurchaseCoffee(Guid coffeeId) =>
        _purchaseCoffeeUseCase.Execute(coffeeId);
}
