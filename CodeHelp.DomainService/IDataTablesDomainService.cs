using System.Threading.Tasks;
using CodeHelp.DomainService.UICommands;

namespace CodeHelp.DomainService
{
    public interface IDataTablesDomainService
    {
        Task Add(AddDataTablesUiCommand entity);
        Task Update(UpdateDataTablesUiCommand entity);
        Task Delete(DeleteDataTablesUiCommand entity);
    }
}