using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Submitting;

public static class SubmitEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/submit")]
    public static (IResult, Events, OutgoingMessages) Post(
        Contracts.Inspection.SubmitInspectionResult command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, _, formId) = command;

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in locked state");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.SubmitInspectionResult(inspectionId, user.Id, formId, now));

        events.Add(new InspectionResultSubmitted(inspectionId, user.Id, formId, now));

        return (Ok(), events, messages);
    }
}
