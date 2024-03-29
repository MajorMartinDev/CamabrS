// <auto-generated/>
#pragma warning disable
using CamabrS.API.Specialist.GettingDetails;
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
    // START: UpsertSpecialistDetailsOperation1013209401
    public class UpsertSpecialistDetailsOperation1013209401 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly CamabrS.API.Specialist.GettingDetails.SpecialistDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertSpecialistDetailsOperation1013209401(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_specialistdetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session)
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

    // END: UpsertSpecialistDetailsOperation1013209401
    
    
    // START: InsertSpecialistDetailsOperation1013209401
    public class InsertSpecialistDetailsOperation1013209401 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly CamabrS.API.Specialist.GettingDetails.SpecialistDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertSpecialistDetailsOperation1013209401(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_specialistdetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session)
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

    // END: InsertSpecialistDetailsOperation1013209401
    
    
    // START: UpdateSpecialistDetailsOperation1013209401
    public class UpdateSpecialistDetailsOperation1013209401 : Marten.Internal.Operations.StorageOperation<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly CamabrS.API.Specialist.GettingDetails.SpecialistDetails _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateSpecialistDetailsOperation1013209401(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_specialistdetails(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session)
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

    // END: UpdateSpecialistDetailsOperation1013209401
    
    
    // START: QueryOnlySpecialistDetailsSelector1013209401
    public class QueryOnlySpecialistDetailsSelector1013209401 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlySpecialistDetailsSelector1013209401(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Specialist.GettingDetails.SpecialistDetails Resolve(System.Data.Common.DbDataReader reader)
        {

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = _serializer.FromJson<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Specialist.GettingDetails.SpecialistDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlySpecialistDetailsSelector1013209401
    
    
    // START: LightweightSpecialistDetailsSelector1013209401
    public class LightweightSpecialistDetailsSelector1013209401 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightSpecialistDetailsSelector1013209401(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Specialist.GettingDetails.SpecialistDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = _serializer.FromJson<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Specialist.GettingDetails.SpecialistDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightSpecialistDetailsSelector1013209401
    
    
    // START: IdentityMapSpecialistDetailsSelector1013209401
    public class IdentityMapSpecialistDetailsSelector1013209401 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapSpecialistDetailsSelector1013209401(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Specialist.GettingDetails.SpecialistDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = _serializer.FromJson<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Specialist.GettingDetails.SpecialistDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapSpecialistDetailsSelector1013209401
    
    
    // START: DirtyTrackingSpecialistDetailsSelector1013209401
    public class DirtyTrackingSpecialistDetailsSelector1013209401 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>, Marten.Linq.Selectors.ISelector<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingSpecialistDetailsSelector1013209401(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public CamabrS.API.Specialist.GettingDetails.SpecialistDetails Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = _serializer.FromJson<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<CamabrS.API.Specialist.GettingDetails.SpecialistDetails> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            CamabrS.API.Specialist.GettingDetails.SpecialistDetails document;
            document = await _serializer.FromJsonAsync<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingSpecialistDetailsSelector1013209401
    
    
    // START: QueryOnlySpecialistDetailsDocumentStorage1013209401
    public class QueryOnlySpecialistDetailsDocumentStorage1013209401 : Marten.Internal.Storage.QueryOnlyDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlySpecialistDetailsDocumentStorage1013209401(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlySpecialistDetailsSelector1013209401(session, _document);
        }

    }

    // END: QueryOnlySpecialistDetailsDocumentStorage1013209401
    
    
    // START: LightweightSpecialistDetailsDocumentStorage1013209401
    public class LightweightSpecialistDetailsDocumentStorage1013209401 : Marten.Internal.Storage.LightweightDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightSpecialistDetailsDocumentStorage1013209401(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightSpecialistDetailsSelector1013209401(session, _document);
        }

    }

    // END: LightweightSpecialistDetailsDocumentStorage1013209401
    
    
    // START: IdentityMapSpecialistDetailsDocumentStorage1013209401
    public class IdentityMapSpecialistDetailsDocumentStorage1013209401 : Marten.Internal.Storage.IdentityMapDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapSpecialistDetailsDocumentStorage1013209401(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapSpecialistDetailsSelector1013209401(session, _document);
        }

    }

    // END: IdentityMapSpecialistDetailsDocumentStorage1013209401
    
    
    // START: DirtyTrackingSpecialistDetailsDocumentStorage1013209401
    public class DirtyTrackingSpecialistDetailsDocumentStorage1013209401 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingSpecialistDetailsDocumentStorage1013209401(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertSpecialistDetailsOperation1013209401
            (
                document, Identity(document),
                session.Versions.ForType<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override System.Guid Identity(CamabrS.API.Specialist.GettingDetails.SpecialistDetails document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingSpecialistDetailsSelector1013209401(session, _document);
        }

    }

    // END: DirtyTrackingSpecialistDetailsDocumentStorage1013209401
    
    
    // START: SpecialistDetailsBulkLoader1013209401
    public class SpecialistDetailsBulkLoader1013209401 : Marten.Internal.CodeGeneration.BulkLoader<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid> _storage;

        public SpecialistDetailsBulkLoader1013209401(Marten.Internal.Storage.IDocumentStorage<CamabrS.API.Specialist.GettingDetails.SpecialistDetails, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_specialistdetails(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_specialistdetails_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_specialistdetails (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_specialistdetails_temp.\"id\", mt_doc_specialistdetails_temp.\"data\", mt_doc_specialistdetails_temp.\"mt_version\", mt_doc_specialistdetails_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_specialistdetails_temp left join public.mt_doc_specialistdetails on mt_doc_specialistdetails_temp.id = public.mt_doc_specialistdetails.id where public.mt_doc_specialistdetails.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_specialistdetails target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_specialistdetails_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_specialistdetails_temp as select * from public.mt_doc_specialistdetails limit 0";


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


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, CamabrS.API.Specialist.GettingDetails.SpecialistDetails document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
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

    // END: SpecialistDetailsBulkLoader1013209401
    
    
    // START: SpecialistDetailsProvider1013209401
    public class SpecialistDetailsProvider1013209401 : Marten.Internal.Storage.DocumentProvider<CamabrS.API.Specialist.GettingDetails.SpecialistDetails>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public SpecialistDetailsProvider1013209401(Marten.Schema.DocumentMapping mapping) : base(new SpecialistDetailsBulkLoader1013209401(new QueryOnlySpecialistDetailsDocumentStorage1013209401(mapping)), new QueryOnlySpecialistDetailsDocumentStorage1013209401(mapping), new LightweightSpecialistDetailsDocumentStorage1013209401(mapping), new IdentityMapSpecialistDetailsDocumentStorage1013209401(mapping), new DirtyTrackingSpecialistDetailsDocumentStorage1013209401(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: SpecialistDetailsProvider1013209401
    
    
}

