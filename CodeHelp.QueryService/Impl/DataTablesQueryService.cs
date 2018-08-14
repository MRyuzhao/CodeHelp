using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.Common.Mapper;
using CodeHelp.Data.Dapper;
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

        public async Task<IList<DataTablesListViewModel>> GetAll(string tableName)
        {
            var whereSql = "";
            if (!string.IsNullOrEmpty(tableName))
            {
                whereSql += "WHERE ST.name LIKE @tableName";
            }
            var sql = @"SELECT ST.name TableName, SEG.value Description
                        FROM sys.tables ST
                        LEFT JOIN sys.extended_properties SEG ON ST.object_id = SEG.major_id AND SEG.minor_id = 0 "
                        + (!string.IsNullOrEmpty(whereSql) ? $"{whereSql}" : "");
            var queryResult = await _sqlDatabaseProxy.Query<DataTablesListViewModel>(sql, new
            {
                tableName=$"%{tableName}%"
            });
            var result = _map.Map<List<DataTablesListViewModel>>(queryResult);
            return result;
        }
    }
}