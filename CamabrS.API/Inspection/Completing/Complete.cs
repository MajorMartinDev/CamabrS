using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Completing;

public sealed record CompleteInspection(Guid InspectionId, int Version)
{
    public sealed class CompleteInspectionValidator : AbstractValidator<CompleteInspection>
    {
        public CompleteInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }
};

public static class CompleteEndpoints
{
    [AggregateHandler]
    [WolverinePost("/api/inspections/complete")]
    public static (IResult, Events, OutgoingMessages) Post(
        CompleteInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command;

        if (inspection.Status != InspectionStatus.Reviewed)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in reviewed state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.CompleteInspection(inspectionId, user.Id, now));

        events.Add(new InspectionCompleted(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }    
}