using Api.TestServices;

namespace IntegrationTest;

public class NoOpService : ITestService
{
    public string GetTest()
    {
        return string.Empty;
    }
}