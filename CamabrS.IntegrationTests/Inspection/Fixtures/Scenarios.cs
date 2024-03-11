using CamabrS.API.Core.Http;
using CamabrS.API.Inspection;
using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.Closeing;
using CamabrS.API.Inspection.Completing;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Opening;
using CamabrS.API.Inspection.Reopening;
using CamabrS.API.Inspection.Reviewing;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;
public static class Scenarios{
        
    private static readonly Lorem loremIpsum = new();
    private static readonly Internet internet = new();

    public static async Task<InspectionDetails> OpenedInspection(
        this IAlbaHost api)
    {
        var assetId = BaselineData.DefaultTestAssetId;
        var createdAt = DateTimeOffset.UtcNow;

        var result = await api.OpenInspection(assetId, createdAt);

        result = await api.GetInspectionDetails(await result.GetCreatedId());
        var inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Opened);

        return inspection;
    }

    public static async Task<InspectionDetails> AssignedInspection(
        this IAlbaHost api)
    {        
        var lockHoldingSpecialistId = BaselineData.LockHoldingSpecialist;
        var assignedAt = DateTimeOffset.UtcNow;        

        var inspection = await api.OpenedInspection();
        var result = await api.AssignSpecialist(inspection.Id, inspection.Version, lockHoldingSpecialistId, assignedAt);        

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Assigned);
        inspection.AssignedSpecialists.Length.ShouldBe(1);
        inspection.AssignedSpecialists
            .FirstOrDefault().ShouldBe(lockHoldingSpecialistId);

        return inspection;      
    }

    public static async Task<InspectionDetails> LockedInspection(
        this IAlbaHost api)
    {
        var lockedAt = DateTimeOffset.UtcNow;

        var inspection = await api.AssignedInspection();
        var result = await api.LockInspection(inspection.Id, BaselineData.LockHoldingSpecialist, inspection.Version, lockedAt, BaselineData.LockHoldingSpecialist);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Locked);
        return inspection;
    }

    public static async Task<InspectionDetails> SubmittedInspection(
        this IAlbaHost api)
    {
        var submittedAt = DateTimeOffset.UtcNow;

        var inspection = await api.LockedInspection();
        var result = await api.SubmitInspection(inspection.Id, inspection.Version, BaselineData.DefaultFormId, submittedAt, BaselineData.LockHoldingSpecialist);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Submitted);
        inspection.FormId.ShouldBe(BaselineData.DefaultFormId);
        return inspection;
    }

    public static async Task<InspectionDetails> SignedInspection(
        this IAlbaHost api)
    {
        var signatureLink = internet.Url();
        var signedAt = DateTimeOffset.UtcNow;

        var inspection = await api.SubmittedInspection();
        var result = await api.SignInspection(inspection.Id, inspection.Version, signatureLink, signedAt, BaselineData.LockHoldingSpecialist);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Signed);
        inspection.SignatureLink.ShouldBe(signatureLink);
        return inspection;
    }

    public static async Task<InspectionDetails> ClosedInspection(
        this IAlbaHost api)
    {
        var closedAt = DateTimeOffset.UtcNow;

        var inspection = await api.SignedInspection();
        var result = await api.CloseInspection(inspection.Id, inspection.Version, closedAt);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Closed);
        return inspection;
    }

    public static async Task<InspectionDetails> ReviewedInspection(
        this IAlbaHost api,
        ReviewVerdict verdict = ReviewVerdict.Approved)
    {
        var summary = loremIpsum.Paragraph();
        var reviewedAt = DateTimeOffset.UtcNow;

        var inspection = await api.ClosedInspection();
        var result = await api.ReviewInspection(inspection.Id, inspection.Version, verdict, summary, reviewedAt);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Reviewed);
        inspection.Summary.ShouldBe(summary);
        inspection.Verdict.ShouldBe(verdict);
        return inspection;
    }

    public static async Task<InspectionDetails> CompletedInspection(
        this IAlbaHost api)
    {
        var completedAt = DateTimeOffset.UtcNow;

        var inspection = await api.ReviewedInspection();
        var result = await api.CompleteInspection(inspection.Id, inspection.Version, completedAt);

        result = await api.GetInspectionDetails(inspection.Id);
        inspection = await result.ReadAsJsonAsync<InspectionDetails>();

        inspection.ShouldNotBeNull();
        inspection.Status.ShouldBe(InspectionStatus.Completed);
        return inspection;
    }

    public static Task<IScenarioResult> OpenInspection(
        this IAlbaHost api,
        Guid assetId,
        DateTimeOffset openedAt,
        Guid userId = default) =>
            api.Scenario(x => 
            {
                x.Post.Url(OpenEndpoints.OpenEnpoint);
                x.Post.Json(new OpenInspection(assetId, openedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));                
            });

    public static Task<IScenarioResult> AssignSpecialist(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid specialistId,        
        DateTimeOffset assignedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {                
                x.Post
                    .Json(new AssignSpecialist(inspectionId, expectedVersion, specialistId, assignedAt))
                    .ToUrl(AssignEndpoints.AssignEnpoint);

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));                
            });

    public static Task<IScenarioResult> UnassignSpecialist(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid specialistId,        
        DateTimeOffset unassignedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(UnassignEndpoints.UnassignEnpoint);
                x.Post.Json(new UnassignSpecialist(inspectionId, expectedVersion, specialistId, unassignedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));                
            });

    public static Task<IScenarioResult> LockInspection(
        this IAlbaHost api,
        Guid inspectionId,
        Guid lockHoldingSpecialist,
        int expectedVersion,
        DateTimeOffset lockedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(LockEndpoints.LockEnpoint);
                x.Post.Json(new LockInspection(inspectionId, lockHoldingSpecialist, expectedVersion, lockedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> UnlockInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset unlockedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(UnlockEndpoints.UnlockEnpoint);
                x.Post.Json(new UnlockInspection(inspectionId, expectedVersion, unlockedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> SubmitInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid formId,
        DateTimeOffset submittedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(SubmitEndpoints.SubmitEnpoint);
                x.Post.Json(new SubmitInspectionResult(inspectionId, expectedVersion, formId, submittedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> SignInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        string signatureLink,
        DateTimeOffset submittedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(SignEndpoints.SignEnpoint);
                x.Post.Json(new SignInspection(inspectionId, expectedVersion, signatureLink, submittedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> CloseInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset closedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(CloseEndpoints.CloseEnpoint);
                x.Post.Json(new CloseInspection(inspectionId, expectedVersion, closedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> ReviewInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        ReviewVerdict verdict,
        string summary,
        DateTimeOffset reviewedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(ReviewEndpoints.ReviewEnpoint);
                x.Post.Json(new ReviewInspection(inspectionId, expectedVersion, verdict, summary, reviewedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> ReopenInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset completedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(ReopenEndpoints.ReopenEnpoint);
                x.Post.Json(new ReopenInspection(inspectionId, expectedVersion, completedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> CompleteInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset completedAt,
        Guid userId = default) =>
            api.Scenario(x =>
            {
                x.Post.Url(CompleteEndpoints.CompleteEnpoint);
                x.Post.Json(new CompleteInspection(inspectionId, expectedVersion, completedAt));

                x.IgnoreStatusCode();

                x.WithClaim(new Claim("user-id", userId.ToString()));
            });

    public static Task<IScenarioResult> GetInspectionDetails(
        this IAlbaHost api,
        Guid inspectionId,
        Guid userId = default
    ) =>
        api.Scenario(x =>
        {
            x.Get.Url($"/api/inspections/{inspectionId}");

            x.WithClaim(new Claim("user-id", userId.ToString()));
        });

    public static async Task<IScenarioResult> InspectionDetailsShouldBe(
        this IAlbaHost api,
        InspectionDetails inspection
    )
    {
        var result = await api.GetInspectionDetails(inspection.Id);

        var updated = await result.ReadAsJsonAsync<InspectionDetails>();
        updated.ShouldBeEquivalentTo(inspection);

        return result;
    }

    public static async Task<Guid> GetCreatedId(this IScenarioResult result)
    {
        var response = await result.ReadAsJsonAsync<ApiCreationResponse>();
        response.ShouldNotBeNull();        

        return response.Id;
    }    
}

public static class InspectionStreamVersions
{
    public const int Open = 1;
    public const int Assigned = 3;
    public const int Locked = 5;
    public const int Submitted = 7;
    public const int Signed = 9;
    public const int Closed = 11;
    public const int Reviewed = 13;
    public const int Completed = 15;

    public const int AssignedAnother = 5;
    public const int Unassigned = 5;
    public const int Unlocked = 7;
    public const int Resubmitted = 9;
    public const int Reopened = 15;
}