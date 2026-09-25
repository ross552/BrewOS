namespace BrewOS.Application.DTOs;

/// <summary>
/// Data transfer object representing the current status of the coffee machine.
/// </summary>
public sealed class MachineStatusDto
{
    /// <summary>
    /// Current inserted balance in cents.
    /// </summary>
    public int BalanceInCents { get; init; }
}
