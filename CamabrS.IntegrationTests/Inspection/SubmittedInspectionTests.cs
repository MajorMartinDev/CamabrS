﻿using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;

public sealed class SubmittedInspectionTests(AppFixture fixture) : GivenInspectionWithSubmittedInspectionResult(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.AssignSpecialist(
            Inspection.Id, 
            Inspection.Version, 
            BaselineData.LockHoldingSpecialistId, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForAssignment(Inspection.Id));
    }

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.UnassignSpecialist(
            Inspection.Id, 
            Inspection.Version, 
            CombGuidIdGeneration.NewGuid(), 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //lock

    [Fact]
    public async Task Locking_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.LockInspection(
            Inspection.Id, 
            CombGuidIdGeneration.NewGuid(), 
            Inspection.Version, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, Inspection.Id));
    }

    //unlock

    [Fact]
    public async Task Unlocking_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.UnlockInspection(
            Inspection.Id, 
            Inspection.Version, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, Inspection.Id));
    }    

    //submit
        
    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        //when
        var newFormId = CombGuidIdGeneration.NewGuid();
        var result = await Host.SubmitInspection(
            Inspection.Id, 
            Inspection.Version, 
            newFormId, 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);        

        //then
        var updated = await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Submitted,
                FormId = newFormId,
                Version = InspectionStreamVersions.Resubmitted
            }, TestUser.SuperuserWithLockHoldingId);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Resubmitted,
            NextInspectionSteps.GetNextSteps(updated.Status, updated.Verdict));
    }

    [Fact]
    public async Task Submitting_Inspection_result_to_a_submitted_Inspection_by_another_user_should_fail()
    {
        //when
        var result = await Host.SubmitInspection(
            Inspection.Id, 
            Inspection.Version, 
            CombGuidIdGeneration.NewGuid(), 
            DateTimeOffset.Now,
            TestUser.Superuser);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(SubmitEndpoints.GetInvalidSubmittingAttemptErrorMessage());
    }

    //sign
    [Fact]
    public async Task Signing_a_submitted_Inspection_by_lock_holding_Specialist_should_succeed()
    {
        //when
        var url = internet.Url();
        var result = await Host.SignInspection(
            Inspection.Id, 
            Inspection.Version, 
            url, 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);        

        //then
        var updated = await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Signed,
                SignatureLink = url,
                Version = InspectionStreamVersions.Signed
            }, TestUser.SuperuserWithLockHoldingId);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Signed,
            NextInspectionSteps.GetNextSteps(updated.Status, updated.Verdict));
    }

    [Fact]
    public async Task Signing_a_submitted_Inspection_by_another_user_should_fail()
    {
        //when
        var result = await Host.SignInspection(
            Inspection.Id, 
            Inspection.Version, 
            internet.Url(), 
            DateTimeOffset.Now,
            TestUser.Superuser);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(SignEndpoints.GetInvalidSigningAttemptErrorMessage());
    }

    //close

    [Fact]
    public async Task Closeing_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.CloseInspection(
            Inspection.Id, 
            Inspection.Version, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, Inspection.Id));
    }

    //review

    [Fact]
    public async Task Reviewing_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.ReviewInspection(
            Inspection.Id, 
            Inspection.Version, 
            ReviewVerdict.Approved, 
            loremIpsum.Paragraph(), 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, Inspection.Id));
    }

    //reopen

    [Fact]
    public async Task Reopening_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.ReopenInspection(
            Inspection.Id, 
            Inspection.Version, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }

    //complete

    [Fact]
    public async Task Completing_a_submitted_Inspection_should_fail()
    {
        //when
        var result = await Host.CompleteInspection(
            Inspection.Id, 
            Inspection.Version, 
            DateTimeOffset.Now,
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, Inspection.Id));
    }
}