﻿using CamabrS.API.Core.Http;

namespace CamabrS.IntegrationTests;
public static class ApiResponseAssertion
{
    public static void ApiResponseShouldHave(
        this IScenarioResult? result,
        int version,
        List<string> availableActions)
    {
        result.ShouldNotBeNull();

        var apiResponse = result.ReadAsJson<ApiResponse>();
        apiResponse.ShouldNotBeNull();
        apiResponse.Version.ShouldBe(version);
        apiResponse.AvailableActions.ShouldBe(availableActions);
    }
}