using System;
using System.Data;
using System.Transactions;
using CodeHelp.Data.Dapper;

namespace CodeHelp.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _dbContext;
        private readonly TransactionScope _transaction;

        public UnitOfWork(IDbConnectionFactory dbContext)
        {
            _dbContext = dbContext.CreateConnection();
            _transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _transaction.Complete();
        }
    }
}