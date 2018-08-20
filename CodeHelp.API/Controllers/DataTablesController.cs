using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.DomainService;
using CodeHelp.DomainService.UICommands;
using CodeHelp.QueryService;
using CodeHelp.QueryService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeHelp.API.Controllers
{
    [Route("api/dataTables")]
    public class DataTablesController : Controller
    {
        private readonly IDataTablesQueryService _queryService;
        private readonly IDataTablesDomainService _domainService;

        public DataTablesController(IDataTablesQueryService queryService, 
            IDataTablesDomainService domainService)
        {
            _queryService = queryService;
            _domainService = domainService;
        }

        // GET api/dataTables
        [HttpGet]
        public async Task<IList<DataTablesListViewModel>> GetAll()
        {
            return await _queryService.GetAll();
        }

        // GET api/dataTables
        [HttpPost]
        public async Task Post([FromBody]BirthDataTablesUiCommand command)
        {
            await _domainService.BirthCodeFile(command);
        }
    }
}