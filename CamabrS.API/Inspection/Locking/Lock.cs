using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Locking;

public sealed record LockInspection(Guid InspectionId, int Version)
{
    public sealed class LockInspectionValidator : AbstractValidator<LockInspection>
    {
        public LockInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }
};

public static class LockEndpoints
{
    public const string LockEnpoint = "/api/inspections/lock";

    [WolverinePost(LockEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        LockInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command; 

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.LockInspection(inspectionId, user.Id, now));

        events.Add(new InspectionLocked(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }    
}