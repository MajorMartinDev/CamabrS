using FluentValidation;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Attributes;
using CamabrS.API.Inspection.GettingDetails;

namespace CamabrS.API.Inspection.Assigning;

public sealed record UnassignSpecialist(Guid InspectionId, int Version, Guid SpecialistId)
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
    [WolverineBefore]
    public static async Task<ProblemDetails> ValidateInspectionState(
        AssignSpecialist command,
        IDocumentSession session)
    {
        var (inspectionId, _, specialistId) = command;

        var inspectionHasSpecialistAssigned = await session.Query<InspectionDetails>()
            .AnyAsync(x => x.Id == inspectionId && x.AssignedSpecialists.Contains(specialistId));

        return inspectionHasSpecialistAssigned
            ? WolverineContinue.NoProblems
            : new ProblemDetails { Detail = $"Specialist with id {specialistId} was not previously assigned to Inspection with id {inspectionId}!" };
    }

    [AggregateHandler]
    [WolverinePost("/api/inspections/unassign")]
    public static (IResult, Events, OutgoingMessages) Post(
        UnassignSpecialist command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version, specialistId) = command;

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in assigned state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.UnassignSpecialist(inspectionId, user.Id, specialistId, now));

        //TODO add missing business logic to check if the specialist can be assigned based on certifications
        //TODO consider saving or logging information if the specialist could not be assigned based in missing certification
        events.Add(new SpecialistUnassigned(inspectionId, user.Id, specialistId, now));

        //TODO send off message to notify Specialist that they got assigned an inspection

        return (Ok(version + events.Count), events, messages);
    }    
}