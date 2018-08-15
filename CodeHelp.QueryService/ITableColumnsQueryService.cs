using System.Threading.Tasks;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService
{
    public interface ITableColumnsQueryService
    {
        Task<TableColumnsListPaginationViewModel> Get(string tableName);
    }
}