using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class SubmitInspectionResultTests(AppFixture fixture) : ApiWithLockedInspection(fixture)
{
}
