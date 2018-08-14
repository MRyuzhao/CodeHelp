using System;
using System.Data;

namespace CodeHelp.Data.Dapper
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection CreateConnection();
    }
}