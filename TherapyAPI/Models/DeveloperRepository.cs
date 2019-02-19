using TherapyAPI.Entities;
using TherapyAPI.Repository;

namespace TherapyAPI.Models {
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository {
        public DeveloperRepository (RepositoryContext RepositoryContext) : base (RepositoryContext) { }
    }
}