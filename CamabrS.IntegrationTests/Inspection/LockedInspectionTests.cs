using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Reopening;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;
using CamabrS.IntegrationTests.Inspection.Fixtures;
using static FastExpressionCompiler.ExpressionCompiler;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class LockedInspectionTests(AppFixture fixture) : ApiWithLockedInspection(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_locked_Inspection_should_fail()
    {
        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForAssignment(Inspection.Id));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_a_locked_Inspection_should_fail()
    {
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //lock

    [Fact]
    public async Task Lock_a_locked_Inspection_should_fail()
    {
        var result = await Host.LockInspection(Inspection.Id, CombGuidIdGeneration.NewGuid(), Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //unlock
    
    [Fact]
    public async Task Unlock_a_locked_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        var result = await Host.UnlockInspection(
            Inspection.Id, Inspection.Version, DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);
        
        result.ApiResponseShouldHave(
            InspectionStreamVersions.Unlocked, 
            [AssignEndpoints.AssignEnpoint, UnassignEndpoints.UnassignEnpoint, LockEndpoints.LockEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Assigned,
                LockHoldingSpecialist = null,
                Version = InspectionStreamVersions.Unlocked
            });               
    }

    [Fact]
    public async Task Unlock_a_locked_Inspection_by_other_user_should_fail()
    {
        var result = await Host.UnlockInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(UnlockEndpoints.GetInvalidUnlockingAttemptErrorMessage());
    }

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_to_a_locked_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        var formId = CombGuidIdGeneration.NewGuid();

        var result = await Host.SubmitInspection(
            Inspection.Id, Inspection.Version, formId, DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Submitted,
            [SubmitEndpoints.SubmitEnpoint, SignEndpoints.SignEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Submitted,
                FormId = formId,
                Version = InspectionStreamVersions.Submitted
            });
    }

    [Fact]
    public async Task Submitting_Inspection_result_to_a_locked_Inspection_by_other_user_should_fail()
    {
        var result = await Host.SubmitInspection(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(SubmitEndpoints.GetInvalidSubmittingAttemptErrorMessage());
    }

    //sign

    [Fact]
    public async Task Signing_a_locked_Inspection_by_any_user_should_fail()
    {
        var result = await Host.SignInspection(Inspection.Id, Inspection.Version, internet.Url(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, Inspection.Id));
    }

    //close

    [Fact]
    public async Task Closeing_a_Locked_Inspection_should_fail()
    {
        var result = await Host.CloseInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }

    //review

    [Fact]
    public async Task Reviewing_a_locked_Inspection_should_fail()
    {
        var result = await Host.ReviewInspection(Inspection.Id, Inspection.Version, ReviewVerdict.Approved, loremIpsum.Paragraph(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, Inspection.Id));
    }

    //reopen

    [Fact]
    public async Task Reopening_a_locked_Inspection_should_fail()
    {
        var result = await Host.ReopenInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }

    //complete

    [Fact]
    public async Task Completing_a_locked_Inspection_should_fail()
    {
        var result = await Host.CompleteInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }
}