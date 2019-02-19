using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuddhaTerapiasAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuddhaTerapias_API.Interfaces
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected RepositoryContext repositoryContext { get; set; }
        public Repository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.repositoryContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            return await this.repositoryContext.Set<T>().FindAsync(Id);
        }

        public async Task Create(T entity)
        {
            await this.repositoryContext.Set<T>().AddAsync(entity);
            await this.repositoryContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            this.repositoryContext.Set<T>().Update(entity);
            await this.repositoryContext.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            var entity = await GetById(Id);
            this.repositoryContext.Set<T>().Remove(entity);
            await this.repositoryContext.SaveChangesAsync();
        }
    }
}