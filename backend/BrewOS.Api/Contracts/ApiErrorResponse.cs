namespace BrewOS.Api.Contracts;

/// <summary>
/// Consistent API error response contract returned to clients on failures.
/// </summary>
public sealed class ApiErrorResponse
{
    /// <summary>
    /// Machine-readable error code (for example, InsufficientBalance).
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Human-readable error description.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// UTC timestamp when the error response was created.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
