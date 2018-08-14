using AutoMapper;
using CodeHelp.Domain;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Mapper
{
    public class TableColumnsViewModelAutoMapper : Profile
    {
        public TableColumnsViewModelAutoMapper()
        {
            CreateMap<TableColumns, TableColumnsListViewModel>()
                ;
        }
    }
}