using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace CodeHelp.Data.Dapper
{
    public class SqlDatabaseProxy : ISqlDatabaseProxy
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlDatabaseProxy(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        private IDbConnection CreateConnection()
        {
            try
            {
                var connection = _connectionFactory.CreateConnection();
                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Failed to create connection");
            }
        }

        public async Task<int> Add<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<int>(sql + ";SELECT CAST(SCOPE_IDENTITY() as int)", item);
            }
        }

        public async Task Update<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, item);
            }
        }

        public async Task Delete<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, item);
            }
        }

        public async Task<T> Get<T>(string sql, object param)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sql)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string sql)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string sql, object param)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(sql, param);
            }
        }
    }
}