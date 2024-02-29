using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class SubmittedInspectionResultTests(AppFixture fixture) : ApiWithSubmittedInspectionResult(fixture)
{
    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_submitted_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unassign

    [Fact]
    public async Task Unassigning_an_assigned_Specialist_from_a_submitted_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //lock

    [Fact]
    public async Task Locking_a_submitted_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unlock

    [Fact]
    public async Task Unlocking_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }    

    //submit

    //TODO add missing business logic to submitting multiple times
    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_another_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //sign
    [Fact]
    public async Task Signing_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task Signing_Inspection_by_another_assigned_Specialist_should_fail()
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