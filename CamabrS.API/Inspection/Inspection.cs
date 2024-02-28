using Marten.Events;

namespace CamabrS.API.Inspection;

public sealed record Inspection(
    Guid Id,
    InspectionStatus Status)
{
    //These are needed for "command sourcing".
    //I want to show the flexibility of event sourcing. Commands can be enriched
    //or transformed and appended to the event stream. Not for decision making,
    //but for history like projections and stronger auditing purposes.
    public sealed record AssignSpecialist(Guid InspectionId, Guid AssignedBy, Guid SpecialistId, DateTimeOffset AssignedAt);
    public sealed record UnassignSpecialist(Guid InspectionId, Guid UnassigendBy, Guid SpecialistId, DateTimeOffset UnassignedAt);
    public sealed record LockInspection(Guid InspectionId, Guid LockedBy, DateTimeOffset LockedAt);
    public sealed record UnlockInspection(Guid InspectionId, Guid UnlockedBy, DateTimeOffset UnlockedAt);
    //TODO Guid FormId is just a placeholder for now. Figure out how to handle forms.
    public sealed record SubmitInspectionResult(Guid InspectionId, Guid SubmittedBy, Guid FormId, DateTimeOffset SubmittedAt);
    public sealed record SignInspection(Guid InspectionId, Guid SignedBy, string SignatureLink, DateTimeOffset SignedAt);
    public sealed record CloseInspection(Guid InspectionId, Guid ClosedBy, DateTimeOffset ClosedAt);
    public sealed record ReviewInspection(Guid InspectionId, Guid ReviewedBy, bool Verdict, string Summary, DateTimeOffset ReviewedAt);
    public sealed record ReopenInspection(Guid InspectionId, Guid ReopenedBy, DateTimeOffset ReopenedAt);
    public sealed record CompleteInspection(Guid InspectionId, Guid CompletedBy, DateTimeOffset CompletedAt);

    public static Inspection Create(IEvent<InspectionOpened> Opened) =>
        new(Opened.Id, InspectionStatus.Opened);

    public Inspection Apply(SpecialistAssigned SpecialistAssigned) =>
        this with { Status = InspectionStatus.Assigned };

    //TODO missing business logic
    public Inspection Apply(SpecialistUnassigned SpecialistUnassigned) =>
        this with { Status = InspectionStatus.Opened };

    public Inspection Apply(InspectionLocked InspectionLocked) =>
        this with { Status = InspectionStatus.Locked };

    public Inspection Apply(InspectionUnlocked InspectionUnlocked) =>
       this with { Status = InspectionStatus.Assigned };

    public Inspection Apply(InspectionResultSubmitted InspectionResultSubmitted) =>
       this with { Status = InspectionStatus.Submitted };

    public Inspection Apply(InspectionSigned InspectionSigned) =>
       this with { Status = InspectionStatus.Signed };

    public Inspection Apply(InspectionClosed InspectionClosed) =>
       this with { Status = InspectionStatus.Closed };

    public Inspection Apply(InspectionReviewed InspectionReviewed) =>
       this with { Status = InspectionStatus.Reviewed };

    public Inspection Apply(InspectionReopened InspectionReopened) =>
       this with { Status = InspectionStatus.Opened };

    public Inspection Apply(InspectionCompleted InspectionCompleted) =>
       this with { Status = InspectionStatus.Completed };
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

public sealed record InspectionOpened(Guid OpenedBy, Guid AssetId, DateTimeOffset OpenedAt);
public sealed record SpecialistAssigned(Guid InspectionId, Guid AssignedBy, Guid SpecialistId, DateTimeOffset AssignedAt);
public sealed record SpecialistUnassigned(Guid InspectionId,Guid UnassignedBy, Guid SpecialistId, DateTimeOffset UnassignedAt);
public sealed record InspectionLocked(Guid InspectionId, Guid LockedBy, DateTimeOffset LockedAt);
public sealed record InspectionUnlocked(Guid InspectionId, Guid UnlockedBy, DateTimeOffset UnlockedAt);
public sealed record InspectionResultSubmitted(Guid InspectionId, Guid SubmittedBy, Guid FormId, DateTimeOffset SubmittedAt);
public sealed record InspectionSigned(Guid InspectionId, Guid SignedBy, string SignatureLink, DateTimeOffset SignedAt);
public sealed record InspectionClosed(Guid InspectionId, Guid ClosedBy, DateTimeOffset ClosedAt);
public sealed record InspectionReviewed(Guid InspectionId, Guid ReviewedBy, bool Verdict, string Summary, DateTimeOffset ReviewedAt);
public sealed record InspectionReopened(Guid InspectionId, Guid ReopenedBy, DateTimeOffset ReopenedAt);
public sealed record InspectionCompleted(Guid InspectionId, Guid CompletedBy, DateTimeOffset CompletedAt);

public class InvalidStateException()
{
    public static string GetInvalidStateExceptionMessage(InspectionStatus status, Guid id) =>
        $"Inspection with id {id} is not in {status.ToString().ToLower()} state!";

    public static string GetInvalidStateExceptionMessageForAssignment(Guid id) =>
        $"Inspection with id {id} is not in opened or assigned state!";

}