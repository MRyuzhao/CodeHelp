﻿using System.Collections.Generic;
using CodeHelp.Common;

namespace CodeHelp.Repository.ViewModels
{
    public class TableColumnsListViewModel
    {
        public string Key { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Description { get; set; }
        public int ColumnColid { get; set; }
        public string ColumnType { get; set; }
        public int ColumnLength { get; set; }
        public string DefaultValue { get; set; }
        public string IsNull { get; set; }
        public string IsPrimaryKey { get; set; }
        public string IsIdentity { get; set; }
        public string Scale { get; set; }
    }

    public class TableColumnsListPaginationViewModel : PaginationViewModel<TableColumnsListViewModel>
    {
        public TableColumnsListPaginationViewModel(IList<TableColumnsListViewModel> list,
            int currentPage, int pageSize, int total) : base(list, currentPage, pageSize, total)
        {
        }
    }
}
