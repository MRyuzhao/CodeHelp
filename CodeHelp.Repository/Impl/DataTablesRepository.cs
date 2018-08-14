using CodeHelp.Data.Dapper;
using CodeHelp.Data.Dapper.Repository;
using CodeHelp.Domain;

namespace CodeHelp.Repository.Impl
{
    public class DataTablesRepository : RepositoryBase<DataTables>, IDataTablesRepository
    {
        public static SqlStrings DataTablesSql = new SqlStrings
        {
            TableName = "",
            Add = @"",
            Update = @"",
            Delete = @"",
            Get = @"",
            GetAll = @""
        };

        public DataTablesRepository(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {
            Sql = DataTablesSql;
        }

        protected sealed override SqlStrings Sql
        {
            get => base.Sql;
            set => base.Sql = value;
        }
    }
}