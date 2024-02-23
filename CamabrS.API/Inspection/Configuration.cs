using CamabrS.API.Inspection.GettingDetails;
using CamabrS.API.Inspection.GettingHistory;
using Marten;
using Marten.Events.Projections;

namespace CamabrS.API.Inspection;

public static class Configuration
{
    public static StoreOptions ConfigureInspections(this StoreOptions options)
    {
        options.Projections.LiveStreamAggregation<Inspection>();
        options.Projections.Add<InspectionHistoryTransformation>(ProjectionLifecycle.Inline);
        options.Projections.Add<InspectionDetailsProjection>(ProjectionLifecycle.Inline);

        return options;
    }
}
