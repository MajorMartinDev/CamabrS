using CamabrS.API.Core.Http;

namespace CamabrS.IntegrationTests;
public static class ApiResponseAssertion
{
    public static void ApiResponseShouldHave(
        this IScenarioResult? result,
        int version,
        List<string> nextSteps)
    {
        result.ShouldNotBeNull();

        var apiResponse = result.ReadAsJson<ApiResponse>();
        apiResponse.ShouldNotBeNull();
        apiResponse.Version.ShouldBe(version);
        apiResponse.NextSteps.ShouldBe(nextSteps);
    }
}