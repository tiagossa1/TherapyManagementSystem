using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TherapyAPI.Repository.Base.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(List<string> includes = null);
        Task<T> GetById(Guid Id);
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid Id);
    }
}