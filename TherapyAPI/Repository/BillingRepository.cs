using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class BillingRepository : Repository<Billing>, IBillingRepository
    {
        public BillingRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
        }
    }
}