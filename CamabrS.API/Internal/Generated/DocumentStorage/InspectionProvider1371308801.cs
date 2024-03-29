// <auto-generated/>
#pragma warning disable
using CamabrS.API.Inspection;
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
    // START: UpsertInspectionOperation1371308801
    public class UpsertInspectionOperation1371308801 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly CamabrS.API.Inspection.Inspection _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertInspectionOperation1371308801(CamabrS.API.Inspection.Inspection document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_inspection(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session)
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

    // END: UpsertInspectionOperation1371308801
    
    
    // START: InsertInspectionOperation1371308801
    public class InsertInspectionOperation1371308801 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly CamabrS.API.Inspection.Inspection _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertInspectionOperation1371308801(CamabrS.API.Inspection.Inspection document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_inspection(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session)
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

    // END: InsertInspectionOperation1371308801
    
    
    // START: UpdateInspectionOperation1371308801
    public class UpdateInspectionOperation1371308801 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly CamabrS.API.Inspection.Inspection _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateInspectionOperation1371308801(CamabrS.API.Inspection.Inspection document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_inspection(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session)
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

    // END: UpdateInspectionOperation1371308801
    
    
    // START: QueryOnlyInspectionSelector1371308801
    public class QueryOnlyInspectionSelector1371308801 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyInspectionSelector1371308801(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.Inspection Resolve(System.Data.Common.DbDataReader reader)
        {

            CamabrS.API.Inspection.Inspection document;
            document = _serializer.FromJson<CamabrS.API.Inspection.Inspection>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.Inspection> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            CamabrS.API.Inspection.Inspection document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.Inspection>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyInspectionSelector1371308801
    
    
    // START: LightweightInspectionSelector1371308801
    public class LightweightInspectionSelector1371308801 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<CamabrS.API.Inspection.Inspection, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightInspectionSelector1371308801(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.Inspection Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            CamabrS.API.Inspection.Inspection document;
            document = _serializer.FromJson<CamabrS.API.Inspection.Inspection>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.Inspection> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            CamabrS.API.Inspection.Inspection document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.Inspection>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightInspectionSelector1371308801
    
    
    // START: IdentityMapInspectionSelector1371308801
    public class IdentityMapInspectionSelector1371308801 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<CamabrS.API.Inspection.Inspection, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapInspectionSelector1371308801(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.Inspection Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.Inspection document;
            document = _serializer.FromJson<CamabrS.API.Inspection.Inspection>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.Inspection> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.Inspection document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.Inspection>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapInspectionSelector1371308801
    
    
    // START: DirtyTrackingInspectionSelector1371308801
    public class DirtyTrackingInspectionSelector1371308801 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<CamabrS.API.Inspection.Inspection, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingInspectionSelector1371308801(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.Inspection Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.Inspection document;
            document = _serializer.FromJson<CamabrS.API.Inspection.Inspection>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.Inspection> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.Inspection document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.Inspection>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingInspectionSelector1371308801
    
    
    // START: QueryOnlyInspectionDocumentStorage1371308801
    public class QueryOnlyInspectionDocumentStorage1371308801 : Marten.Internal.Storage.QueryOnlyDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyInspectionDocumentStorage1371308801(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.Inspection document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.Inspection document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyInspectionSelector1371308801(session, _document);
        }

    }

    // END: QueryOnlyInspectionDocumentStorage1371308801
    
    
    // START: LightweightInspectionDocumentStorage1371308801
    public class LightweightInspectionDocumentStorage1371308801 : Marten.Internal.Storage.LightweightDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightInspectionDocumentStorage1371308801(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.Inspection document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.Inspection document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightInspectionSelector1371308801(session, _document);
        }

    }

    // END: LightweightInspectionDocumentStorage1371308801
    
    
    // START: IdentityMapInspectionDocumentStorage1371308801
    public class IdentityMapInspectionDocumentStorage1371308801 : Marten.Internal.Storage.IdentityMapDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapInspectionDocumentStorage1371308801(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.Inspection document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.Inspection document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapInspectionSelector1371308801(session, _document);
        }

    }

    // END: IdentityMapInspectionDocumentStorage1371308801
    
    
    // START: DirtyTrackingInspectionDocumentStorage1371308801
    public class DirtyTrackingInspectionDocumentStorage1371308801 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingInspectionDocumentStorage1371308801(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.Inspection document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionOperation1371308801
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.Inspection, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.Inspection document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.Inspection document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingInspectionSelector1371308801(session, _document);
        }

    }

    // END: DirtyTrackingInspectionDocumentStorage1371308801
    
    
    // START: InspectionBulkLoader1371308801
    public class InspectionBulkLoader1371308801 : Marten.Internal.CodeGeneration.BulkLoader<CamabrS.API.Inspection.Inspection, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid> _storage;

        public InspectionBulkLoader1371308801(Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.Inspection, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_inspection(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_inspection_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_inspection (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_inspection_temp.\"id\", mt_doc_inspection_temp.\"data\", mt_doc_inspection_temp.\"mt_version\", mt_doc_inspection_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_inspection_temp left join public.mt_doc_inspection on mt_doc_inspection_temp.id = public.mt_doc_inspection.id where public.mt_doc_inspection.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_inspection target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_inspection_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_inspection_temp as select * from public.mt_doc_inspection limit 0";


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


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.Inspection document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.Inspection document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
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

    // END: InspectionBulkLoader1371308801
    
    
    // START: InspectionProvider1371308801
    public class InspectionProvider1371308801 : Marten.Internal.Storage.DocumentProvider<CamabrS.API.Inspection.Inspection>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InspectionProvider1371308801(Marten.Schema.DocumentMapping mapping) : base(new InspectionBulkLoader1371308801(new QueryOnlyInspectionDocumentStorage1371308801(mapping)), new QueryOnlyInspectionDocumentStorage1371308801(mapping), new LightweightInspectionDocumentStorage1371308801(mapping), new IdentityMapInspectionDocumentStorage1371308801(mapping), new DirtyTrackingInspectionDocumentStorage1371308801(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: InspectionProvider1371308801
    
    
}

