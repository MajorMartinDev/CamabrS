using CamabrS.API.Core.Http;
using CamabrS.API.Inspection.Reviewing;

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
    public static (ApiResponse, Events, OutgoingMessages) Post(
        CloseInspection command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, closedAt) = command;

        if (inspection.Status != InspectionStatus.Signed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Signed, inspectionId));        

        events.Add(new Inspection.CloseInspection(inspectionId, user.Id, closedAt));

        events.Add(new InspectionClosed(inspectionId, user.Id, closedAt));

        return (
            new ApiResponse(
                (version + events.Count), 
                [ReviewEndpoints.ReviewEnpoint]),
                events, messages);
    }    
}