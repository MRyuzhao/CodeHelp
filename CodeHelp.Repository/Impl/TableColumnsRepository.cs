using CodeHelp.Data.Dapper;
using CodeHelp.Data.Dapper.Repository;
using CodeHelp.Domain;

namespace CodeHelp.Repository.Impl
{
    public class TableColumnsRepository : RepositoryBase<TableColumns>, ITableColumnsRepository
    {
        public static SqlStrings TableColumnsSql = new SqlStrings
        {
            TableName = "User",
            Add = @"",
            Update = @"",
            Delete = @"",
            Get = @"SELECT * FROM [User]",
            GetAll = @"SELECT * FROM [User]"
        };

        public TableColumnsRepository(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {
            Sql = TableColumnsSql;
        }

        protected sealed override SqlStrings Sql
        {
            get => base.Sql;
            set => base.Sql = value;
        }
    }
}