using CamabrS.API.Inspection;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;
using CamabrS.IntegrationTests.Inspection.Fixtures;
using System;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class SubmittedInspectionTests(AppFixture fixture) : ApiWithSubmittedInspectionResult(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_submitted_Inspection_should_fail()
    {
        var result = await Host.AssignSpecialist(Inspection.Id, Inspection.Version, BaselineData.LockHoldingSpecialist, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForAssignment(Inspection.Id));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_a_submitted_Inspection_should_fail()
    {
        var result = await Host.UnassignSpecialist(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //lock

    [Fact]
    public async Task Locking_a_submitted_Inspection_should_fail()
    {
        var result = await Host.LockInspection(Inspection.Id, CombGuidIdGeneration.NewGuid(), Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //unlock

    [Fact]
    public async Task Unlocking_a_submitted_Inspection_should_fail()
    {
        var result = await Host.UnlockInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, Inspection.Id));
    }    

    //submit
        
    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        var newFormId = CombGuidIdGeneration.NewGuid();

        await Host.SubmitInspection(Inspection.Id, Inspection.Version, newFormId, DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);

        var result = await Host.GetInspectionDetails(Inspection.Id);

        var inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Submitted);
        inspection.FormId.ShouldBe(newFormId);       
    }

    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_another_user_should_fail()
    {
        var result = await Host.SubmitInspection(Inspection.Id, Inspection.Version, CombGuidIdGeneration.NewGuid(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(SubmitEndpoints.GetInvalidSubmittingAttemptErrorMessage());
    }

    //sign
    [Fact]
    public async Task Signing_a_submitted_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        var url = internet.Url();

        await Host.SignInspection(Inspection.Id, Inspection.Version, url, DateTimeOffset.Now, BaselineData.LockHoldingSpecialist);
        var result = await Host.GetInspectionDetails(Inspection.Id);

        var inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Signed);
        inspection.SignatureLink.ShouldBe(url);        
    }

    [Fact]
    public async Task Signing_a_submitted_Inspection_by_another_user_should_fail()
    {
        var result = await Host.SignInspection(Inspection.Id, Inspection.Version, internet.Url(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(SignEndpoints.GetInvalidSigningAttemptErrorMessage());
    }

    //close

    [Fact]
    public async Task Closeing_a_submitted_Inspection_should_fail()
    {
        var result = await Host.CloseInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }

    //review

    [Fact]
    public async Task Reviewing_a_submitted_Inspection_should_fail()
    {
        var result = await Host.ReviewInspection(Inspection.Id, Inspection.Version, true, loremIpsum.Paragraph(), DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, Inspection.Id));
    }

    //reopen

    [Fact]
    public async Task Reopening_a_submitted_Inspection_should_fail()
    {
        var result = await Host.ReopenInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }

    //complete

    [Fact]
    public async Task Completing_a_submitted_Inspection_should_fail()
    {
        var result = await Host.CompleteInspection(Inspection.Id, Inspection.Version, DateTimeOffset.Now);

        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(500);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }
}