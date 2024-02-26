using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Reopening;

public sealed record ReopenInspection(Guid InspectionId, int Version)
{
    public sealed class ReopenInspectionValidator : AbstractValidator<ReopenInspection>
    {
        public ReopenInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
        }
    }
};

public static class ReopenEndpoints
{
    public const string ReopenEnpoint = "/api/inspections/reopen";

    [WolverinePost(ReopenEnpoint), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        ReopenInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version) = command;

        if (inspection.Status != InspectionStatus.Reviewed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.ReopenInspection(inspectionId, user.Id, now));

        events.Add(new InspectionReopened(inspectionId, user.Id, now));

        return (Ok(version + events.Count), events, messages);
    }    
}