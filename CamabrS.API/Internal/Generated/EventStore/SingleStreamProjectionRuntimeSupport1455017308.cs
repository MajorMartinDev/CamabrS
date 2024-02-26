// <auto-generated/>
#pragma warning disable
using Marten;
using Marten.Events.Aggregation;
using Marten.Internal.Storage;
using System;
using System.Linq;

namespace Marten.Generated.EventStore
{
    // START: SingleStreamProjectionLiveAggregation1455017308
    public class SingleStreamProjectionLiveAggregation1455017308 : Marten.Events.Aggregation.SyncLiveAggregatorBase<CamabrS.API.Asset.Asset>
    {
        private readonly Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Asset.Asset> _singleStreamProjection;

        public SingleStreamProjectionLiveAggregation1455017308(Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Asset.Asset> singleStreamProjection)
        {
            _singleStreamProjection = singleStreamProjection;
        }



        public override CamabrS.API.Asset.Asset Build(System.Collections.Generic.IReadOnlyList<Marten.Events.IEvent> events, Marten.IQuerySession session, CamabrS.API.Asset.Asset snapshot)
        {
            if (!events.Any()) return null;
            CamabrS.API.Asset.Asset asset = null;
            var usedEventOnCreate = snapshot is null;
            snapshot ??= Create(events[0], session);;
            if (snapshot is null)
            {
                usedEventOnCreate = false;
                snapshot = CreateDefault(events[0]);
            }

            foreach (var @event in events.Skip(usedEventOnCreate ? 1 : 0))
            {
                snapshot = Apply(@event, snapshot, session);
            }

            return snapshot;
        }


        public CamabrS.API.Asset.Asset Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            var byteArray = new System.Byte[]{};
            var guid = new System.Guid(byteArray);
            var asset = new CamabrS.API.Asset.Asset(guid);
            switch (@event)
            {
                case Marten.Events.IEvent<CamabrS.API.Asset.AssetCreated> event_AssetCreated48:
                    asset = CamabrS.API.Asset.Asset.Create(event_AssetCreated48.Data);
                    break;
            }

            return null;
        }


        public CamabrS.API.Asset.Asset Apply(Marten.Events.IEvent @event, CamabrS.API.Asset.Asset aggregate, Marten.IQuerySession session)
        {
            return aggregate;
        }

    }

    // END: SingleStreamProjectionLiveAggregation1455017308
    
    
    // START: SingleStreamProjectionInlineHandler1455017308
    public class SingleStreamProjectionInlineHandler1455017308 : Marten.Events.Aggregation.AggregationRuntime<CamabrS.API.Asset.Asset, System.Guid>
    {
        private readonly Marten.IDocumentStore _store;
        private readonly Marten.Events.Aggregation.IAggregateProjection _projection;
        private readonly Marten.Events.Aggregation.IEventSlicer<CamabrS.API.Asset.Asset, System.Guid> _slicer;
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Asset.Asset, System.Guid> _storage;
        private readonly Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Asset.Asset> _singleStreamProjection;

        public SingleStreamProjectionInlineHandler1455017308(Marten.IDocumentStore store, Marten.Events.Aggregation.IAggregateProjection projection, Marten.Events.Aggregation.IEventSlicer<CamabrS.API.Asset.Asset, System.Guid> slicer, Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Asset.Asset, System.Guid> storage, Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Asset.Asset> singleStreamProjection) : base(store, projection, slicer, storage)
        {
            _store = store;
            _projection = projection;
            _slicer = slicer;
            _storage = storage;
            _singleStreamProjection = singleStreamProjection;
        }



        public override async System.Threading.Tasks.ValueTask<CamabrS.API.Asset.Asset> ApplyEvent(Marten.IQuerySession session, Marten.Events.Projections.EventSlice<CamabrS.API.Asset.Asset, System.Guid> slice, Marten.Events.IEvent evt, CamabrS.API.Asset.Asset aggregate, System.Threading.CancellationToken cancellationToken)
        {
            switch (evt)
            {
                case Marten.Events.IEvent<CamabrS.API.Asset.AssetCreated> event_AssetCreated50:
                    aggregate = CamabrS.API.Asset.Asset.Create(event_AssetCreated50.Data);
                    return aggregate;
            }

            return aggregate;
        }


        public CamabrS.API.Asset.Asset Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            var byteArray = new System.Byte[]{};
            var guid = new System.Guid(byteArray);
            var asset = new CamabrS.API.Asset.Asset(guid);
            switch (@event)
            {
                case Marten.Events.IEvent<CamabrS.API.Asset.AssetCreated> event_AssetCreated49:
                    asset = CamabrS.API.Asset.Asset.Create(event_AssetCreated49.Data);
                    break;
            }

            return null;
        }

    }

    // END: SingleStreamProjectionInlineHandler1455017308
    
    
}

