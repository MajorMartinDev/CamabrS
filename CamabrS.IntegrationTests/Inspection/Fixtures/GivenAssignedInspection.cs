using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class GivenAssignedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
       Inspection = await Host.AssignedInspection();

    public InspectionDetails Inspection { get; protected set; } = default!;
}
