using BrewOS.Api.Contracts.Requests;
using BrewOS.Application.DTOs;
using BrewOS.Application.UseCases.GetCoffeeMenu;
using BrewOS.Application.UseCases.GetMachineStatus;
using BrewOS.Application.UseCases.InsertCoin;
using BrewOS.Application.UseCases.PurchaseCoffee;
using Microsoft.AspNetCore.Mvc;

namespace BrewOS.Api.Controllers;

/// <summary>
/// HTTP endpoints for the virtual coffee machine.
/// Controllers remain thin: receive request, invoke use case, return response.
/// </summary>
[ApiController]
[Route("api/machine")]
[Produces("application/json")]
public sealed class CoffeeMachineController : ControllerBase
{
    private readonly InsertCoinUseCase _insertCoinUseCase;
    private readonly GetCoffeeMenuUseCase _getCoffeeMenuUseCase;
    private readonly GetMachineStatusUseCase _getMachineStatusUseCase;
    private readonly PurchaseCoffeeUseCase _purchaseCoffeeUseCase;

    /// <summary>
    /// Creates the coffee machine controller.
    /// </summary>
    public CoffeeMachineController(
        InsertCoinUseCase insertCoinUseCase,
        GetCoffeeMenuUseCase getCoffeeMenuUseCase,
        GetMachineStatusUseCase getMachineStatusUseCase,
        PurchaseCoffeeUseCase purchaseCoffeeUseCase)
    {
        _insertCoinUseCase = insertCoinUseCase;
        _getCoffeeMenuUseCase = getCoffeeMenuUseCase;
        _getMachineStatusUseCase = getMachineStatusUseCase;
        _purchaseCoffeeUseCase = purchaseCoffeeUseCase;
    }

    /// <summary>
    /// Returns the available coffee menu.
    /// </summary>
    /// <response code="200">Coffee catalog retrieved successfully.</response>
    [HttpGet("/api/coffees")]
    [ProducesResponseType(typeof(IReadOnlyList<CoffeeDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CoffeeDto>> GetCoffees()
    {
        var coffees = _getCoffeeMenuUseCase.Execute();
        return Ok(coffees);
    }

    /// <summary>
    /// Inserts a coin into the machine and returns the updated balance.
    /// </summary>
    /// <param name="request">Coin denomination to insert.</param>
    /// <response code="200">Coin accepted; current balance returned.</response>
    /// <response code="400">Invalid or unsupported coin denomination.</response>
    [HttpPost("coins")]
    [ProducesResponseType(typeof(MachineStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<MachineStatusDto> InsertCoin([FromBody] InsertCoinRequest request)
    {
        _insertCoinUseCase.Execute(request.ValueInCents);
        var status = _getMachineStatusUseCase.Execute();
        return Ok(status);
    }

    /// <summary>
    /// Returns the current machine balance.
    /// </summary>
    /// <response code="200">Current balance in cents.</response>
    [HttpGet("status")]
    [ProducesResponseType(typeof(MachineStatusDto), StatusCodes.Status200OK)]
    public ActionResult<MachineStatusDto> GetStatus()
    {
        var status = _getMachineStatusUseCase.Execute();
        return Ok(status);
    }

    /// <summary>
    /// Purchases the selected coffee when the balance is sufficient.
    /// </summary>
    /// <param name="request">Identifier of the coffee to purchase.</param>
    /// <response code="200">Purchase completed; coffee and change returned.</response>
    /// <response code="400">Insufficient balance or invalid request.</response>
    /// <response code="404">Coffee identifier was not found in the catalog.</response>
    [HttpPost("purchase")]
    [ProducesResponseType(typeof(PurchaseResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PurchaseResultDto> Purchase([FromBody] PurchaseCoffeeRequest request)
    {
        var result = _purchaseCoffeeUseCase.Execute(request.CoffeeId);
        return Ok(result);
    }
}
