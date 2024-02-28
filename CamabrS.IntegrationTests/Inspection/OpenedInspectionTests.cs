using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class OpenedInspectionTests(AppFixture fixture) : ApiWithOpenedInspection(fixture)
{
    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_an_open_Inspection_should_succeed()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task Assigning_a_non_existent_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unassign

    [Fact]
    public async Task Unassigning_any_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //lock

    [Fact]
    public async Task Locking_by_any_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unlock

    [Fact]
    public async Task Unlocking_by_any_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_by_any_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //sign

    [Fact]
    public async Task Signing_Inspection_by_any_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //close

    [Fact]
    public async Task Closeing_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //review

    [Fact]
    public async Task Reviewing_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //reopen

    [Fact]
    public async Task Reopening_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //complete

    [Fact]
    public async Task Completing_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }
}