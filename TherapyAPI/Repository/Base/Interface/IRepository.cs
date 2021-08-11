using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TherapyAPI.Repository.Base.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(List<string> includes = null);
        Task<T> GetById(Guid Id);
        Task<int> Create(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(Guid Id);
    }
}