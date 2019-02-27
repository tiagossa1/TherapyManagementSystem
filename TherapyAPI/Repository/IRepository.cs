using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TherapyAPI.Repository {
    public interface IRepository<T> {
        Task<IEnumerable<T>> GetAllAsync ();
        Task<T> GetById (Guid Id);
        void Create (T entity);
        void Update (T entity);
        void Delete (Guid Id);
    }
}