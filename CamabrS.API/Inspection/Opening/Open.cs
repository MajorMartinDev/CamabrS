using CamabrS.API.Asset.GettingDetails;
using CamabrS.API.Core.Http;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Attributes;
using Wolverine.Http;
using Wolverine.Marten;

namespace CamabrS.API.Inspection.Opening;

public sealed record OpenInspection(Guid AssetId)
{
    public sealed class OpenInspectionValidator : AbstractValidator<OpenInspection>
    {
        public OpenInspectionValidator()
        {
            RuleFor(x => x.AssetId).NotEmpty().NotNull();
        }
    }
};

public static class OpenEndpoints
{
    public const string OpenEnpoint = "/api/inspections/open"; 
    
    public static string GetAssetNotExistsErrorDetail(Guid assetId) 
        => $"Asset with id {assetId} does not exist!";



    [WolverineBefore]
    public static async Task<ProblemDetails> ValidateInspectionState(
        OpenInspection command,
        IDocumentSession session)
    {
        var assetExists = await session.Query<AssetDetails>()
            .AnyAsync(x => x.Id == command.AssetId);

        return assetExists
            ? WolverineContinue.NoProblems            
            : new ProblemDetails
                {
                    Status = 403,
                    Detail = GetAssetNotExistsErrorDetail(command.AssetId)
            };
    }

    public record NewInspectionOpenedResponse(Guid InspectionId) 
        : CreationResponse("/api/inspections/" + InspectionId);

    [WolverinePost(OpenEnpoint)]
    public static (NewInspectionOpenedResponse, IStartStream) OpenInspection(
        OpenInspection command,
        DateTimeOffset now,
        User user)
    {
        var inspectionOpened = new InspectionOpened(user.Id, command.AssetId, now);

        var open = MartenOps.StartStream<Inspection>(inspectionOpened);

        return (new NewInspectionOpenedResponse(open.StreamId), open);
    }
}