using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Submitting;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class AssignedInspectionTests(AppFixture fixture) : ApiWithAssignedInspection(fixture), IAsyncLifetime
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_another_Specialist_to_an_assigned_Inspection_should_succeed()
    {
        var result = await Host.AssignSpecialist(
            Inspection.Id, Inspection.Version, BaselineData.AnotherAssignedSpecialist, DateTimeOffset.Now);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.AssignedAnother, 
            [UnassignEndpoints.UnassignEnpoint, AssignEndpoints.AssignEnpoint, LockEndpoints.LockEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with 
            { 
                Status = InspectionStatus.Assigned,
                AssignedSpecialists = [BaselineData.LockHoldingSpecialist, BaselineData.AnotherAssignedSpecialist],
                Version = InspectionStreamVersions.AssignedAnother
            });            
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
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Unassigned, 
            [AssignEndpoints.AssignEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Opened,
                AssignedSpecialists = [],
                Version = InspectionStreamVersions.Unassigned
            });       
    }

    [Fact]
    public async Task Unassigning_a_not_assigned_Specialist_from_an_assigned_Inspection_should_fail()
    {
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, BaselineData.AnotherSpecilaist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(UnassignEndpoints.GetSpecialistWasPreviouslyAssignedErrorDetail(BaselineData.AnotherSpecilaist, Inspection.Id));
    }

    //lock

    [Fact]
    public async Task Locking_Inspection_should_succeed()
    {
        var result = await Host.LockInspection(Inspection.Id, BaselineData.LockHoldingSpecialist, 
            Inspection.Version, DateTimeOffset.Now);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Locked, 
            [UnlockEndpoints.UnlockEnpoint, SubmitEndpoints.SubmitEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Locked,
                LockHoldingSpecialist = BaselineData.LockHoldingSpecialist,
                AssignedSpecialists = [BaselineData.LockHoldingSpecialist],
                Version = InspectionStreamVersions.Locked
            });        
    }

    [Fact]
    public async Task Not_assigned_specialist_tries_to_lock_Inspection_should_fail()
    {
        var result = await Host.LockInspection(Inspection.Id, BaselineData.AnotherSpecilaist,
            Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(LockEndpoints.GetLockHoldingSpecialistWasNotAssignedErrorDetail(BaselineData.AnotherSpecilaist, Inspection.Id));
    }

    //unlock

    [Fact]
    public async Task Unlocking_Inspection_by_assigned_Specialist_should_fail()
    {
        var result = await Host.UnlockInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, Inspection.Id));
    }

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_by_assigned_Specialist_should_fail()
    {
        var result = await Host.SubmitInspection(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForSubmitting(Inspection.Id));
    }

    //sign

    [Fact]
    public async Task Signing_Inspection_by_assigend_Specialist_should_fail()
    {
        var result = await Host.SignInspection(Inspection.Id, Inspection.Version, internet.Url(), DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, Inspection.Id));
    }

    //close

    [Fact]
    public async Task Closeing_Inspection_by_assigned_Specialist_should_fail()
    {
        var result = await Host.CloseInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }

    //review

    [Fact]
    public async Task Reviewing_Inspection_should_fail()
    {
        var result = await Host.ReviewInspection(Inspection.Id, Inspection.Version, ReviewVerdict.Approved, loremIpsum.Paragraph(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, Inspection.Id));
    }

    //reopen

    [Fact]
    public async Task Reopening_Inspection_should_fail()
    {
        var result = await Host.ReopenInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }

    //complete

    [Fact]
    public async Task Completing_Inspection_should_fail()
    {
        var result = await Host.CompleteInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }
}