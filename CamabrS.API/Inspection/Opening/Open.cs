using CamabrS.Contracts.Inspection;
using FluentValidation;
using JasperFx.Core;
using Wolverine.Http;
using Wolverine.Marten;

namespace CamabrS.API.Inspection.Opening;

public static class OpenEndpoint
{
    [WolverinePost("/api/inspection/open")]
    public static (CreationResponse, IStartStream) OpenInspection(
        OpenInspection command,
        DateTimeOffset now,
        User user)
    {
        //TODO: log command

        var objectId = command.ObjectId;
        var inspectionId = CombGuidIdGeneration.NewGuid();

        var @event = new InspectionOpened(inspectionId, objectId, user.Id, now);

        return (
                new CreationResponse($"/api/inspection/{inspectionId}"),
                new StartStream<Inspection>(inspectionId, @event)
            );
    }

    public class OpenInspectionValidator : AbstractValidator<OpenInspection>
    {
        public OpenInspectionValidator()
        {
            RuleFor(x => x.ObjectId).NotEmpty().NotNull();
        }
    }
}