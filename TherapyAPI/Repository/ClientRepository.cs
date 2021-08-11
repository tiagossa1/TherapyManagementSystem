using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext RepositoryContext) : base(RepositoryContext) { }
    }
}