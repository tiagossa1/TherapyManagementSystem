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
            //var query = Context.Set<T>().AsQueryable();

            //if (includes != null)
            //{
            //    foreach (var include in includes)
            //    {
            //        if (!string.IsNullOrWhiteSpace(include))
            //            query = query.Include(include);
            //    }
            //}

            return await Context.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetById(Guid Id)
        {
            return await Context.Set<T>().FindAsync(Id).ConfigureAwait(false);
        }
        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChangesAsync();
        }

        public void Delete(Guid Id)
        {
            T entity = Context.Set<T>().Find(Id);
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
            Context.SaveChanges();
        }
    }
}