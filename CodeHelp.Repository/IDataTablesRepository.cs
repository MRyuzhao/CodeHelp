using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.Data.Dapper.Repository;
using CodeHelp.Domain;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.Repository
{
    public interface IDataTablesRepository: IRepositoryBase<DataTables>
    {
        Task<IList<DataTablesListViewModel>> GetAllTables();
    }
}