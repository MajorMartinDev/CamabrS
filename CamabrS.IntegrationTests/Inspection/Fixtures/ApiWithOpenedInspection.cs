using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;
public class ApiWithOpenedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
        Inspection = await Host.OpenedInspection();

    public InspectionDetails Inspection { get; protected set; } = default!;
}