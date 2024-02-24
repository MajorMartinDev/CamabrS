using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;
using CamabrS.Contracts.Inspection;

namespace CamabrS.API.Inspection.Closeing;
public static class CloseEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/close")]
    public static (IResult, Events, OutgoingMessages) Post(
        CloseInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command;

        if (inspection.Status != InspectionStatus.Signed)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in signed state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.CloseInspection(inspectionId, user.Id, now));

        events.Add(new InspectionClosed(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }

    public class CloseInspectionValidator : AbstractValidator<CloseInspection>
    {
        public CloseInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();            
        }
    }
}