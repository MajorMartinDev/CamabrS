// <auto-generated/>
#pragma warning disable
using CamabrS.API.Inspection.GettingDetails;
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
    // START: UpsertInspectionDetailsOperation1306443353
    public class UpsertInspectionDetailsOperation1306443353 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingDetails.InspectionDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertInspectionDetailsOperation1306443353(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_inspectiondetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session)
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

    // END: UpsertInspectionDetailsOperation1306443353
    
    
    // START: InsertInspectionDetailsOperation1306443353
    public class InsertInspectionDetailsOperation1306443353 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingDetails.InspectionDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertInspectionDetailsOperation1306443353(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_inspectiondetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session)
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

    // END: InsertInspectionDetailsOperation1306443353
    
    
    // START: UpdateInspectionDetailsOperation1306443353
    public class UpdateInspectionDetailsOperation1306443353 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly CamabrS.API.Inspection.GettingDetails.InspectionDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateInspectionDetailsOperation1306443353(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_inspectiondetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session)
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

    // END: UpdateInspectionDetailsOperation1306443353
    
    
    // START: QueryOnlyInspectionDetailsSelector1306443353
    public class QueryOnlyInspectionDetailsSelector1306443353 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingDetails.InspectionDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyInspectionDetailsSelector1306443353(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingDetails.InspectionDetails Resolve(System.Data.Common.DbDataReader reader)
        {

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingDetails.InspectionDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyInspectionDetailsSelector1306443353
    
    
    // START: LightweightInspectionDetailsSelector1306443353
    public class LightweightInspectionDetailsSelector1306443353 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingDetails.InspectionDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightInspectionDetailsSelector1306443353(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingDetails.InspectionDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingDetails.InspectionDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightInspectionDetailsSelector1306443353
    
    
    // START: IdentityMapInspectionDetailsSelector1306443353
    public class IdentityMapInspectionDetailsSelector1306443353 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingDetails.InspectionDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapInspectionDetailsSelector1306443353(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingDetails.InspectionDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingDetails.InspectionDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapInspectionDetailsSelector1306443353
    
    
    // START: DirtyTrackingInspectionDetailsSelector1306443353
    public class DirtyTrackingInspectionDetailsSelector1306443353 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Inspection.GettingDetails.InspectionDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingInspectionDetailsSelector1306443353(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Inspection.GettingDetails.InspectionDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = _serializer.FromJson<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Inspection.GettingDetails.InspectionDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Inspection.GettingDetails.InspectionDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Inspection.GettingDetails.InspectionDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingInspectionDetailsSelector1306443353
    
    
    // START: QueryOnlyInspectionDetailsDocumentStorage1306443353
    public class QueryOnlyInspectionDetailsDocumentStorage1306443353 : Marten.Internal.Storage.QueryOnlyDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyInspectionDetailsDocumentStorage1306443353(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyInspectionDetailsSelector1306443353(session, _document);
        }

    }

    // END: QueryOnlyInspectionDetailsDocumentStorage1306443353
    
    
    // START: LightweightInspectionDetailsDocumentStorage1306443353
    public class LightweightInspectionDetailsDocumentStorage1306443353 : Marten.Internal.Storage.LightweightDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightInspectionDetailsDocumentStorage1306443353(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightInspectionDetailsSelector1306443353(session, _document);
        }

    }

    // END: LightweightInspectionDetailsDocumentStorage1306443353
    
    
    // START: IdentityMapInspectionDetailsDocumentStorage1306443353
    public class IdentityMapInspectionDetailsDocumentStorage1306443353 : Marten.Internal.Storage.IdentityMapDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapInspectionDetailsDocumentStorage1306443353(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapInspectionDetailsSelector1306443353(session, _document);
        }

    }

    // END: IdentityMapInspectionDetailsDocumentStorage1306443353
    
    
    // START: DirtyTrackingInspectionDetailsDocumentStorage1306443353
    public class DirtyTrackingInspectionDetailsDocumentStorage1306443353 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingInspectionDetailsDocumentStorage1306443353(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertInspectionDetailsOperation1306443353
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Inspection.GettingDetails.InspectionDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingInspectionDetailsSelector1306443353(session, _document);
        }

    }

    // END: DirtyTrackingInspectionDetailsDocumentStorage1306443353
    
    
    // START: InspectionDetailsBulkLoader1306443353
    public class InspectionDetailsBulkLoader1306443353 : Marten.Internal.CodeGeneration.BulkLoader<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid> _storage;

        public InspectionDetailsBulkLoader1306443353(Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Inspection.GettingDetails.InspectionDetails, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_inspectiondetails(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_inspectiondetails_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_inspectiondetails (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_inspectiondetails_temp.\"id\", mt_doc_inspectiondetails_temp.\"data\", mt_doc_inspectiondetails_temp.\"mt_version\", mt_doc_inspectiondetails_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_inspectiondetails_temp left join public.mt_doc_inspectiondetails on mt_doc_inspectiondetails_temp.id = public.mt_doc_inspectiondetails.id where public.mt_doc_inspectiondetails.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_inspectiondetails target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_inspectiondetails_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_inspectiondetails_temp as select * from public.mt_doc_inspectiondetails limit 0";


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


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Inspection.GettingDetails.InspectionDetails document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
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

    // END: InspectionDetailsBulkLoader1306443353
    
    
    // START: InspectionDetailsProvider1306443353
    public class InspectionDetailsProvider1306443353 : Marten.Internal.Storage.DocumentProvider<CamabrS.API.Inspection.GettingDetails.InspectionDetails>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InspectionDetailsProvider1306443353(Marten.Schema.DocumentMapping mapping) : base(new InspectionDetailsBulkLoader1306443353(new QueryOnlyInspectionDetailsDocumentStorage1306443353(mapping)), new QueryOnlyInspectionDetailsDocumentStorage1306443353(mapping), new LightweightInspectionDetailsDocumentStorage1306443353(mapping), new IdentityMapInspectionDetailsDocumentStorage1306443353(mapping), new DirtyTrackingInspectionDetailsDocumentStorage1306443353(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: InspectionDetailsProvider1306443353
    
    
}

