using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Reviewing;
public static class ReviewEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/review")]
    public static (IResult, Events, OutgoingMessages) Post(
        Contracts.Inspection.ReviewInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, _, verdict, summary) = command;

        if (inspection.Status != InspectionStatus.Closed)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in closed state");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.ReviewInspection(inspectionId, user.Id, verdict, summary, now));

        events.Add(new InspectionReviewed(inspectionId, user.Id, verdict, summary, now));

        return (Ok(), events, messages);
    }
}