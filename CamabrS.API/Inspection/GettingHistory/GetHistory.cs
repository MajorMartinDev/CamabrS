using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Http;

namespace CamabrS.API.Inspection.GettingHistory;

public static class GetHistoryEndpoints
{
    public const string GetHistoryEndpointsByInspectionId = "/api/inspections/{incidentId:guid}/history";

    [WolverineGet(GetHistoryEndpointsByInspectionId)]
    public static Task<IReadOnlyList<InspectionHistory>> GetHistory(
        [FromRoute] Guid incidentId,
        IQuerySession querySession,
        CancellationToken ct
    ) =>
        querySession.Query<InspectionHistory>().Where(i => i.InspectionId == incidentId).ToListAsync(ct);
}