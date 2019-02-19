using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuddhaTerapias_API.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid Id);
    }
}