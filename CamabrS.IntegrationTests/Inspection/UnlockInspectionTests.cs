using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed  class UnlockInspectionTests(AppFixture fixture) : ApiWithLockedInspection(fixture)
{
}
