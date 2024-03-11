using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Reopening;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class ReviewedInspectionTests(AppFixture fixture) : ApiWithReviewedInspection(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForAssignment(Inspection.Id));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }    

    //lock

    [Fact]
    public async Task Locking_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.LockInspection(Inspection.Id, CombGuidIdGeneration.NewGuid(), Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //unlock

    [Fact]
    public async Task Unlocking_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.UnlockInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, Inspection.Id));
    }   

    //submit

    [Fact]
    public async Task Submitting_Inspection_result_to_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.SubmitInspection(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForSubmitting(Inspection.Id));
    }

    //sign

    [Fact]
    public async Task Signing_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.SignInspection(Inspection.Id, Inspection.Version, internet.Url(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, Inspection.Id));
    }

    //close

    [Fact]
    public async Task Closeing_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.CloseInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }
   
    //review

    [Fact]
    public async Task Reviewing_a_reviewed_Inspection_should_fail()
    {
        var result = await Host.ReviewInspection(Inspection.Id, Inspection.Version, ReviewVerdict.Approved, loremIpsum.Paragraph(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, Inspection.Id));
    }

    //reopen

    [Fact]
    public async Task Reopening_a_reviewed_and_approved_Inspection_should_fail()
    {
        var result = await Host.ReopenInspection(
            Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(ReopenEndpoints.ApprovedInspectionCanNotBeReopenedErrorMessage);
    }

    [Fact]
    public async Task Reopening_a_reviewed_and_disapproved_Inspection_should_succeed()
    {
        Inspection = await Host.ReviewedInspection(ReviewVerdict.Disapproved);        

        var result = await Host.ReopenInspection(
            Inspection.Id, Inspection.Version, DateTimeOffset.Now);        

        var updated = await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Opened,
                AssignedSpecialists = [],
                LockHoldingSpecialist = null,
                Version = InspectionStreamVersions.Reopened
            });

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Reopened,
            NextInspectionSteps.GetNextSteps(updated.Status, updated.Verdict));
    }

    //complete

    [Fact]
    public async Task Completeing_a_reviewed_Inspection_should_succeed()
    {
        var result = await Host.CompleteInspection(
            Inspection.Id, Inspection.Version, DateTimeOffset.Now);        

        var updated = await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Completed,
                Version = InspectionStreamVersions.Completed
            });

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Completed,
            NextInspectionSteps.GetNextSteps(updated.Status, updated.Verdict));
    }
}