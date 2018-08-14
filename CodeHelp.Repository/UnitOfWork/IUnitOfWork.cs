using System;

namespace CodeHelp.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}