using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class ApiWithClosedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
       Inspection = await Host.ClosedInspection();

    public InspectionDetails Inspection { get; protected set; } = default!;
}
