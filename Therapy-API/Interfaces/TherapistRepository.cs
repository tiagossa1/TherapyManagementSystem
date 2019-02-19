using BuddhaTerapiasAPI.Entities;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Interfaces
{
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}