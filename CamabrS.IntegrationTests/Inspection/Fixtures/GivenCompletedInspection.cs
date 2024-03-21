using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class GivenCompletedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
        Inspection = await Host.CompletedInspection(TestUser.SuperuserWithLockHoldingId);

    public InspectionDetails Inspection { get; protected set; } = default!;
}
