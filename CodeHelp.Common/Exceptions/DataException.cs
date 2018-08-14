using System;
using System.Data.SqlClient;

namespace CodeHelp.Common.Exceptions
{
    public class DataException : Exception
    {
        public DataException(Exception exception, string message) : base(message, exception)
        {

        }

        public static DataException DatabaseError(SqlException sqlException)
        {
            return new DataException(sqlException, ErrorMessage.ConnectingError);
        }

        public static DataException GeneralError(Exception exception)
        {
            return new DataException(exception, ErrorMessage.RepositoryFailure);
        }
    }
}