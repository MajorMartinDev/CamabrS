using CamabrS.API.Core.Http;
using CamabrS.Contracts.Inspection;
using FluentValidation;
using JasperFx.Core;
using Wolverine.Http;
using Wolverine.Marten;

namespace CamabrS.API.Inspection.Opening;

public static class OpenEndpoints
{
    [WolverinePost("/api/inspections/open")]
    public static (CreationResponse, IStartStream) OpenInspection(
        OpenInspection command,
        DateTimeOffset now,
        User user)
    {
        var objectId = command.ObjectId;
        var inspectionId = CombGuidIdGeneration.NewGuid();

        var @event = new InspectionOpened(inspectionId, objectId, user.Id, now);

        return (
                new CreationResponse($"/api/inspections/{inspectionId}"),
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