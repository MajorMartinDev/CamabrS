using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Locking;

public sealed record UnlockInspection(Guid InspectionId, int Version)
{
    public sealed class UnlockInspectionValidator : AbstractValidator<UnlockInspection>
    {
        public UnlockInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }
};

public static class UnlockEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/unlock")]
    public static (IResult, Events, OutgoingMessages) Post(
        UnlockInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command;

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in locked state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.UnlockInspection(inspectionId, user.Id, now));

        events.Add(new InspectionUnlocked(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }    
}