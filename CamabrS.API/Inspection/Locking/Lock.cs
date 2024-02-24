using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;
using CamabrS.Contracts.Inspection;

namespace CamabrS.API.Inspection.Locking;

public static class LockEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/lock")]
    public static (IResult, Events, OutgoingMessages) Post(
        LockInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command; 

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in assigned state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.LockInspection(inspectionId, user.Id, now));

        events.Add(new InspectionLocked(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }

    public class LockInspectionValidator : AbstractValidator<LockInspection>
    {
        public LockInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();            
        }
    }
}