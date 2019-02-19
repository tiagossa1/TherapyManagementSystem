using BuddhaTerapiasAPI.Entities;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Interfaces
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}