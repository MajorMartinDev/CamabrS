using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class CompletedInspectionTests(AppFixture fixture) : ApiWithCompletedInspection(fixture)
{
    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unassign

    [Fact]
    public async Task Unassigning_an_assigned_Specialist_from_a_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //lock

    [Fact]
    public async Task Locking_a_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }


    //unlock

    [Fact]
    public async Task Unlocking_Inspection_by_lock_holding_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task Unlocking_Inspection_by_another_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }


    //submit

    [Fact]
    public async Task Submitting_Inspection_result_by_lock_holding_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //sign

    [Fact]
    public async Task Signing_Inspection_by_lock_holding_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //close

    [Fact]
    public async Task Closeing_Inspection_by_lock_holding_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //review

    [Fact]
    public async Task Reviewing_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //reopen

    [Fact]
    public async Task Reopening_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //complete

    [Fact]
    public async Task Completing_completed_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }
}