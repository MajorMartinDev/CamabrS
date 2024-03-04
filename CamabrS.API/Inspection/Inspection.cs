namespace CamabrS.API.Inspection;

public sealed record Inspection(
    Guid Id,
    Guid[] AssignedSpecialists,    
    InspectionStatus Status,
    Guid? LockHoldingSpecialist = null)
{
    //These are needed for "command sourcing".
    //I want to show the flexibility of event sourcing. Commands can be enriched
    //or transformed and appended to the event stream. Not for decision making,
    //but for history like projections and stronger auditing purposes.
    public sealed record AssignSpecialist(Guid InspectionId, Guid AssignedBy, Guid SpecialistId, DateTimeOffset AssignedAt);
    public sealed record UnassignSpecialist(Guid InspectionId, Guid UnassigendBy, Guid SpecialistId, DateTimeOffset UnassignedAt);
    public sealed record LockInspection(Guid InspectionId, Guid LockHoldingSpecialist, Guid LockedBy, DateTimeOffset LockedAt);
    public sealed record UnlockInspection(Guid InspectionId, Guid UnlockedBy, DateTimeOffset UnlockedAt);
    //TODO Guid FormId is just a placeholder for now. Figure out how to handle forms.
    public sealed record SubmitInspectionResult(Guid InspectionId, Guid SubmittedBy, Guid FormId, DateTimeOffset SubmittedAt);
    public sealed record SignInspection(Guid InspectionId, Guid SignedBy, string SignatureLink, DateTimeOffset SignedAt);
    public sealed record CloseInspection(Guid InspectionId, Guid ClosedBy, DateTimeOffset ClosedAt);
    public sealed record ReviewInspection(Guid InspectionId, Guid ReviewedBy, ReviewVerdict Verdict, string Summary, DateTimeOffset ReviewedAt);
    public sealed record ReopenInspection(Guid InspectionId, Guid ReopenedBy, DateTimeOffset ReopenedAt);
    public sealed record CompleteInspection(Guid InspectionId, Guid CompletedBy, DateTimeOffset CompletedAt);

    public static Inspection Create(IEvent<InspectionOpened> opened) =>
        new(opened.Id, [], InspectionStatus.Opened);

    public Inspection Apply(SpecialistAssigned specialistAssigned) =>
        this with 
        {
            AssignedSpecialists = AssignedSpecialists.Union
            (
                new[] { specialistAssigned.SpecialistId }
            ).ToArray(),
            Status = InspectionStatus.Assigned 
        };

    public Inspection Apply(SpecialistUnassigned specialistUnassigned)
    {
        var (assignedSpecialists, status) = Unassign(AssignedSpecialists, specialistUnassigned.SpecialistId);

        return this with { AssignedSpecialists = assignedSpecialists, Status = status };
    }

    public Inspection Apply(InspectionLocked inspectionLocked) =>
        this with { Status = InspectionStatus.Locked, LockHoldingSpecialist = inspectionLocked.LockHoldingSpecialist };

    public Inspection Apply(InspectionUnlocked inspectionUnlocked) =>
       this with { Status = InspectionStatus.Assigned, LockHoldingSpecialist = null };

    public Inspection Apply(InspectionResultSubmitted inspectionResultSubmitted) =>
       this with { Status = InspectionStatus.Submitted };

    public Inspection Apply(InspectionSigned inspectionSigned) =>
       this with { Status = InspectionStatus.Signed };

    public Inspection Apply(InspectionClosed inspectionClosed) =>
       this with { Status = InspectionStatus.Closed };

    public Inspection Apply(InspectionReviewed inspectionReviewed) =>
       this with { Status = InspectionStatus.Reviewed };

    public Inspection Apply(InspectionReopened inspectionReopened) =>
       this with { Status = InspectionStatus.Opened, AssignedSpecialists = [] };

    public Inspection Apply(InspectionCompleted inspectionCompleted) =>
       this with { Status = InspectionStatus.Completed };

    public static (Guid[], InspectionStatus) Unassign(Guid[] assignedSpecialists, Guid specialistToUnassign)
    {
        var removed = assignedSpecialists.Remove(specialistToUnassign);

        var status = removed.Length == 0 ? InspectionStatus.Opened : InspectionStatus.Assigned;

        return (removed, status);
    }
}

public enum InspectionStatus
{    
    Opened,
    Assigned,
    Locked,
    Submitted,
    Signed,
    Closed,
    Reviewed,
    Completed
}

public enum ReviewVerdict
{
    NotReviewed,
    Approved,
    Disapproved
}

public sealed record InspectionOpened(Guid OpenedBy, Guid AssetId, DateTimeOffset OpenedAt);
public sealed record SpecialistAssigned(Guid InspectionId, Guid AssignedBy, Guid SpecialistId, DateTimeOffset AssignedAt);
public sealed record SpecialistUnassigned(Guid InspectionId,Guid UnassignedBy, Guid SpecialistId, DateTimeOffset UnassignedAt);
public sealed record InspectionLocked(Guid InspectionId, Guid LockHoldingSpecialist, Guid LockedBy, DateTimeOffset LockedAt);
public sealed record InspectionUnlocked(Guid InspectionId, Guid UnlockedBy, DateTimeOffset UnlockedAt);
public sealed record InspectionResultSubmitted(Guid InspectionId, Guid SubmittedBy, Guid FormId, DateTimeOffset SubmittedAt);
public sealed record InspectionSigned(Guid InspectionId, Guid SignedBy, string SignatureLink, DateTimeOffset SignedAt);
public sealed record InspectionClosed(Guid InspectionId, Guid ClosedBy, DateTimeOffset ClosedAt);
public sealed record InspectionReviewed(Guid InspectionId, Guid ReviewedBy, ReviewVerdict Verdict, string Summary, DateTimeOffset ReviewedAt);
public sealed record InspectionReopened(Guid InspectionId, Guid ReopenedBy, DateTimeOffset ReopenedAt);
public sealed record InspectionCompleted(Guid InspectionId, Guid CompletedBy, DateTimeOffset CompletedAt);

public class InvalidStateException()
{
    public static string GetInvalidStateExceptionMessage(InspectionStatus status, Guid id) =>
        $"Inspection with id {id} is not in {status.ToString().ToLower()} state!";

    public static string GetInvalidStateExceptionMessageForAssignment(Guid id) =>
        $"Inspection with id {id} is not in opened or assigned state!";

    public static string GetInvalidStateExceptionMessageForSubmitting(Guid id) =>
        $"Inspection with id {id} is not in locked or submitted state!";
}