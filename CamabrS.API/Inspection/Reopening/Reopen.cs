using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;
using CamabrS.API.Inspection.Assigning;

namespace CamabrS.API.Inspection.Reopening;

public sealed record ReopenInspection(Guid InspectionId, int Version, DateTimeOffset ReopenedAt)
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
    public static (ApiResponse, Events, OutgoingMessages) Post(
        ReopenInspection command,
        Inspection inspection,        
        User user)
    {
        var (inspectionId, version, reopenedAt) = command;

        if (inspection.Status != InspectionStatus.Reviewed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.ReopenInspection(inspectionId, user.Id, reopenedAt));

        events.Add(new InspectionReopened(inspectionId, user.Id, reopenedAt));

        return (
            new ApiResponse(
                (version + events.Count), 
                [AssignEndpoints.AssignEnpoint]), 
                events, messages);
    }    
}