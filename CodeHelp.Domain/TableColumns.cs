using CodeHelp.Common.DomainModels;

namespace CodeHelp.Domain
{
    public class TableColumns: Aggregate
    {
        public string TableName { get; private set; }
        public string ColumnName { get; private set; }
        public string Description { get; private set; }
        public string ColumnType { get; private set; }
        public int ColumnLength { get; private set; }
        public string DefaultValue { get; private set; }
        public int IsNull { get; private set; }
        public int IsPrimaryKey { get; private set; }
        public int IsIdentity { get; private set; }
        public int? Scale { get; private set; }
    }
}
