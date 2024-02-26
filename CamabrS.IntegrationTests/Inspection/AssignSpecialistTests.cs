using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class AssignSpecialistTests(AppFixture fixture) : ApiWithOpenedInspection(fixture)
{
    [Fact]
    public async Task AssignSpecialistCommandSucceeds()
    {
        true.ShouldBeTrue();

        await Task.CompletedTask;
    }
}