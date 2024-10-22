using Microsoft.AspNetCore.Mvc.Testing;

namespace WorkTime.Tests;

public class IntegrationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Theory]
    [InlineData("GET", "/", "WorkTime is running")]
    [InlineData("POST", "/clock-in", "Clocked in")]
    [InlineData("POST", "/clock-out", "Clocked out")]
    public async Task Get_Index_ReturnsOk(string method, string url, string expectedResponse)
    {
        // Arrange
        HttpResponseMessage response;

        // Act
        switch (method)
        {
            case "GET":
                response = await _client.GetAsync(url);
                break;
            case "POST":
                response = await _client.PostAsync(url, null);
                break;
            default:
                Assert.Fail();
                return;
        }

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(expectedResponse, responseString);
    }
}
