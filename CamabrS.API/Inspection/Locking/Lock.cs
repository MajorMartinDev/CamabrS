using CamabrS.API.Core.Http;
using CamabrS.API.Inspection.Submitting;

namespace CamabrS.API.Inspection.Locking;

public sealed record LockInspection(Guid InspectionId, Guid LockHoldingSpecialist, int Version, DateTimeOffset LockedAt)
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

    public static string GetLockHoldingSpecialistWasNotAssignedErrorDetail(Guid specialistId, Guid inspectionId)
        => $"Specialist with id {specialistId} was not previously assigned to inspection {inspectionId}!";

    [WolverinePost(LockEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        LockInspection command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, lockHoldingSpecialist, version, lockedAt) = command; 

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, inspectionId));        

        var lockHoldingSpecialistIsNotAssigned = !inspection.AssignedSpecialists.Contains(lockHoldingSpecialist);
        if (lockHoldingSpecialistIsNotAssigned)
            throw new InvalidOperationException(GetLockHoldingSpecialistWasNotAssignedErrorDetail(lockHoldingSpecialist, inspectionId));

        events.Add(new Inspection.LockInspection(inspectionId, lockHoldingSpecialist, user.Id, lockedAt));

        events.Add(new InspectionLocked(inspectionId, lockHoldingSpecialist, user.Id, lockedAt));

        return (
            new ApiResponse(
                (version + events.Count),
                [UnlockEndpoints.UnlockEnpoint, SubmitEndpoints.SubmitEnpoint]),
                events, messages);
    }    
}