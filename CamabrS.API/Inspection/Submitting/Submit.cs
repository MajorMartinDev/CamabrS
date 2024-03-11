using CamabrS.API.Core.Http;
using CamabrS.API.Inspection.Signing;

namespace CamabrS.API.Inspection.Submitting;

public sealed record SubmitInspectionResult(Guid InspectionId, int Version, Guid FormId, DateTimeOffset SubmittedAt)
{
    public sealed class SubmitInspectionResultValidator : AbstractValidator<SubmitInspectionResult>
    {
        public SubmitInspectionResultValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.FormId).NotEmpty().NotNull();
        }
    }
};

public static class SubmitEndpoints
{
    public const string SubmitEnpoint = "/api/inspections/submit";

    public static string GetInvalidSubmittingAttemptErrorMessage()
        => "Inspection can only be submitted by the lock holding specialist.";

    [WolverinePost(SubmitEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        SubmitInspectionResult command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, formId, submittedAt) = command;

        var invalidState = !(inspection.Status == InspectionStatus.Locked || inspection.Status == InspectionStatus.Submitted);
        if (invalidState)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessageForSubmitting(inspectionId));

        if (user.Id != inspection.LockHoldingSpecialist)
            throw new InvalidOperationException(
                GetInvalidSubmittingAttemptErrorMessage());                

        events.Add(new Inspection.SubmitInspectionResult(inspectionId, user.Id, formId, submittedAt));

        events.Add(new InspectionResultSubmitted(inspectionId, user.Id, formId, submittedAt));

        return (
            new ApiResponse(
                (version + events.Count),
                [SubmitEnpoint, SignEndpoints.SignEnpoint]),
                events, messages);
    }    
}