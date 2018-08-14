using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHelp.Common.DomainModels;

namespace CodeHelp.Data.Dapper.Repository
{
    public interface IRepositoryBase<T> where T : Aggregate
    {
        Task<int> Add(T model);

        Task Update(T model);

        Task Delete(Guid id);

        Task<T> Get(Guid id);

        Task<IEnumerable<T>> GetAll();
    }
}