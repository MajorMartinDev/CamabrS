using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class CompleteInspectionTests(AppFixture fixture) : ApiWithReviewedInspection(fixture)
{
}
