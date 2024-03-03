// <auto-generated/>
#pragma warning disable
using CamabrS.API.Inspection.GettingHistory;
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertInspectionHistoryOperation1682837465
    public class UpsertInspectionHistoryOperation1682837465 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingHistory.InspectionHistory _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertInspectionHistoryOperation1682837465(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_inspectionhistory(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Upsert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: UpsertInspectionHistoryOperation1682837465
    
    
    // START: InsertInspectionHistoryOperation1682837465
    public class InsertInspectionHistoryOperation1682837465 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingHistory.InspectionHistory _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertInspectionHistoryOperation1682837465(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_inspectionhistory(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Insert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: InsertInspectionHistoryOperation1682837465
    
    
    // START: UpdateInspectionHistoryOperation1682837465
    public class UpdateInspectionHistoryOperation1682837465 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingHistory.InspectionHistory _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateInspectionHistoryOperation1682837465(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_inspectionhistory(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
            postprocessUpdate(reader, exceptions);
        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            await postprocessUpdateAsync(reader, exceptions, token);
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
            parameters[2].Value = document.Id;
            setVersionParameter(parameters[3]);
        }

    }

    // END: UpdateInspectionHistoryOperation1682837465
    
    
    // START: QueryOnlyInspectionHistorySelector1682837465
    public class QueryOnlyInspectionHistorySelector1682837465 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingHistory.InspectionHistory>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyInspectionHistorySelector1682837465(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingHistory.InspectionHistory Resolve(System.Data.Common.DbDataReader reader)
        {

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingHistory.InspectionHistory> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyInspectionHistorySelector1682837465
    
    
    // START: LightweightInspectionHistorySelector1682837465
    public class LightweightInspectionHistorySelector1682837465 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingHistory.InspectionHistory>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightInspectionHistorySelector1682837465(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingHistory.InspectionHistory Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingHistory.InspectionHistory> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightInspectionHistorySelector1682837465
    
    
    // START: IdentityMapInspectionHistorySelector1682837465
    public class IdentityMapInspectionHistorySelector1682837465 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingHistory.InspectionHistory>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapInspectionHistorySelector1682837465(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingHistory.InspectionHistory Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingHistory.InspectionHistory> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapInspectionHistorySelector1682837465
    
    
    // START: DirtyTrackingInspectionHistorySelector1682837465
    public class DirtyTrackingInspectionHistorySelector1682837465 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingHistory.InspectionHistory>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingInspectionHistorySelector1682837465(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingHistory.InspectionHistory Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingHistory.InspectionHistory> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingHistory.InspectionHistory document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingHistory.InspectionHistory>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingInspectionHistorySelector1682837465
    
    
    // START: QueryOnlyInspectionHistoryDocumentStorage1682837465
    public class QueryOnlyInspectionHistoryDocumentStorage1682837465 : Marten.Internal.Storage.QueryOnlyDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyInspectionHistoryDocumentStorage1682837465(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyInspectionHistorySelector1682837465(session, _document);
        }

    }

    // END: QueryOnlyInspectionHistoryDocumentStorage1682837465
    
    
    // START: LightweightInspectionHistoryDocumentStorage1682837465
    public class LightweightInspectionHistoryDocumentStorage1682837465 : Marten.Internal.Storage.LightweightDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightInspectionHistoryDocumentStorage1682837465(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightInspectionHistorySelector1682837465(session, _document);
        }

    }

    // END: LightweightInspectionHistoryDocumentStorage1682837465
    
    
    // START: IdentityMapInspectionHistoryDocumentStorage1682837465
    public class IdentityMapInspectionHistoryDocumentStorage1682837465 : Marten.Internal.Storage.IdentityMapDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapInspectionHistoryDocumentStorage1682837465(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapInspectionHistorySelector1682837465(session, _document);
        }

    }

    // END: IdentityMapInspectionHistoryDocumentStorage1682837465
    
    
    // START: DirtyTrackingInspectionHistoryDocumentStorage1682837465
    public class DirtyTrackingInspectionHistoryDocumentStorage1682837465 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingInspectionHistoryDocumentStorage1682837465(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionHistoryOperation1682837465
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingHistory.InspectionHistory document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingInspectionHistorySelector1682837465(session, _document);
        }

    }

    // END: DirtyTrackingInspectionHistoryDocumentStorage1682837465
    
    
    // START: InspectionHistoryBulkLoader1682837465
    public class InspectionHistoryBulkLoader1682837465 : Marten.Internal.CodeGeneration.BulkLoader<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid> _storage;

        public InspectionHistoryBulkLoader1682837465(Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.GettingHistory.InspectionHistory, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_inspectionhistory(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_inspectionhistory_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_inspectionhistory (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_inspectionhistory_temp.\"id\", mt_doc_inspectionhistory_temp.\"data\", mt_doc_inspectionhistory_temp.\"mt_version\", mt_doc_inspectionhistory_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_inspectionhistory_temp left join public.mt_doc_inspectionhistory on mt_doc_inspectionhistory_temp.id = public.mt_doc_inspectionhistory.id where public.mt_doc_inspectionhistory.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_inspectionhistory target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_inspectionhistory_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_inspectionhistory_temp as select * from public.mt_doc_inspectionhistory limit 0";


        public override string CreateTempTableForCopying()
        {
            return CREATE_TEMP_TABLE_FOR_COPYING_SQL;
        }


        public override string CopyNewDocumentsFromTempTable()
        {
            return COPY_NEW_DOCUMENTS_SQL;
        }


        public override string OverwriteDuplicatesFromTempTable()
        {
            return OVERWRITE_SQL;
        }


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.GettingHistory.InspectionHistory document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
        {
            await writer.WriteAsync(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar, cancellation);
            await writer.WriteAsync(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb, cancellation);
        }


        public override string MainLoaderSql()
        {
            return MAIN_LOADER_SQL;
        }


        public override string TempLoaderSql()
        {
            return TEMP_LOADER_SQL;
        }

    }

    // END: InspectionHistoryBulkLoader1682837465
    
    
    // START: InspectionHistoryProvider1682837465
    public class InspectionHistoryProvider1682837465 : Marten.Internal.Storage.DocumentProvider<CamabrS.API.Inspection.GettingHistory.InspectionHistory>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InspectionHistoryProvider1682837465(Marten.Schema.DocumentMapping mapping) : base(new InspectionHistoryBulkLoader1682837465(new QueryOnlyInspectionHistoryDocumentStorage1682837465(mapping)), new QueryOnlyInspectionHistoryDocumentStorage1682837465(mapping), new LightweightInspectionHistoryDocumentStorage1682837465(mapping), new IdentityMapInspectionHistoryDocumentStorage1682837465(mapping), new DirtyTrackingInspectionHistoryDocumentStorage1682837465(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: InspectionHistoryProvider1682837465
    
    
}

