using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.Completing;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Reopening;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class ClosedInspectionTests(AppFixture fixture) : ApiWithClosedInspection(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_closed_Inspection_should_fail()
    {
        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForAssignment(Inspection.Id));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_a_closed_Inspection_should_fail()
    {
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }    

    //lock

    [Fact]
    public async Task Locking_a_closed_Inspection_should_fail()
    {
        var result = await Host.LockInspection(Inspection.Id, CombGuidIdGeneration.NewGuid(), Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //unlock

    [Fact]
    public async Task Unlocking_a_closed_Inspection_should_fail()
    {
        var result = await Host.UnlockInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, Inspection.Id));
    }

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_to_a_closed_Inspection_should_fail()
    {
        var result = await Host.SubmitInspection(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForSubmitting(Inspection.Id));
    }

    //sign

    [Fact]
    public async Task Signing_a_closed_Inspection_should_fail()
    {
        var result = await Host.SignInspection(Inspection.Id, Inspection.Version, internet.Url(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, Inspection.Id));
    }

    //close

    [Fact]
    public async Task Closeing_a_closed_Inspection_should_fail()
    {
        var result = await Host.CloseInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }

    //review

    [Fact]
    public async Task Reviewing_a_closed_Inspection_with_approval_should_succeed()
    {
        var summary = loremIpsum.Paragraph();

        var result = await Host.ReviewInspection(
            Inspection.Id, Inspection.Version, ReviewVerdict.Approved, summary, DateTimeOffset.Now);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Reviewed, 
            [CompleteEndpoints.CompleteEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Reviewed,
                Verdict = ReviewVerdict.Approved,
                Summary = summary,
                AssignedSpecialists = [BaselineData.LockHoldingSpecialist],
                Version = InspectionStreamVersions.Reviewed
            });        
    }

    [Fact]
    public async Task Reviewing_a_closed_Inspection_with_disapproval_should_succeed()
    {
        var summary = loremIpsum.Paragraph();

        var result = await Host.ReviewInspection(
            Inspection.Id, Inspection.Version, ReviewVerdict.Disapproved, summary, DateTimeOffset.Now);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Reviewed, 
            [ReopenEndpoints.ReopenEnpoint]);

        await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Reviewed,
                Verdict = ReviewVerdict.Disapproved,
                Summary = summary,
                AssignedSpecialists = [BaselineData.LockHoldingSpecialist],
                Version = InspectionStreamVersions.Reviewed
            });
    }

    //reopen

    [Fact]
    public async Task Reopening_a_closed_Inspection_should_fail()
    {
        var result = await Host.ReopenInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }

    //complete

    [Fact]
    public async Task Completing_a_closed_Inspection_should_fail()
    {
        var result = await Host.CompleteInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }
}
