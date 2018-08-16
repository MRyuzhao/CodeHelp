using CodeHelp.Common.DomainModels;
using CodeHelp.Common.Enums;

namespace CodeHelp.Domain
{
    public class TableColumns: Entity
    {
        public string TableName { get; private set; }
        public string ColumnName { get; private set; }
        public string Description { get; private set; }
        public int ColumnColid { get; private set; }
        public string ColumnType { get; private set; }
        public int ColumnLength { get; private set; }
        public string DefaultValue { get; private set; }
        public IsNull IsNull { get; private set; }
        public IsPrimaryKey IsPrimaryKey { get; private set; }
        public IsIdentity IsIdentity { get; private set; }
        public int? Scale { get; private set; }
    }
}
