using BrewOS.Application.DTOs;
using BrewOS.Application.Mappings;
using BrewOS.Domain.Entities;

namespace BrewOS.Application.UseCases.GetCoffeeMenu;

/// <summary>
/// Use case: retrieve the available coffee menu.
/// Reads the Domain catalog and maps it to DTOs.
/// </summary>
public sealed class GetCoffeeMenuUseCase
{
    /// <summary>
    /// Returns all predefined coffees as DTOs.
    /// </summary>
    public IReadOnlyList<CoffeeDto> Execute()
    {
        return Coffee.Catalog
            .Select(coffee => coffee.ToDto())
            .ToArray();
    }
}
