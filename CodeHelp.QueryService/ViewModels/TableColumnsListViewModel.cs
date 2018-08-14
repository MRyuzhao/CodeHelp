namespace CodeHelp.QueryService.ViewModels
{
    public class TableColumnsListViewModel
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Description { get; set; }
        public string ColumnType { get; set; }
        public int ColumnLength { get; set; }
        public string DefaultValue { get; set; }
        public int IsNull { get; set; }
        public int IsPrimaryKey { get; set; }
        public int IsIdentity { get; set; }
        public int? Scale { get; set; }
    }
}
