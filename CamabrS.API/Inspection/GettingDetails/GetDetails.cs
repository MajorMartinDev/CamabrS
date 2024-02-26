using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Http;

namespace CamabrS.API.Inspection.GettingDetails;

public static class GetDetailsEndpoints
{
    [WolverineGet("/api/inspections/{inspectionId:guid}")]
    public static Task GetInspection([FromRoute] Guid inspectionId, IQuerySession querySession, HttpContext context)
        => querySession.Json.WriteById<InspectionDetails>(inspectionId, context);
}