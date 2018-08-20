
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.Repository;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.QueryService.Impl
{
    public class DataTablesQueryService : IDataTablesQueryService
    {
        private readonly IDataTablesRepository _dataTablesRepository;

        public DataTablesQueryService(IDataTablesRepository dataTablesRepository)
        {
            _dataTablesRepository = dataTablesRepository;
        }

        public async Task<IList<DataTablesListViewModel>> GetAll()
        {
            return await _dataTablesRepository.GetAllTables();
        }
    }
}