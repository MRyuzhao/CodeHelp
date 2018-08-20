using System.Threading.Tasks;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.QueryService
{
    public interface ITableColumnsQueryService
    {
        Task<TableColumnsListPaginationViewModel> GetByPage(string tableName,
            int currentPage, int pageSize,string orderByPropertyName, bool isAsc);
    }
}