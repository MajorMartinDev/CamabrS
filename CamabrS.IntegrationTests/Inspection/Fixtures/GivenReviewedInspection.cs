using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;

public class GivenReviewedInspection(AppFixture fixture) : IntegrationContext(fixture), IAsyncLifetime
{
    public override async Task InitializeAsync() =>
        Inspection = await Host.ReviewedInspection();

    public InspectionDetails Inspection { get; protected set; } = default!;
}
