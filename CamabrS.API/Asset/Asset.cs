namespace CamabrS.API.Asset;

public sealed record Asset(Guid Id)
{
    public static Asset Create(AssetCreated Created) =>
        new(Created.Id);
}

public sealed record AssetCreated(Guid Id);