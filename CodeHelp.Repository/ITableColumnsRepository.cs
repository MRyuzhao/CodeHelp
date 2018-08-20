using System.Threading.Tasks;
using CodeHelp.Data.Dapper.Repository;
using CodeHelp.Domain;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.Repository
{
    public interface ITableColumnsRepository : IRepositoryBase<TableColumns>
    {
        Task<TableColumnsViewModel> QueryTableColumnsByTableName(string tableName);
    }
}