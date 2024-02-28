﻿using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;
using CamabrS.API.Inspection.Reopening;
using CamabrS.API.Inspection.Completing;

namespace CamabrS.API.Inspection.Reviewing;

public sealed record ReviewInspection(Guid InspectionId, int Version, bool Verdict, string Summary, DateTimeOffset ReviewedAt)
{
    public sealed class ReviewInspectionValidator : AbstractValidator<ReviewInspection>
    {
        public ReviewInspectionValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().NotNull();
            RuleFor(x => x.Verdict).NotEmpty().NotNull();
            //TODO define a reasonable default max length and get the value from configuration
            RuleFor(x => x.Summary).NotEmpty().NotNull().MaximumLength(4000);
        }
    }
};

public static class ReviewEndpoints
{
    public const string ReviewEnpoint = "/api/inspections/review";

    [WolverinePost(ReviewEnpoint), AggregateHandler]
    public static (ApiResponse, Events, OutgoingMessages) Post(
        ReviewInspection command,
        Inspection inspection,       
        User user)
    {
        var (inspectionId, version, verdict, summary, reviewedAt) = command;

        if (inspection.Status != InspectionStatus.Closed)
            throw new InvalidOperationException(
                InvalidStateException.GetInvalidStateExceptionMessage(InspectionStatus.Closed, inspectionId));

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.ReviewInspection(inspectionId, user.Id, verdict, summary, reviewedAt));

        events.Add(new InspectionReviewed(inspectionId, user.Id, verdict, summary, reviewedAt));

        return (
            new ApiResponse(
                (version + events.Count),                
                GetNextAvailableSteps(verdict)), 
                events, messages);
    }

    public static List<string> GetNextAvailableSteps(bool verdict)
    {
        return verdict is true ? 
            [CompleteEndpoints.CompleteEnpoint] : [ReopenEndpoints.ReopenEnpoint];
    }
}