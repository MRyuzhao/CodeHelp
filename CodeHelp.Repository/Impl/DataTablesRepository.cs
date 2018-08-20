using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CodeHelp.Common.Exceptions;
using CodeHelp.Common.Mapper;
using CodeHelp.Common.SqlHelp;
using CodeHelp.Data.Dapper;
using CodeHelp.Data.Dapper.Repository;
using CodeHelp.Domain;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.Repository.Impl
{
    public class DataTablesRepository : RepositoryBase<DataTables>, IDataTablesRepository
    {
        private readonly ISqlDatabaseProxy _sqlDatabaseProxy;
        private readonly IMap _map;
        public static SqlStrings DataTablesSql = new SqlStrings
        {
            TableName = "",
            Add = @"",
            Update = @"",
            Delete = @"",
            Get = @"",
            GetAll = @""
        };

        public DataTablesRepository(ISqlDatabaseProxy databaseProxy,
            ISqlDatabaseProxy sqlDatabaseProxy, IMap map)
            : base(databaseProxy)
        {
            _sqlDatabaseProxy = sqlDatabaseProxy;
            _map = map;
            Sql = DataTablesSql;
        }

        protected sealed override SqlStrings Sql
        {
            get => base.Sql;
            set => base.Sql = value;
        }

        public async Task<IList<DataTablesListViewModel>> GetAllTables()
        {
            var builder = new SqlBuilder();
            var select = builder.AddTemplate(@"SELECT /**select**/ 
                        FROM sys.tables ST
                        /**leftjoin**/");
            builder.Select("ST.name TableName, SEG.value Description");
            builder.LeftJoin(@"sys.extended_properties SEG ON ST.object_id = SEG.major_id AND SEG.minor_id = 0 ");
            try
            {
                var queryResult = await _sqlDatabaseProxy.Query<DataTables>(select.RawSql);
                var result = _map.Map<List<DataTablesListViewModel>>(queryResult);
                return result;
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }
    }
}