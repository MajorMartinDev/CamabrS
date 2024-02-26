using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Http;

namespace CamabrS.API.Inspection.GettingHistory;

public static class GetHistoryEndpoints
{   
    [WolverineGet("/api/inspections/{incidentId:guid}/history")]
    public static Task<IReadOnlyList<InspectionHistory>> GetHistory(
        [FromRoute] Guid incidentId,
        IQuerySession querySession,
        CancellationToken ct
    ) =>
        querySession.Query<InspectionHistory>().Where(i => i.InspectionId == incidentId).ToListAsync(ct);
}