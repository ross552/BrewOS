using BrewOS.Api.Contracts;

namespace BrewOS.Tests.Api;

public class ApiErrorResponseTests
{
    [Fact]
    public void Constructor_SetsCodeMessageAndUtcTimestamp()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var response = new ApiErrorResponse
        {
            Code = "INTERNAL_ERROR",
            Message = "Unexpected server error occurred."
        };

        var after = DateTime.UtcNow;

        // Assert
        Assert.Equal("INTERNAL_ERROR", response.Code);
        Assert.Equal("Unexpected server error occurred.", response.Message);
        Assert.True(response.Timestamp >= before);
        Assert.True(response.Timestamp <= after);
    }

    [Fact]
    public void Constructor_AllowsExplicitTimestampOverride()
    {
        // Arrange
        var timestamp = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var response = new ApiErrorResponse
        {
            Code = "NOT_FOUND",
            Message = "Missing resource",
            Timestamp = timestamp
        };

        // Assert
        Assert.Equal("NOT_FOUND", response.Code);
        Assert.Equal("Missing resource", response.Message);
        Assert.Equal(timestamp, response.Timestamp);
    }
}
