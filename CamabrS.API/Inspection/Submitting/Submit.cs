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

    [WolverinePost(SubmitEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        SubmitInspectionResult command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, formId, submittedAt) = command;

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, inspectionId));

        events.Add(new Inspection.SubmitInspectionResult(inspectionId, user.Id, formId, submittedAt));

        events.Add(new InspectionResultSubmitted(inspectionId, user.Id, formId, submittedAt));

        return (
            new ApiResponse(
                (version + events.Count), 
                [SignEndpoints.SignEnpoint]),
                events, messages);
    }    
}