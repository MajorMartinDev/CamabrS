using CamabrS.API.Asset.GettingDetails;
using CamabrS.API.Core.Http;

namespace CamabrS.API.Inspection.Opening;

public sealed record OpenInspection(Guid AssetId, DateTimeOffset OpenedAt)
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
                    Status = StatusCodes.Status412PreconditionFailed,
                    Detail = GetAssetNotExistsErrorDetail(command.AssetId)
            };
    }

    [Authorize("can:open")]
    [WolverinePost(OpenEnpoint)]
    public static (ApiCreationResponse, IStartStream) OpenInspection(
        OpenInspection command,        
        User user)
    {
        var (assetId, openedAt) = command;

        var inspectionOpened = new InspectionOpened(user.Id, assetId, openedAt);

        var open = MartenOps.StartStream<Inspection>(inspectionOpened);

        return (
            new ApiCreationResponse(
                Id: open.StreamId, 
                Version: 1, 
                NextInspectionSteps.GetNextSteps(InspectionStatus.Opened)), 
            open);
    }
}