using System.Threading.Tasks;
using CodeHelp.QueryService;
using CodeHelp.QueryService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeHelp.API.Controllers
{
    [Route("api/tableColumns")]
    public class TableColumnsController : Controller
    {
        private readonly ITableColumnsQueryService _queryService;

        public TableColumnsController(ITableColumnsQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/tableColumns/pagination
        [Route("pagination")]
        [HttpGet]
        public async Task<TableColumnsListPaginationViewModel> GetByPage(string tableName,
            int currentPage, int pageSize, string orderByPropertyName, bool isAsc)
        {
            return await _queryService.GetByPage(tableName, currentPage, pageSize, orderByPropertyName, isAsc);
        }
    }
}