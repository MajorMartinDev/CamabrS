using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Locking;

public sealed record UnlockInspection(Guid InspectionId, int Version, DateTimeOffset UnlockedAt)
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
    public const string UnlockEnpoint = "/api/inspections/unlock";

    [WolverinePost(UnlockEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        UnlockInspection command,
        Inspection inspection,        
        User user)
    {
        var (inspectionId, version, unlockedAt) = command;

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.UnlockInspection(inspectionId, user.Id, unlockedAt));

        events.Add(new InspectionUnlocked(inspectionId, user.Id, unlockedAt));

        return (Ok(version + events.Count), events, messages);
    }    
}