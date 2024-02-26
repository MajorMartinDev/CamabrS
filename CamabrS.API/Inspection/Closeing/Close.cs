using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Closeing;

public sealed record CloseInspection(Guid InspectionId, int Version, DateTimeOffset ClosedAt)
{
    public sealed class CloseInspectionValidator : AbstractValidator<CloseInspection>
    {
        public CloseInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }
};

public static class CloseEndpoints
{
    public const string CloseEnpoint = "/api/inspections/close";

    [WolverinePost(CloseEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        CloseInspection command,
        Inspection inspection,        
        User user)
    {
        var (inspectionId, version, closedAt) = command;

        if (inspection.Status != InspectionStatus.Signed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.CloseInspection(inspectionId, user.Id, closedAt));

        events.Add(new InspectionClosed(inspectionId, user.Id, closedAt));

        return (Ok(version + events.Count), events, messages);
    }    
}