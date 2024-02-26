using CamabrS.API.Inspection.Assigning;
using CamabrS.API.Inspection.Closeing;
using CamabrS.API.Inspection.Completing;
using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.Locking;
using CamabrS.API.Inspection.Reviewing;
using CamabrS.API.Inspection.Signing;
using CamabrS.API.Inspection.Submitting;

namespace CamabrS.IntegrationTests.Inspection.Fixtures;
public static class Scenarios
{
    public static async Task<InspectionDetails> OpenedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> AssignedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> LockedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> SubmittedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> SignedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> ClosedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> ReviewedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static async Task<InspectionDetails> CompletedInspection(
        this IAlbaHost api)
    {
        return default;
    }

    public static Task<IScenarioResult> OpenInspection(
        this IAlbaHost api) =>
            api.Scenario(x => 
            {
                x.StatusCodeShouldBe(201);
            });

    public static Task<IScenarioResult> AssignInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid specialistId,        
        DateTimeOffset assignedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(AssignEndpoints.AssignEnpoint);
                x.Post.Json(new AssignSpecialist(inspectionId, expectedVersion, specialistId, assignedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> UnassignInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid specialistId,        
        DateTimeOffset unassignedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(UnassignEndpoints.UnassignEnpoint);
                x.Post.Json(new UnassignSpecialist(inspectionId, expectedVersion, specialistId, unassignedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> LockInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset lockedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(LockEndpoints.LockEnpoint);
                x.Post.Json(new LockInspection(inspectionId, expectedVersion, lockedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> UnlockInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset unlockedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(UnlockEndpoints.UnlockEnpoint);
                x.Post.Json(new UnlockInspection(inspectionId, expectedVersion, unlockedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> SubmitInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        Guid formId,
        DateTimeOffset submittedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(SubmitEndpoints.SubmitEnpoint);
                x.Post.Json(new SubmitInspectionResult(inspectionId, expectedVersion, formId, submittedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> SignInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        string signatureLink,
        DateTimeOffset submittedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(SignEndpoints.SignEnpoint);
                x.Post.Json(new SignInspection(inspectionId, expectedVersion, signatureLink, submittedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> CloseInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset closedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(CloseEndpoints.CloseEnpoint);
                x.Post.Json(new CloseInspection(inspectionId, expectedVersion, closedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> ReviewInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        bool verdict,
        string summary,
        DateTimeOffset reviewedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(ReviewEndpoints.ReviewEnpoint);
                x.Post.Json(new ReviewInspection(inspectionId, expectedVersion, verdict, summary, reviewedAt));

                x.StatusCodeShouldBeOk();
            });

    public static Task<IScenarioResult> CompleteInspection(
        this IAlbaHost api,
        Guid inspectionId,
        int expectedVersion,
        DateTimeOffset completedAt) =>
            api.Scenario(x =>
            {
                x.Post.Url(CompleteEndpoints.CompleteEnpoint);
                x.Post.Json(new CompleteInspection(inspectionId, expectedVersion, completedAt));

                x.StatusCodeShouldBeOk();
            });
}