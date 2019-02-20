using TherapyAPI.Entities;
using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository
{
    public class BillingRepository : Repository<Billing>, IBillingRepository
    {
        public BillingRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
        }
    }
}