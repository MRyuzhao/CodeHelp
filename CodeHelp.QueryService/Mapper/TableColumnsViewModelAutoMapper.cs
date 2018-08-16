using AutoMapper;
using CodeHelp.Common;
using CodeHelp.Domain;
using CodeHelp.QueryService.ViewModels;

namespace CodeHelp.QueryService.Mapper
{
    public class TableColumnsViewModelAutoMapper : Profile
    {
        public TableColumnsViewModelAutoMapper()
        {
            CreateMap<TableColumns, TableColumnsListViewModel>()
                .ForMember(i => i.Key, o => o.MapFrom(s => s.Id.ToUpperString()))
                .ForMember(i => i.Scale, o => o.MapFrom(s => s.Scale.ToString()))
                .ForMember(i => i.IsNull, o => o.MapFrom(s => s.IsNull.IsNullDescription()))
                .ForMember(i => i.IsPrimaryKey, o => o.MapFrom(s => s.IsPrimaryKey.IsPrimaryKeyDescription()))
                .ForMember(i => i.IsIdentity, o => o.MapFrom(s => s.IsIdentity.IsIdentityDescription()))
                ;
        }
    }
}