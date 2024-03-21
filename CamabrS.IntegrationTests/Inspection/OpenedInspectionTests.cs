using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.IntegrationTests.Inspection.Fixtures;

namespace CamabrS.IntegrationTests.Inspection;
public sealed class OpenedInspectionTests(AppFixture fixture) : GivenOpenedInspection(fixture)
{
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    //assign

    [Fact]
    public async Task Assigning_a_Specialist_to_an_open_Inspection_should_succeed()
    {
        //when
        var result = await Host.AssignSpecialist(
            Inspection.Id, 
            Inspection.Version, 
            BaselineData.LockHoldingSpecialistId, 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);        

        //then
        var updated = await Host.InspectionDetailsShouldBe(
            Inspection with
            {
                Status = InspectionStatus.Assigned,
                AssignedSpecialists = [BaselineData.LockHoldingSpecialistId],
                Version = InspectionStreamVersions.Assigned                
            }, TestUser.SuperuserWithLockHoldingId);

        result.ApiResponseShouldHave(
            InspectionStreamVersions.Assigned,
            NextInspectionSteps.GetNextSteps(updated.Status, updated.Verdict));
    }

    [Fact]
    public async Task Assigning_a_non_existent_Specialist_should_fail()
    {
        //when
        var specialistId = CombGuidIdGeneration.NewGuid();
        var result = await Host.AssignSpecialist
            (Inspection.Id, 
            Inspection.Version, 
            specialistId, 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status412PreconditionFailed);
        problemDetails.Detail.ShouldBe(AssignEndpoints.GetSpecialistNotExistsErrorDetail(specialistId));
    }    

    //unassign

    [Fact]
    public async Task Unassigning_a_Specialist_from_an_opened_Inspection_should_fail()
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
    public async Task Locking_an_opened_Inspection_should_fail()
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
    public async Task Unlocking_an_opened_Inspection_should_fail()
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
    public async Task Submitting_an_opened_Inspection_result_by_any_Specialist_should_fail()
    {
        //when
        var result = await Host.SubmitInspection(
            Inspection.Id, 
            Inspection.Version, 
            CombGuidIdGeneration.NewGuid(), 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessageForSubmitting(Inspection.Id));
    }

    //sign

    [Fact]
    public async Task Signing_an_opened_Inspection_by_any_Specialist_should_fail()
    {
        //when
        var result = await Host.SignInspection(
            Inspection.Id, 
            Inspection.Version, 
            internet.Url(), 
            DateTimeOffset.Now, 
            TestUser.SuperuserWithLockHoldingId);

        //then
        var problemDetails = await result.ReadAsJsonAsync<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status403Forbidden);
        problemDetails.Detail.ShouldBe(InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Submitted, Inspection.Id));
    }

    //close

    [Fact]
    public async Task Closeing_an_opened_Inspection_should_fail()
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
    public async Task Reviewing_an_opened_Inspection_should_fail()
    {
        //when
        var result = 
            await Host.ReviewInspection(
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
    public async Task Reopening_an_opened_Inspection_should_fail()
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
    public async Task Completing_an_opened_Inspection_should_fail()
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