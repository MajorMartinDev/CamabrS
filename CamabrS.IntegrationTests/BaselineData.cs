using CamabrS.API.Asset;
using CamabrS.API.Specialist;

namespace CamabrS.IntegrationTests;

internal sealed class BaselineData : IInitialData
{
    public static Guid DefaultTestAssetId { get; } = CombGuidIdGeneration.NewGuid();

    public static Guid LockHoldingSpecialist { get;  } = CombGuidIdGeneration.NewGuid();
    public static Guid AnotherAssignedSpecialist { get; } = CombGuidIdGeneration.NewGuid();
    public static Guid AnotherSpecilaist { get; } = CombGuidIdGeneration.NewGuid();

    public static Guid DefaultFormId { get; } = CombGuidIdGeneration.NewGuid();
    

    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        //Assets
        session.Events.StartStream<Asset>(DefaultTestAssetId, new AssetCreated(DefaultTestAssetId));

        //Specialists
        session.Events.StartStream<Specialist>(LockHoldingSpecialist, new SpecialistCreated(LockHoldingSpecialist));
        session.Events.StartStream<Specialist>(AnotherAssignedSpecialist, new SpecialistCreated(AnotherAssignedSpecialist));
        session.Events.StartStream<Specialist>(AnotherSpecilaist, new SpecialistCreated(AnotherSpecilaist));

        await session.SaveChangesAsync(cancellation);
    }
}