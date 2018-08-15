using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService
{
    public interface IDataTablesQueryService
    {
        Task<IList<DataTablesListViewModel>> GetAll();
    }
}
