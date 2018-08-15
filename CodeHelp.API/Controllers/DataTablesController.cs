using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.QueryService;
using CodeHelp.QueryService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeHelp.API.Controllers
{
    [Route("api/dataTables")]
    public class DataTablesController : Controller
    {
        private readonly IDataTablesQueryService _queryService;

        public DataTablesController(IDataTablesQueryService queryService)
        {
            _queryService = queryService;
        }

        // GET api/dataTables
        [HttpGet]
        public async Task<IList<DataTablesListViewModel>> GetAll()
        {
            return await _queryService.GetAll();
        }
    }
}