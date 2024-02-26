using JasperFx.Core;
using Marten.Events;
using Marten.Events.Projections;
using static CamabrS.API.Inspection.Inspection;


namespace CamabrS.API.Inspection.GettingHistory;

public sealed record InspectionHistory(Guid Id, Guid InspectionId, string Description);

public sealed class InspectionHistoryTransformation : EventProjection 
{
    public InspectionHistory Transform(IEvent<InspectionOpened> input)
    {
        var (openedBy, assetId, openedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            input.Id,
            $"['{openedAt}'] Inspection with id: '{input.Id}' for asset with id: '{assetId}' opened by user '{openedBy}'.");
    }

    public InspectionHistory Transform(IEvent<AssignSpecialist> input)
    {
        var (inspectionId, assignedBy, specialistId, assignedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{assignedAt}'] User '{assignedBy}' wants to assign Specialist with id: '{specialistId}' to Inspection with id: '{inspectionId}'.");
    }

    public InspectionHistory Transform(IEvent<SpecialistAssigned> input)
    {
        var (inspectionId, assignedBy, specialistId, assignedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{assignedAt}'] Specialist with id: '{specialistId}' assigned to Inspection '{inspectionId}' by user '{assignedBy}'.");
    }

    public InspectionHistory Transform(IEvent<UnassignSpecialist> input)
    {
        var(inspectionId, unassignedBy, specialistId, unassasignedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{unassasignedAt}'] User '{unassignedBy}' wants to unassign Specialist with id: '{specialistId}' from Inspection with id: '{inspectionId}'.");
    }

    public InspectionHistory Transform(IEvent<SpecialistUnassigned> input)
    {
        var(inspectionId, unassignedBy, specialistId, unassignedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{unassignedAt}'] Specialist with id: '{specialistId}' unassigned to Inspection '{inspectionId}' by user '{unassignedBy}'.");
    }

    public InspectionHistory Transform(IEvent<LockInspection> input)
    {
        var(inspectionId, lockedBy, lockedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{lockedAt}'] User with id: '{lockedBy}' wants to lock Inspection.");
    }

    public InspectionHistory Transform(IEvent<InspectionLocked> input)
    {
        var(inspectionId, lockedBy, lockedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{lockedAt}'] User with id: '{lockedBy}' locked Inspection.");
    }

    public InspectionHistory Transform(IEvent<UnlockInspection> input)
    {
        var(inspectionId, unlockedBy, unlockedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{unlockedAt}'] User with id: '{unlockedBy}' wants to unlock Inspection.");
    }

    public InspectionHistory Transform(IEvent<InspectionUnlocked> input)
    {
        var(inspectionId, unlockedBy, unlockedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{unlockedAt}'] User with id: '{unlockedBy}' unlocked Inspection.");
    }

    public InspectionHistory Transform(IEvent<SubmitInspectionResult> input)
    {
        var(inspectionId, submittedBy, formId, submittedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{submittedAt}'] User with id: '{submittedBy}' is submitting Form with id '{formId}'.");
    }

    public InspectionHistory Transform(IEvent<InspectionResultSubmitted> input)
    {
        var(inspectionId, submittedBy, formId, submittedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{submittedAt}'] User with id: '{submittedBy}' submitted Form with id '{formId}'.");
    }

    public InspectionHistory Transform(IEvent<SignInspection> input)
    {
        var(inspectionId, signedBy, _, signedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{signedAt}'] User with id: '{signedBy}' is signing the Inspection.");
    }

    public InspectionHistory Transform(IEvent<InspectionSigned> input)
    {
        var(inspectionId, signedBy, signedAt, signatureLink) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{signedAt}'] User with id: '{signedBy}' signed the Inspection.");
    }

    public InspectionHistory Transform(IEvent<CloseInspection> input)
    {
        var(inspectionId, closedBy, closedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{closedAt}'] User with id '{closedBy}' wants to close the Inspection.");
    }

    public InspectionHistory Transform(IEvent<InspectionClosed> input)
    {
        var(inspectionId, closedBy, closedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{closedAt}'] User with id '{closedBy}' closed the Inspection.");
    }

    public InspectionHistory Transform(IEvent<ReviewInspection> input)
    {
        var(inspectionId, reviewedBy, verdict, _, reviewedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{reviewedAt}'] User with id: '{reviewedBy}' wants to send Inspection review with verdict '{verdict}'.");
    }

    public InspectionHistory Transform(IEvent<InspectionReviewed> input)
    {
        var(inspectionId, reviewedBy, reviewedAt, verdict, summary) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{reviewedAt}'] User with id: '{reviewedBy}' reviewed the Inspection.");
    }

    public InspectionHistory Transform(IEvent<ReopenInspection> input)
    {
        var(inspectionId, reopenedBy, reopenedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{reopenedAt}'] User with id: '{reopenedBy}' wants to reopen the Inspection");
    }

    public InspectionHistory Transform(IEvent<InspectionReopened> input)
    {
        var (inspectionId, reopenedBy, reopenedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{reopenedAt}'] User with id: '{reopenedBy}' reopened the Inspection");
    }

    public InspectionHistory Transform(IEvent<CompleteInspection> input)
    {
        var (inspectionId, completedBy, completedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{completedAt}'] User with id: '{completedBy}' wants to complete the Inspection.");
    }

    public InspectionHistory Transform(IEvent<InspectionCompleted> input)
    {
        var (inspectionId, completedBy, completedAt) = input.Data;

        return new InspectionHistory(
            CombGuidIdGeneration.NewGuid(),
            inspectionId,
            $"['{completedAt}'] User with id: '{completedBy}' completed the Inspection.");
    }
}