using CodeHelp.Common.DomainModels;
using CodeHelp.Common.Exceptions;

namespace CodeHelp.Domain
{
    public class DataTables : Aggregate
    {
        public string TableName { get; private set; }
        public string Description { get; private set; }

        public static DataTables Add(string tableName, string description)
        {
            ValidateFields(tableName, description);

            return new DataTables
            {
                TableName = tableName,
                Description = description
            };
        }

        public DataTables Update(string tableName, string description)
        {
            ValidateFields(tableName, description);

            TableName = tableName;
            Description = description;
            return this;
        }

        private static void ValidateFields(string tableName, string description)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new DomainException(ErrorMessage.TableNameIsNull);
            }
            if (string.IsNullOrEmpty(description))
            {
                throw new DomainException(ErrorMessage.DescriptionIsNull);
            }
        }
    }
}
