using System;
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
    public class TableColumnsRepository : RepositoryBase<TableColumns>, ITableColumnsRepository
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

        public TableColumnsRepository(ISqlDatabaseProxy databaseProxy,
            ISqlDatabaseProxy sqlDatabaseProxy,
            IMap map)
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

        public async Task<TableColumnsViewModel> QueryTableColumnsByTableName(string tableName)
        {
            const string querySql = @"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( /**orderby**/ ) AS RowNum,* FROM (
                        SELECT 
                        syscolumns.name ColumnName,
                        syscolumns.colid
                        FROM syscolumns 
                        JOIN systypes ON syscolumns.xtype=systypes.xtype AND systypes.name <> 'sysname' 
                        LEFT JOIN sys.extended_properties ON sys.extended_properties.minor_id = syscolumns.colid AND sys.extended_properties.major_id = syscolumns.id
                        LEFT JOIN sysobjects ON sysobjects.id =syscolumns.id 
                        LEFT JOIN syscomments ON syscolumns.cdefault=syscomments.id 
                        /**where**/ )QueryTable) AS RowConstrainedResult";
            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(querySql);
            builder.OrderBy("colid");
            if (!string.IsNullOrEmpty(tableName))
            {
                builder.Where("sysobjects.name = @tableName", new { tableName = $"{tableName}" });
            }
            try
            {
                var result = await _sqlDatabaseProxy.Query<TableColumns>(selector.RawSql, selector.Parameters);
                return _map.Map<TableColumnsViewModel>(result);
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