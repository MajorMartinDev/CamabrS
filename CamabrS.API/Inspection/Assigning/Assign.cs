﻿using CamabrS.API.Core.Http;
using CamabrS.API.Specialist.GettingDetails;

namespace CamabrS.API.Inspection.Assigning;

public sealed record AssignSpecialist(Guid InspectionId, int Version, Guid SpecialistId, DateTimeOffset AssignedAt)
{
    public sealed class AssignSpecialistValidator : AbstractValidator<AssignSpecialist>
    {
        public AssignSpecialistValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.SpecialistId).NotEmpty().NotNull();
        }
    }
};

public static class AssignEndpoints
{
    public const string AssignEnpoint = "/api/inspections/assign";

    public static string GetSpecialistNotExistsErrorDetail(Guid specialistId)
        => $"Specialist with id {specialistId} does not exist!";

    public static string GetSpecialistHasAlreadyBeenAddedErrorDetail(Guid specialistId)
        => $"Specialist with id {specialistId} has already been assigned to the Inspection.";

    [WolverineBefore]
    public static async Task<ProblemDetails> CheckIfSpecialistExists(
        AssignSpecialist command,
        IDocumentSession session)
    {
        var (_, _, specialistId, _) = command;

        var specialistExists = await session.Query<SpecialistDetails>().AnyAsync(x => x.Id == specialistId);

        return specialistExists ? 
            WolverineContinue.NoProblems : 
            new ProblemDetails 
            { 
                Status = StatusCodes.Status412PreconditionFailed, 
                Detail = GetSpecialistNotExistsErrorDetail(specialistId) 
            };         
    }
        
    [Authorize("can:assign")]
    [WolverinePost(AssignEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        AssignSpecialist command,
        [Required] Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, specialistId, assignedAt) = command;

        var notAssignable = !(inspection.Status == InspectionStatus.Opened || inspection.Status == InspectionStatus.Assigned);

        if (notAssignable)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessageForAssignment(inspectionId));

        var specialistHasAlreadyBeenAdded = inspection.AssignedSpecialists.Contains(specialistId);
        if (specialistHasAlreadyBeenAdded) throw new InvalidOperationException(GetSpecialistHasAlreadyBeenAddedErrorDetail(specialistId));

        events.Add(new Inspection.AssignSpecialist(inspectionId, user.Id, specialistId, assignedAt));

        //TODO add missing business logic to check if the specialist can be assigned based on certifications
        //TODO consider saving or logging information if the specialist could not be assigned based in missing certification
        SpecialistAssigned specialistAssigned = new(inspectionId, user.Id, specialistId, assignedAt);
        events.Add(specialistAssigned);

        //TODO send off message to notify Specialist that they got assigned an inspection

        var newState = inspection.Apply(specialistAssigned);

        return (
            new ApiResponse(
                (version + events.Count),
                NextInspectionSteps.GetNextSteps(newState.Status)),
            events, messages);
    }    
}