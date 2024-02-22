using Marten.Events;
using Marten.Events.Projections;
using static CamabrS.API.Inspection.Inspection;


namespace CamabrS.API.Inspection.GetInspectionHistory;

public sealed record InspectionHistory
{
}

public sealed class InspectionHistoryTransformation : EventProjection 
{ 
    public InspectionHistory Transform(IEvent<TryAssignSpecialist> input)
    {
        return new InspectionHistory();
    }
}