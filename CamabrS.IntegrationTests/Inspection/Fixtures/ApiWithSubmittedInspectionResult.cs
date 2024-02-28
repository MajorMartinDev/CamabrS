using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class ApiWithSubmittedInspectionResult(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
       Inspection = await Host.SubmittedInspection();

    public InspectionDetails Inspection { get; protected set; } = default!;
}