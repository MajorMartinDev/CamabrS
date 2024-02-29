﻿using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Completing;

public sealed record CompleteInspection(Guid InspectionId, int Version, DateTimeOffset CompletedAt)
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
    public const string CompleteEnpoint = "/api/inspections/complete";

    [WolverinePost(CompleteEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        CompleteInspection command,
        Inspection inspection,       
        User user)
    {
        var events = new Events();
        var messages = new OutgoingMessages();

        var (inspectionId, version, completedAt) = command;

        if (inspection.Status != InspectionStatus.Reviewed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Reviewed, inspectionId));
        
        events.Add(new Inspection.CompleteInspection(inspectionId, user.Id, completedAt));

        events.Add(new InspectionCompleted(inspectionId, user.Id, completedAt));

        return (
            new ApiResponse(
                (version + events.Count), 
                []),
                events, messages);
    }    
}