using CamabrS.API.Specialist;
using CamabrS.Contracts.Inspection;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Attributes;
using Wolverine.Http;
using Wolverine.Marten;
using static CamabrS.API.Inspection.Inspection;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace CamabrS.API.Inspection.Assigning;

public static class AssignEndpoint
{
    [AggregateHandler]
    [WolverinePost("/api/inspection/assign")]
    public static (IResult, Events, OutgoingMessages) Post(
        AssignSpecialist command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, specialistId) = command;

        if (inspection.Status != InspectionStatus.Opened)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in opened state");

        var events = new Events();
        var messages = new OutgoingMessages();

        //TODO consider/try moveing "command sourcing" to Wolverine before
        events.Add(new TryAssignSpecialist(inspectionId, specialistId, user.Id, now));

        //TODO add missing business logic to check if the specialist can be assigned based on certifications
        events.Add(new SpecialistAssigned(specialistId, user.Id, now));

        //TODO send off message to notify Specialist that they got assigned an inspection

        return (Ok(), events, messages);
    }

    public class AssignSpecialistValidator : AbstractValidator<AssignSpecialist>
    {
        public AssignSpecialistValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }

    [WolverineBefore]
    public static async Task<ProblemDetails> ValidateInspectionState(
        AssignSpecialist command,
        IDocumentSession session)
    {
        var (inspectionId, specialistId) = command;

        var specialistExists = await session.Query<SpecialistInfo>().AnyAsync(x => x.Id == specialistId);

        return specialistExists
            ? WolverineContinue.NoProblems
            : new ProblemDetails { Detail = $"Specialist with id {specialistId} does not exist" };        
    }
}