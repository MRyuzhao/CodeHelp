using CodeHelp.Common.DomainModels;

namespace CodeHelp.Data.Dapper.Repository
{
    public class RepositoryBase<T> : EntityBaseRepository<T>, IRepositoryBase<T> where T : Aggregate
    {
        protected RepositoryBase(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {

        }

        protected override SqlStrings Sql { get; set; }
    }
}