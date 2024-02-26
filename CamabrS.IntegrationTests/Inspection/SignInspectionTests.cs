using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class SignInspectionTests(AppFixture fixture) : ApiWithSubmittedInspection(fixture)
{
}
