using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CodeHelp.Common.Exceptions;
using CodeHelp.Common.Mapper;
using CodeHelp.Data.Dapper;
using CodeHelp.Domain;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Impl
{
    public class DataTablesQueryService : IDataTablesQueryService
    {
        private readonly ISqlDatabaseProxy _sqlDatabaseProxy;
        private readonly IMap _map;

        public DataTablesQueryService(ISqlDatabaseProxy sqlDatabaseProxy, 
            IMap map)
        {
            _sqlDatabaseProxy = sqlDatabaseProxy;
            _map = map;
        }

        public async Task<IList<DataTablesListViewModel>> GetAll()
        {
            const string sql = @"SELECT ST.name TableName, SEG.value Description
                        FROM sys.tables ST
                        LEFT JOIN sys.extended_properties SEG ON ST.object_id = SEG.major_id AND SEG.minor_id = 0 ";
            try
            {
                var queryResult = await _sqlDatabaseProxy.Query<DataTables>(sql);
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