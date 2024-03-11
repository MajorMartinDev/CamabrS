using CamabrS.API.Core.Http;
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

    public const string ApprovedInspectionCanNotBeReopenedErrorMessage 
        = "An approved Inspection can not be reopened!";

    [WolverinePost(ReopenEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        ReopenInspection command,
        Inspection inspection,        
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, reopenedAt) = command;

        if (inspection.Status != InspectionStatus.Reviewed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, inspectionId));

        if (inspection.Verdict != ReviewVerdict.Disapproved)
            throw new InvalidOperationException(
                ApprovedInspectionCanNotBeReopenedErrorMessage);

        events.Add(new Inspection.ReopenInspection(inspectionId, user.Id, reopenedAt));

        events.Add(new InspectionReopened(inspectionId, user.Id, reopenedAt));

        //TODO send off message to notify Specialist that they got unassigned from an inspection

        return (
            new ApiResponse(
                (version + events.Count), 
                [AssignEndpoints.AssignEnpoint]), 
                events, messages);
    }    
}