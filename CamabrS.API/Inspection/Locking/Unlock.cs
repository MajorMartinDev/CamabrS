using CamabrS.API.Core.Http;
using CamabrS.API.Inspection.Assigning;

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

    public static string GetInvalidUnlockingAttemptErrorMessage()
        => "Inspection can only be unlocked by the lock holding specialist.";

    [WolverinePost(UnlockEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        UnlockInspection command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, unlockedAt) = command;        

        if (inspection.Status != InspectionStatus.Locked)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Locked, inspectionId));

        if (user.Id != inspection.LockHoldingSpecialist)
            throw new InvalidOperationException(
                GetInvalidUnlockingAttemptErrorMessage());

        events.Add(new Inspection.UnlockInspection(inspectionId, user.Id, unlockedAt));

        events.Add(new InspectionUnlocked(inspectionId, user.Id, unlockedAt));

        return (
            new ApiResponse(
                (version + events.Count),
                [UnassignEndpoints.UnassignEnpoint, LockEndpoints.LockEnpoint]),
                events, messages);
    }    
}