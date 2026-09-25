using BrewOS.Application.Interfaces;
using BrewOS.Application.Services;
using BrewOS.Application.UseCases.GetCoffeeMenu;
using BrewOS.Application.UseCases.GetMachineStatus;
using BrewOS.Application.UseCases.InsertCoin;
using BrewOS.Application.UseCases.PurchaseCoffee;
using BrewOS.Domain.Entities;

namespace BrewOS.Api.Extensions;

/// <summary>
/// Extension methods for registering BrewOS application dependencies.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Domain coffee machine, Application use cases, and application services.
    /// </summary>
    public static IServiceCollection AddBrewOsApplication(this IServiceCollection services)
    {
        // The singleton CoffeeMachine keeps machine state between HTTP requests for this assessment.
        // A production system would persist machine sessions externally.
        services.AddSingleton<CoffeeMachine>();

        services.AddSingleton<InsertCoinUseCase>();
        services.AddSingleton<GetCoffeeMenuUseCase>();
        services.AddSingleton<GetMachineStatusUseCase>();
        services.AddSingleton<PurchaseCoffeeUseCase>();

        services.AddSingleton<ICoffeeMachineService, CoffeeMachineService>();

        return services;
    }
}
