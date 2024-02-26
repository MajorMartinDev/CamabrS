using Marten.Events.Aggregation;

namespace CamabrS.API.Asset.GettingDetails;

public sealed record AssetDetails(Guid Id);

public sealed class AssetDetailsProjection : SingleStreamProjection<AssetDetails>
{
    public static AssetDetails Create(AssetCreated created) =>
        new(created.Id);
}