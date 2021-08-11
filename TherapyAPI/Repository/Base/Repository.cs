using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TherapyAPI.Context;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected RepositoryContext Context { get; }
        public Repository(RepositoryContext repositoryContext)
        {
            Context = repositoryContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(List<string> includes)
        {
            return await Context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetById(Guid Id)
        {
            return await Context.Set<T>().FindAsync(Id).ConfigureAwait(false);
        }
        public Task<int> Create(T entity)
        {
            Context.Set<T>().Add(entity);
            return Context.SaveChangesAsync();
        }

        public Task<int> Delete(Guid Id)
        {
            T entity = Context.Set<T>().Find(Id);
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public Task<int> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return Context.SaveChangesAsync();
        }
    }
}