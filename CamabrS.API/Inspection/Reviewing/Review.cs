using Wolverine.Http;
using Wolverine.Marten;
using Wolverine;
using static Microsoft.AspNetCore.Http.TypedResults;
using CamabrS.API.Core.Http;
using FluentValidation;

namespace CamabrS.API.Inspection.Reviewing;

public sealed record ReviewInspection(Guid InspectionId, int Version, bool Verdict, string Summary)
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
    [WolverinePost("/api/inspections/review"), AggregateHandler]
    public static (IResult, Events, OutgoingMessages) Post(
        ReviewInspection command,
        Inspection inspection,
        DateTimeOffset now,
        User user)
    {
        var (inspectionId, version, verdict, summary) = command;

        if (inspection.Status != InspectionStatus.Closed)
            throw new InvalidOperationException($"Inspection with id {inspectionId} is not in closed state!");

        var events = new Events();
        var messages = new OutgoingMessages();

        events.Add(new Inspection.ReviewInspection(inspectionId, user.Id, verdict, summary, now));

        events.Add(new InspectionReviewed(inspectionId, user.Id, verdict, summary, now));

        return (Ok(version + events.Count), events, messages);
    }   
}