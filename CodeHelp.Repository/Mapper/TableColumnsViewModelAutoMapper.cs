using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeHelp.Common;
using CodeHelp.Domain;
using CodeHelp.Repository.ViewModels;

namespace CodeHelp.Repository.Mapper
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

            CreateMap<IEnumerable<TableColumns>, TableColumnsViewModel>()
                .ForMember(i => i.ColumnNames, o => o.MapFrom(s => s.Select(x=>x.ColumnName).ToList()))
                ;
        }
    }
}