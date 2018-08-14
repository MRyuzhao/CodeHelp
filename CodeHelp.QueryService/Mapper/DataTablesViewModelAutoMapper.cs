using AutoMapper;
using CodeHelp.Domain;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Mapper
{
    public class DataTablesViewModelAutoMapper : Profile
    {
        public DataTablesViewModelAutoMapper()
        {
            CreateMap<DataTables, DataTablesListViewModel>()
                .ForMember(i => i.TableName, o => o.MapFrom(s => s.TableName.ToString()))
                .ForMember(i => i.Description, o => o.MapFrom(s => s.Description.ToString()))
                ;
        }
    }
}