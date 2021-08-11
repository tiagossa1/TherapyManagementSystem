using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class CivilStatusRepository : Repository<CivilStatus>, ICivilStatusRepository
    {
        public CivilStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
