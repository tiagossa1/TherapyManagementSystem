using BuddhaTerapiasAPI.Entities;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Interfaces
{
    public class DevRepository : Repository<Dev>, IDevRepository
    {
        public DevRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}