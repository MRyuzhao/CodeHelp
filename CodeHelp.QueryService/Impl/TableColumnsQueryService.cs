using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CodeHelp.Common;
using CodeHelp.Common.Exceptions;
using CodeHelp.Common.Mapper;
using CodeHelp.Common.SqlHelp;
using CodeHelp.Data.Dapper;
using CodeHelp.Domain;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Impl
{
    public class TableColumnsQueryService : ITableColumnsQueryService
    {
        private readonly ISqlDatabaseProxy _sqlDatabaseProxy;
        private readonly IMap _map;

        public TableColumnsQueryService(ISqlDatabaseProxy sqlDatabaseProxy,
            IMap map)
        {
            _sqlDatabaseProxy = sqlDatabaseProxy;
            _map = map;
        }

        public async Task<TableColumnsListPaginationViewModel> GetByPage(string tableName, int currentPage, int pageSize, string orderByPropertyName, bool isAsc)
        {
            const string querySql = @"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( /**orderby**/ ) AS RowNum,* FROM (
                        SELECT 
                        sysobjects.name TableName,
                        syscolumns.colid ColumnColid,
                        syscolumns.name ColumnName,
                        syscolumns.length ColumnLength,
                        systypes.name ColumnType,
                        sys.extended_properties.value [Description],
                        syscolumns.isnullable [IsNull],
                        syscomments.text DefaultValue,
                        (CASE WHEN EXISTS(SELECT 1 FROM sysobjects
                        JOIN sysindexes ON sysindexes.name = sysobjects.name  
                        JOIN sysindexkeys ON sysindexes.id = sysindexkeys.id AND sysindexes.indid = sysindexkeys.indid 
                        WHERE xtype='PK' AND parent_obj = syscolumns.id    
                        AND sysindexkeys.colid = syscolumns.colid) THEN 1 ELSE 0 END) AS [IsPrimaryKey],
                        (Case syscolumns.status WHEN 128 THEN 1 ELSE 0 END) AS [IsIdentity],
                        syscolumns.scale [Scale]
                        FROM syscolumns 
                        JOIN systypes ON syscolumns.xtype=systypes.xtype AND systypes.name <> 'sysname' 
                        LEFT JOIN sys.extended_properties ON sys.extended_properties.minor_id = syscolumns.colid AND sys.extended_properties.major_id = syscolumns.id
                        LEFT JOIN sysobjects ON sysobjects.id =syscolumns.id 
                        LEFT JOIN syscomments ON syscolumns.cdefault=syscomments.id 
                        /**where**/ )QueryTable) AS RowConstrainedResult WHERE RowNum >= @StartNo AND RowNum <= @EndNo;";
            const string countSql = @"SELECT COUNT(1) FROM(
                        SELECT 
                        sysobjects.name TableName,
                        syscolumns.colid ColumnColid,
                        syscolumns.name ColumnName,
                        syscolumns.length ColumnLength,
                        systypes.name ColumnType,
                        sys.extended_properties.value [Description],
                        syscolumns.isnullable [IsNull],
                        syscomments.text DefaultValue,
                        (CASE WHEN EXISTS(SELECT 1 FROM sysobjects
                        JOIN sysindexes ON sysindexes.name = sysobjects.name  
                        JOIN sysindexkeys ON sysindexes.id = sysindexkeys.id AND sysindexes.indid = sysindexkeys.indid 
                        WHERE xtype='PK' AND parent_obj = syscolumns.id    
                        AND sysindexkeys.colid = syscolumns.colid) THEN 1 ELSE 0 END) AS [IsPrimaryKey],
                        (Case syscolumns.status WHEN 128 THEN 1 ELSE 0 END) AS [IsIdentity],
                        syscolumns.scale [Scale]
                        FROM syscolumns   
                        JOIN systypes ON syscolumns.xtype=systypes.xtype AND systypes.name <> 'sysname' 
                        LEFT JOIN sys.extended_properties ON sys.extended_properties.minor_id = syscolumns.colid AND sys.extended_properties.major_id = syscolumns.id
                        LEFT JOIN sysobjects ON sysobjects.id =syscolumns.id 
                        LEFT JOIN syscomments ON syscolumns.cdefault=syscomments.id 
                        /**where**/ ) AS RowConstrainedResult";
          
            var items = new List<TableColumns>();
            var number = 0;
            var queryPaginationParams =
                new QueryPaginationParams(currentPage, pageSize, orderByPropertyName, isAsc);
            var builder = new SqlBuilder();
            var count = builder.AddTemplate(countSql);
            var selector = builder.AddTemplate(querySql,
                new
                {
                    queryPaginationParams.StartNo,
                    queryPaginationParams.EndNo
                });
            builder.OrderBy($"{queryPaginationParams.OrderByPropertyName} {queryPaginationParams.IsAsc}");
            if (!string.IsNullOrEmpty(tableName))
            {
                builder.Where("sysobjects.name = @tableName", new { tableName = $"{tableName}" });
            }
            try
            {
                await _sqlDatabaseProxy.QueryMulti(selector.RawSql + count.RawSql, selector.Parameters,
                reader =>
                {
                    items = reader.Read<TableColumns>().ToList();
                    number = reader.Read<int>().Single();
                    return true;
                });
                var results = _map.Map<List<TableColumnsListViewModel>>(items);
                return new TableColumnsListPaginationViewModel(results, currentPage, pageSize, number);
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