﻿using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Assigning;

public sealed record UnassignSpecialist(Guid InspectionId, int Version, Guid SpecialistId, DateTimeOffset UnassignedAt)
{
    public sealed class UnassignSpecialistValidator : AbstractValidator<UnassignSpecialist>
    {
        public UnassignSpecialistValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.SpecialistId).NotEmpty().NotNull();
        }
    }
};

public static class UnassignEndpoints
{
    public const string UnassignEnpoint = "/api/inspections/unassign";

    public static string GetSpecialistWasPreviouslyAssignedErrorDetail(Guid specialistId, Guid inspectionId)
        => $"Specialist with id {specialistId} was not previously assigned to Inspection with id {inspectionId}!";

    [Authorize("can:assign")]
    [WolverinePost(UnassignEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        UnassignSpecialist command,
        [Required] Inspection inspection,
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, specialistId, unassignedAt) = command;

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Assigned, inspectionId));

        var specialistWasNotAssigned = !inspection.AssignedSpecialists.Contains(specialistId);
        if (specialistWasNotAssigned) throw new InvalidOperationException(GetSpecialistWasPreviouslyAssignedErrorDetail(specialistId, inspectionId));

        events.Add(new Inspection.UnassignSpecialist(inspectionId, user.Id, specialistId, unassignedAt));

        SpecialistUnassigned specialistUnassigned = new(inspectionId, user.Id, specialistId, unassignedAt);
        events.Add(specialistUnassigned);

        //TODO send off message to notify Specialist that they got unassigned from an inspection

        var newState = inspection.Apply(specialistUnassigned);

        return (
            new ApiResponse(
                (version + events.Count),
                NextInspectionSteps.GetNextSteps(newState.Status)),
            events, messages);
    }    
}