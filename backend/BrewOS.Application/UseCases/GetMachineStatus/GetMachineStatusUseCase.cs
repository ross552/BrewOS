using BrewOS.Application.DTOs;
using BrewOS.Domain.Entities;

namespace BrewOS.Application.UseCases.GetMachineStatus;

/// <summary>
/// Use case: retrieve the current coffee machine balance.
/// Keeps Domain access inside the Application layer.
/// </summary>
public sealed class GetMachineStatusUseCase
{
    private readonly CoffeeMachine _coffeeMachine;

    /// <summary>
    /// Creates the use case bound to a specific machine instance.
    /// </summary>
    /// <param name="coffeeMachine">Domain coffee machine aggregate.</param>
    public GetMachineStatusUseCase(CoffeeMachine coffeeMachine)
    {
        _coffeeMachine = coffeeMachine ?? throw new ArgumentNullException(nameof(coffeeMachine));
    }

    /// <summary>
    /// Returns the current machine balance as a DTO.
    /// </summary>
    public MachineStatusDto Execute()
    {
        return new MachineStatusDto
        {
            BalanceInCents = _coffeeMachine.GetBalance()
        };
    }
}
