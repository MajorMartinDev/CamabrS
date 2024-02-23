using FluentValidation;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Assigning;

public static class UnassignEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/unassign")]
    public static (IResult, Events, OutgoingMessages) Post(
        Contracts.Inspection.UnassignSpecialist command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, _, specialistId) = command;

        if (inspection.Status != InspectionStatus.Assigned)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in assigned state");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.UnassignSpecialist(inspectionId, user.Id, specialistId, now));

        //TODO add missing business logic to check if the specialist can be assigned based on certifications
        //TODO consider saving or logging information if the specialist could not be assigned based in missing certification
        events.Add(new SpecialistUnassigned(inspectionId, user.Id, specialistId, now));

        //TODO send off message to notify Specialist that they got assigned an inspection

        return (Ok(), events, messages);
    }

    public class UnassignSpecialistValidator : AbstractValidator<Contracts.Inspection.UnassignSpecialist>
    {
        public UnassignSpecialistValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }    
}