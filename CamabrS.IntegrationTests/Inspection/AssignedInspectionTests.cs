using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class AssignedInspectionTests(AppFixture fixture) : ApiWithAssignedInspection(fixture), IAsyncLifetime
{
    //assign

    [Fact]
    public async Task Assigning_another_Specialist_to_an_assigned_Inspection_should_succeed()
    {
        await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.AnotherAssignedSpecialist, DateTimeOffset.Now);
        var result = await Host.GetInspectionDetails(Inspection.Id);

        var inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.AssignedSpecialists.Length.ShouldBe(2);
        inspection.AssignedSpecialists.Contains(BaselineData.AnotherAssignedSpecialist).ShouldBeTrue();       
    }

    [Fact]
    public async Task Assigning_a_not_existing_Specialist_to_an_assigned_Inspection_should_fail()
    {
        var notExistingSpecialistId = CombGuidIdGeneration.NewGuid();

        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, notExistingSpecialistId, DateTimeOffset.Now);        

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(AssignEndpoints.GetSpecialistNotExistsErrorDetail(notExistingSpecialistId));
    }

    [Fact]
    public async Task Assigning_the_same_Specialist_to_an_assigned_Inspection_should_fail()
    {
        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(AssignEndpoints.GetSpecialistHasAlreadyBeenAddedErrorDetail(BaselineData.LockHoldingSpecialist));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_an_assigned_Inspection_should_succeed()
    {
        await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var result = await Host.GetInspectionDetails(Inspection.Id);
        var inspection = await result.ReadAsJsonAsync<InspectionDetails>();
        
        inspection.ShouldNotBeNull();
        inspection.AssignedSpecialists.Length.ShouldBe(0);
        inspection.Status.ShouldBe(InspectionStatus.Opened);
    }

    [Fact]
    public async Task Unassigning_a_not_assigned_Specialist_from_an_assigned_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //lock

    [Fact]
    public async Task Locking_Inspection_should_succeed()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task Not_assigned_specialist_tries_to_lock_Inspection_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //unlock

    [Fact]
    public async Task Unlocking_Inspection_by_assigned_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_by_assigned_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //sign

    [Fact]
    public async Task Signing_Inspection_by_assigend_Specialist_should_fail()
    {
        false.ShouldBeTrue();

        await Task.CompletedTask;
    }

    //close

    [Fact]
    public async Task Closeing_Inspection_by_assigned_Specialist_should_fail()
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