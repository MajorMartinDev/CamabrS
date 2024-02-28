using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;
using CamabrS.API.Inspection.Submitting;

namespace CamabrS.API.Inspection.Locking;

public sealed record LockInspection(Guid InspectionId, int Version, DateTimeOffset LockedAt)
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
    public static (ApiResponse, Events, OutgoingMessages) Post(
        LockInspection command,
        Inspection inspection,        
        User user)
    {
        var (inspectionId, version, lockedAt) = command; 

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.LockInspection(inspectionId, user.Id, lockedAt));

        events.Add(new InspectionLocked(inspectionId, user.Id, lockedAt));

        return (
            new ApiResponse(
                (version + events.Count),
                [UnlockEndpoints.UnlockEnpoint, SubmitEndpoints.SubmitEnpoint]),
                events, messages);
    }    
}