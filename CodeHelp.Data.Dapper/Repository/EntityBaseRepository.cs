using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CodeHelp.Common.DomainModels;
using CodeHelp.Common.Exceptions;

namespace CodeHelp.Data.Dapper.Repository
{
    public abstract class EntityBaseRepository<T> where T : Aggregate
    {
        protected ISqlDatabaseProxy DatabaseProxy { get; set; }
        protected abstract SqlStrings Sql { get; set; }

        protected EntityBaseRepository(ISqlDatabaseProxy databaseProxy)
        {
            DatabaseProxy = databaseProxy;
        }

        public async Task<int> Add(T t)
        {
            if (t == null) throw new ArgumentNullException(typeof(T).Name);
            try
            {
                return await DatabaseProxy.Add(Sql.Add, t);
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }

        public async Task Update(T t)
        {
            if (t == null) throw new ArgumentNullException(typeof(T).Name);
            try
            {
                await DatabaseProxy.Update(Sql.Update, t);
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await DatabaseProxy.Delete(Sql.Delete, id);
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }

        public async Task<T> Get(Guid id)
        {
            try
            {
                return await DatabaseProxy.Get<T>(Sql.Get, id);
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await DatabaseProxy.GetAll<T>(Sql.GetAll);
            }
            catch (SqlException sqlException)
            {
                throw DataException.DatabaseError(sqlException);
            }
            catch (Exception exception)
            {
                throw DataException.GeneralError(exception);
            }
        }
    }
}