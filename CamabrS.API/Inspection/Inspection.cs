namespace CamabrS.API.Inspection;

public sealed record Inspection(
    Guid Id,
    InspectionStatus Status)
{
    public sealed record TryAssignSpecialist(Guid InspectionId, Guid SpecialistId, Guid AssignedBy, DateTimeOffset AssignedAt);
    public sealed record TryUnassignSpecialist();
    public sealed record TryLockInspection();
    public sealed record TryUnlockInspection();
    public sealed record TrySubmitInspectionResult();
    public sealed record TrySignInspection();
    public sealed record TryCloseInspection();
    public sealed record TryReviewInspection();
    public sealed record TryReopenInspection();
    public sealed record TryCompleteInspection();

    public static Inspection Create(InspectionOpened opened) =>
        new(opened.InspectionId, InspectionStatus.Opened);

    public Inspection Apply(SpecialistAssigned specialistAssigned) =>
        this with { Status = InspectionStatus.Assigned };

    public Inspection Apply(SpecialistUnassigned specialistUnassigned) =>
        this with { Status = InspectionStatus.Opened };

    public Inspection Apply(InspectionLocked inspectionLocked) =>
        this with { Status = InspectionStatus.Locked };

    public Inspection Apply(InspectionUnlocked inspectionUnlocked) =>
       this with { Status = InspectionStatus.Assigned };

    public Inspection Apply(InspectionResultSubmitted inspectionResultSubmitted) =>
       this with { Status = InspectionStatus.Submitted };

    public Inspection Apply(InspectionSigned inspectionSigned) =>
       this with { Status = InspectionStatus.Signed };

    public Inspection Apply(InspectionClosed inspectionClosed) =>
       this with { Status = InspectionStatus.Closed };

    public Inspection Apply(InspectionReviewed inspectionReviewed) =>
       this with { Status = InspectionStatus.Reviewed };

    public Inspection Apply(InspectionReopened inspectionReopened) =>
       this with { Status = InspectionStatus.Opened };

    public Inspection Apply(InspectionCompleted inspectionCompleted) =>
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

public sealed record InspectionOpened(Guid InspectionId, Guid ObjectId, Guid OpenedBy, DateTimeOffset OpenedAt);
public sealed record SpecialistAssigned(Guid SpecialistId, Guid AssignedBy, DateTimeOffset AssignedAt);
public sealed record SpecialistUnassigned(Guid UnassignedBy, DateTimeOffset UnassignedAt);
public sealed record InspectionLocked(DateTimeOffset LockedAt);
public sealed record InspectionUnlocked(Guid UnlockedBy, DateTimeOffset UnlockedAt);
public sealed record InspectionResultSubmitted(Guid FormId, DateTimeOffset SubmittedAt);
public sealed record InspectionSigned(DateTimeOffset SignedAt, string SignatureLink);
public sealed record InspectionClosed(DateTimeOffset ClosedAt);
public sealed record InspectionReviewed(Guid ReviewedBy, DateTimeOffset ReviewedAt, bool Verdict, string Summary);
public sealed record InspectionReopened(DateTimeOffset ReopenedAt);
public sealed record InspectionCompleted(DateTimeOffset ClosedAt);
