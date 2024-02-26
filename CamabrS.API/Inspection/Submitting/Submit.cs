using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Submitting;

public sealed record SubmitInspectionResult(Guid InspectionId, int Version, Guid FormId)
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
    public static (IResult, Events, OutgoingMessages) Post(
        SubmitInspectionResult command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version, formId) = command;

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.SubmitInspectionResult(inspectionId, user.Id, formId, now));

        events.Add(new InspectionResultSubmitted(inspectionId, user.Id, formId, now));

        return (Ok(version + events.Count), events, messages);
    }    
}