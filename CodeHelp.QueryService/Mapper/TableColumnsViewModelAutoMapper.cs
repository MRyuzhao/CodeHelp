using AutoMapper;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Mapper
{
    public class TableColumnsViewModelAutoMapper : Profile
    {
        public TableColumnsViewModelAutoMapper()
        {
            CreateMap<TableColumnsListViewModel, TableColumnsListViewModel>()
                .ForMember(i => i.Key, o => o.MapFrom(s => s.ColumnName))
                ;
        }
    }
}