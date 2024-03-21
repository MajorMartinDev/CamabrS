using CamabrS.API.Asset;
using CamabrS.API.Specialist;

namespace CamabrS.IntegrationTests;

internal sealed class BaselineData : IInitialData
{
    public static Guid DefaultTestAssetId { get; } = CombGuidIdGeneration.NewGuid();

    public static Guid LockHoldingSpecialistId { get;  } = CombGuidIdGeneration.NewGuid();
    public static Guid AnotherAssignedSpecialistId { get; } = CombGuidIdGeneration.NewGuid();
    public static Guid AnotherSpecialistId { get; } = CombGuidIdGeneration.NewGuid();

    public static Guid DefaultFormId { get; } = CombGuidIdGeneration.NewGuid();    

    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        //Assets
        session.Events.StartStream<Asset>(DefaultTestAssetId, new AssetCreated(DefaultTestAssetId));

        //Specialists
        session.Events.StartStream<Specialist>(LockHoldingSpecialistId, new SpecialistCreated(LockHoldingSpecialistId));
        session.Events.StartStream<Specialist>(AnotherAssignedSpecialistId, new SpecialistCreated(AnotherAssignedSpecialistId));
        session.Events.StartStream<Specialist>(AnotherSpecialistId, new SpecialistCreated(AnotherSpecialistId));

        await session.SaveChangesAsync(cancellation);
    }
}