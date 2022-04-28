using System;
using System.Net.Http;
using System.Threading.Tasks;
using Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTest;

public class ApiIntegrationTest : IDisposable
{
    private readonly WebApplicationFactory<Program> application;
    private readonly HttpClient client;

    public ApiIntegrationTest()
    {
        application = new WebApplicationFactory<Program>().WithWebHostBuilder(
            builder =>
            {
                builder.ConfigureServices(
                    services =>
                    {
                        // Inject the override test service to pass all unit tests
                        // services.AddTransient<ITestService, NoOpService>();
                    }
                );
            }
        );

        client = application.CreateClient();
    }

    public void Dispose()
    {
        client.Dispose();
        application.Dispose();
    }

    [Fact]
    public async Task VerifyTestInjectionReturnsEmptyString()
    {
        var response = await client.GetStringAsync("/WeatherForecast/TestInjection");
        response.Should().BeEmpty();
    }

    [Fact]
    public async Task VerifyWeatherForecastEndpoint()
    {
        var response = await client.GetStringAsync("/WeatherForecast");
        response.Should().NotBeEmpty();
    }
}