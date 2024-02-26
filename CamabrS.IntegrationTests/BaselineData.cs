using CamabrS.API.Asset;

namespace CamabrS.IntegrationTests;

internal sealed class BaselineData : IInitialData
{
    public static Guid DefaultTestAssetId { get; } = CombGuidIdGeneration.NewGuid();

    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        session.Events.StartStream<Asset>(DefaultTestAssetId, new AssetCreated(DefaultTestAssetId));

        await session.SaveChangesAsync(cancellation);
    }
}