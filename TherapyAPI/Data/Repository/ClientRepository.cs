using TherapyAPI.Entities;
using TherapyAPI.Repository;

namespace TherapyAPI.Models
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext RepositoryContext) : base(RepositoryContext) { }
    }
}