using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CodeHelp.Data.Dapper
{
    public interface ISqlDatabaseProxy
    {
        Task<int> Add<T>(string sql, T item);

        Task Update<T>(string sql, T item);

        Task Delete<T>(string sql, T item);

        Task<T> Get<T>(string sql, object param);

        Task<IEnumerable<T>> Query<T>(string sql);

        Task<IEnumerable<T>> Query<T>(string sql, object param);

        Task<T> QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill);
    }
}