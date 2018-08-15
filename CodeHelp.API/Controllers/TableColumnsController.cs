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

        // GET api/tableColumns
        [HttpGet("{tableName}")]
        public async Task<TableColumnsListPaginationViewModel> Get(string tableName)
        {
            return await _queryService.Get(tableName);
        }
    }
}