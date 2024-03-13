namespace CamabrS.API.Core.Http;

public record ApiResponse(int Version, List<string> NextSteps);

public record ApiCreationResponse(Guid Id, int Version, List<string> NextSteps) : ApiResponse(Version, NextSteps);