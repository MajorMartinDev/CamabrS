// <auto-generated/>
#pragma warning disable
using Marten;
using Marten.Events.Aggregation;
using Marten.Internal.Storage;
using System;
using System.Linq;

namespace Marten.Generated.EventStore
{
    // START: SingleStreamProjectionLiveAggregation1513161052
    public class SingleStreamProjectionLiveAggregation1513161052 : Marten.Events.Aggregation.SyncLiveAggregatorBase<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Inspection.Inspection> _singleStreamProjection;

        public SingleStreamProjectionLiveAggregation1513161052(Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Inspection.Inspection> singleStreamProjection)
        {
            _singleStreamProjection = singleStreamProjection;
        }



        public override CamabrS.API.Inspection.Inspection Build(System.Collections.Generic.IReadOnlyList<Marten.Events.IEvent> events, Marten.IQuerySession session, CamabrS.API.Inspection.Inspection snapshot)
        {
            if (!events.Any()) return null;
            CamabrS.API.Inspection.Inspection inspection = null;
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


        public CamabrS.API.Inspection.Inspection Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionOpened> event_InspectionOpened1:
                    return CamabrS.API.Inspection.Inspection.Create(event_InspectionOpened1);
                    break;
            }

            return null;
        }


        public CamabrS.API.Inspection.Inspection Apply(Marten.Events.IEvent @event, CamabrS.API.Inspection.Inspection aggregate, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionClosed> event_InspectionClosed8:
                    aggregate = aggregate.Apply(event_InspectionClosed8.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionCompleted> event_InspectionCompleted11:
                    aggregate = aggregate.Apply(event_InspectionCompleted11.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionLocked> event_InspectionLocked4:
                    aggregate = aggregate.Apply(event_InspectionLocked4.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionReopened> event_InspectionReopened10:
                    aggregate = aggregate.Apply(event_InspectionReopened10.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionResultSubmitted> event_InspectionResultSubmitted6:
                    aggregate = aggregate.Apply(event_InspectionResultSubmitted6.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionReviewed> event_InspectionReviewed9:
                    aggregate = aggregate.Apply(event_InspectionReviewed9.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionSigned> event_InspectionSigned7:
                    aggregate = aggregate.Apply(event_InspectionSigned7.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionUnlocked> event_InspectionUnlocked5:
                    aggregate = aggregate.Apply(event_InspectionUnlocked5.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.SpecialistAssigned> event_SpecialistAssigned2:
                    aggregate = aggregate.Apply(event_SpecialistAssigned2.Data);
                    break;
                case Marten.Events.IEvent<CamabrS.API.Inspection.SpecialistUnassigned> event_SpecialistUnassigned3:
                    aggregate = aggregate.Apply(event_SpecialistUnassigned3.Data);
                    break;
            }

            return aggregate;
        }

    }

    // END: SingleStreamProjectionLiveAggregation1513161052
    
    
    // START: SingleStreamProjectionInlineHandler1513161052
    public class SingleStreamProjectionInlineHandler1513161052 : Marten.Events.Aggregation.AggregationRuntime<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.IDocumentStore _store;
        private readonly Marten.Events.Aggregation.IAggregateProjection _projection;
        private readonly Marten.Events.Aggregation.IEventSlicer<CamabrS.API.Inspection.Inspection, System.Guid> _slicer;
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid> _storage;
        private readonly Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Inspection.Inspection> _singleStreamProjection;

        public SingleStreamProjectionInlineHandler1513161052(Marten.IDocumentStore store, Marten.Events.Aggregation.IAggregateProjection projection, Marten.Events.Aggregation.IEventSlicer<CamabrS.API.Inspection.Inspection, System.Guid> slicer, Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid> storage, Marten.Events.Aggregation.SingleStreamProjection<CamabrS.API.Inspection.Inspection> singleStreamProjection) : base(store, projection, slicer, storage)
        {
            _store = store;
            _projection = projection;
            _slicer = slicer;
            _storage = storage;
            _singleStreamProjection = singleStreamProjection;
        }



        public override async System.Threading.Tasks.ValueTask<CamabrS.API.Inspection.Inspection> ApplyEvent(Marten.IQuerySession session, Marten.Events.Projections.EventSlice<CamabrS.API.Inspection.Inspection, System.Guid> slice, Marten.Events.IEvent evt, CamabrS.API.Inspection.Inspection aggregate, System.Threading.CancellationToken cancellationToken)
        {
            switch (evt)
            {
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionClosed> event_InspectionClosed19:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionClosed19.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionCompleted> event_InspectionCompleted22:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionCompleted22.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionLocked> event_InspectionLocked15:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionLocked15.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionOpened> event_InspectionOpened23:
                    aggregate = CamabrS.API.Inspection.Inspection.Create(event_InspectionOpened23);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionReopened> event_InspectionReopened21:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionReopened21.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionResultSubmitted> event_InspectionResultSubmitted17:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionResultSubmitted17.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionReviewed> event_InspectionReviewed20:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionReviewed20.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionSigned> event_InspectionSigned18:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionSigned18.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionUnlocked> event_InspectionUnlocked16:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_InspectionUnlocked16.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.SpecialistAssigned> event_SpecialistAssigned13:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_SpecialistAssigned13.Data);
                    return aggregate;
                case Marten.Events.IEvent<CamabrS.API.Inspection.SpecialistUnassigned> event_SpecialistUnassigned14:
                    aggregate ??= CreateDefault(evt);
                    aggregate = aggregate.Apply(event_SpecialistUnassigned14.Data);
                    return aggregate;
            }

            return aggregate;
        }


        public CamabrS.API.Inspection.Inspection Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<CamabrS.API.Inspection.InspectionOpened> event_InspectionOpened12:
                    return CamabrS.API.Inspection.Inspection.Create(event_InspectionOpened12);
                    break;
            }

            return null;
        }

    }

    // END: SingleStreamProjectionInlineHandler1513161052
    
    
}

