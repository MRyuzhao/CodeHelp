namespace CodeHelp.Common.Exceptions
{
    public class ErrorMessage
    {
        #region DataErrorMessage
        public const string ConnectingError = "Error connecting to the database";
        public const string RepositoryFailure = "General failure in repository";
        #endregion

        #region DomainErrorMessage
        public const string TableNameIsNull = "表面不能为空";
        public const string DescriptionIsNull = "描述不能为空";
        #endregion
    }
}