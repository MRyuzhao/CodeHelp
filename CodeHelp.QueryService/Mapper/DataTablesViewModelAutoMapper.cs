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
                .ForMember(i => i.Value, o => o.MapFrom(s => s.TableName))
                .ForMember(i => i.Text, o => o.MapFrom(s => s.TableName + (string.IsNullOrEmpty(s.Description) ? "" : $"_{s.Description}")))
                ;
        }
    }
}