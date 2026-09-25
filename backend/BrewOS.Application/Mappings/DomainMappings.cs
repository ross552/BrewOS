using BrewOS.Application.DTOs;
using BrewOS.Domain.Entities;
using BrewOS.Domain.Results;

namespace BrewOS.Application.Mappings;

/// <summary>
/// Maps Domain models to Application DTOs.
/// Mapping only — no business rules are applied here.
/// </summary>
internal static class DomainMappings
{
    /// <summary>
    /// Maps a Domain <see cref="Coffee"/> to <see cref="CoffeeDto"/>.
    /// </summary>
    public static CoffeeDto ToDto(this Coffee coffee)
    {
        ArgumentNullException.ThrowIfNull(coffee);

        return new CoffeeDto
        {
            Id = coffee.Id,
            Name = coffee.Name,
            PriceInCents = coffee.PriceInCents
        };
    }

    /// <summary>
    /// Maps a Domain <see cref="PurchaseResult"/> to <see cref="PurchaseResultDto"/>.
    /// </summary>
    public static PurchaseResultDto ToDto(this PurchaseResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return new PurchaseResultDto
        {
            Coffee = result.Coffee.ToDto(),
            ChangeInCents = result.Change.Select(c => c.ValueInCents).ToArray(),
            ChangeTotalInCents = result.ChangeTotalInCents
        };
    }
}
