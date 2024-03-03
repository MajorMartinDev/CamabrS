namespace CamabrS.API.Core.Http;

public record ApiResponse(int Version, List<string> AvailableActions);

public record ApiCreationResponse(Guid Id, int Version, List<string> AvailableActions) : ApiResponse(Version, AvailableActions);