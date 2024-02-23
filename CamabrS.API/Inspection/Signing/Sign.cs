using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Signing;
public static class SignEndpoints 
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/sign")]
    public static (IResult, Events, OutgoingMessages) Post(
        Contracts.Inspection.SignInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, _, signatureLink) = command;

        if (inspection.Status != InspectionStatus.Submitted)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in submitted state");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.SignInspection(inspectionId, user.Id, signatureLink, now));

        events.Add(new InspectionSigned(inspectionId, user.Id, signatureLink, now));

        return (Ok(), events, messages);
    }
}