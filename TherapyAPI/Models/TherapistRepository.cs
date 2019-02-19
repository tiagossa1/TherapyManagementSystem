using TherapyAPI.Entities;
using TherapyAPI.Repository;

namespace TherapyAPI.Models {
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository {
        public TherapistRepository (RepositoryContext RepositoryContext) : base (RepositoryContext) { }
    }
}