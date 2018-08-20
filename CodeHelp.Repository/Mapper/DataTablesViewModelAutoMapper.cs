using AutoMapper;
using CodeHelp.Domain;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.Repository.Mapper
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