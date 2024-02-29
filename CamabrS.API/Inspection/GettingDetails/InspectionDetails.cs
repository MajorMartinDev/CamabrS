namespace CamabrS.API.Inspection.GettingDetails;

public sealed record InspectionDetails
    (Guid Id,
    //TODO this doesn not differ from Inspection for now, but once the Specialist stream is defined as well this will be enriched
    Guid[] AssignedSpecialists, 
    InspectionStatus Status,
    Guid LockHoldingSpecialist = default,
    Guid FormId = default,
    bool Verdict = default,
    string? SignatureLink = default,
    string? Summary = default,
    int Version = 1);

public sealed class InspectionDetailsProjection: SingleStreamProjection<InspectionDetails>
{
    public static InspectionDetails Create(IEvent<InspectionOpened> opened) =>
        new(opened.Id, [], InspectionStatus.Opened);
   
    public InspectionDetails Apply(SpecialistAssigned specialistAssigned, InspectionDetails current) =>
        current with 
        { 
            AssignedSpecialists = current.AssignedSpecialists.Union
            (
                new[] { specialistAssigned.SpecialistId }
            ).ToArray(),
            Status = InspectionStatus.Assigned
        };

    //TODO missing business logic
    public InspectionDetails Apply(SpecialistUnassigned specialistUnassigned, InspectionDetails current) =>
        UnassignSpecialist(specialistUnassigned, current);

    public InspectionDetails Apply(InspectionLocked inspectionLocked, InspectionDetails current) =>
        current with { LockHoldingSpecialist = inspectionLocked.LockHoldingSpecialist, Status = InspectionStatus.Locked };

    public InspectionDetails Apply(InspectionUnlocked inspectionUnlocked, InspectionDetails current) =>
        current with { Status = InspectionStatus.Assigned };

    public InspectionDetails Apply(InspectionResultSubmitted inspectionResultSubmitted, InspectionDetails current) =>
        current with { Status = InspectionStatus.Submitted, FormId = inspectionResultSubmitted.FormId };

    public InspectionDetails Apply(InspectionSigned inspectionSigned, InspectionDetails current) =>
        current with { Status = InspectionStatus.Signed, SignatureLink = inspectionSigned.SignatureLink };

    public InspectionDetails Apply(InspectionClosed inspectionClosed, InspectionDetails current) =>
        current with { Status = InspectionStatus.Closed };

    public InspectionDetails Apply(InspectionReviewed inspectionReviewed, InspectionDetails current) =>
        current with { Status = InspectionStatus.Reviewed, Verdict = inspectionReviewed.Verdict, Summary = inspectionReviewed.Summary };

    public InspectionDetails Apply(InspectionReopened inspectionReopened, InspectionDetails current) =>
        current with { Status = InspectionStatus.Opened, AssignedSpecialists = [] };

    public InspectionDetails Apply(InspectionCompleted inspectionCompleted, InspectionDetails current) =>
        current with { Status = InspectionStatus.Completed };

    private static InspectionDetails UnassignSpecialist(SpecialistUnassigned specialistUnassigned, InspectionDetails current)
    {
        var assignedSpecialist = current.AssignedSpecialists.Remove(specialistUnassigned.SpecialistId);
        InspectionStatus status = assignedSpecialist.Length == 0 ? InspectionStatus.Opened : InspectionStatus.Assigned;

        return current with { AssignedSpecialists = assignedSpecialist, Status = status };
    }
}