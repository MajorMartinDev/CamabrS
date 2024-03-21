using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class GivenSignedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
       Inspection = await Host.SignedInspection(TestUser.SuperuserWithLockHoldingId);

    public InspectionDetails Inspection { get; protected set; } = default!;
}
