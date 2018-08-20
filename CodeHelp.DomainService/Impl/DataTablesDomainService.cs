using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeHelp.Common.CodeModels;
using CodeHelp.Domain;
using CodeHelp.DomainService.UICommands;
using CodeHelp.Repository;
using CodeHelp.Repository.UnitOfWork;

namespace CodeHelp.DomainService.Impl
{
    public class DataTablesDomainService : IDataTablesDomainService
    {
        private readonly IDataTablesRepository _dataTablesRepository;
        private readonly ITableColumnsRepository _tableColumnsRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

        public DataTablesDomainService(IDataTablesRepository dataTablesRepository,
            IUnitOfWorkFactory unitOfWorkFactory,
            ITableColumnsRepository tableColumnsRepository)
        {
            _dataTablesRepository = dataTablesRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _tableColumnsRepository = tableColumnsRepository;
        }

        public async Task Add(AddDataTablesUiCommand entity)
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var dataTables = DataTables.Add(entity.Entity.TableName, entity.Entity.Description);
                using (var unitOnWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
                {
                    await _dataTablesRepository.Add(dataTables);
                    unitOnWork.Commit();
                }
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        public async Task Update(UpdateDataTablesUiCommand entity)
        {
            var dataTables = await _dataTablesRepository.Get(entity.Id);
            dataTables.Update(entity.Entity.TableName, entity.Entity.Description);
            using (var unitOnWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                await _dataTablesRepository.Update(dataTables);
                unitOnWork.Commit();
            }
        }

        public async Task Delete(DeleteDataTablesUiCommand entity)
        {
            using (var unitOnWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                await _dataTablesRepository.Delete(entity.Id);
                unitOnWork.Commit();
            }
        }

        public async Task BirthCodeFile(BirthDataTablesUiCommand entity)
        {
            using (var unitOnWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                var entityCodeModel = new EntityCodeModel();
                var colums = await _tableColumnsRepository.QueryTableColumnsByTableName(entity.TableName);
                var buildText = entityCodeModel.GetCodeBirthText(new CodeModel
                {
                    TableName = entity.TableName,
                    Columns = colums.ColumnNames
                });
                var bithPath = $@"{entity.BirthPath}\{entity.TableName}.cs";
               
                await System.IO.File.WriteAllTextAsync(bithPath, buildText, Encoding.UTF8);
              
                unitOnWork.Commit();
            }
        }
    }
}