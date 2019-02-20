using System.Linq;
using TherapyAPI.Entities;
using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository
{
    public class AppointmentTypesRepository : Repository<AppointmentTypes>, IAppointmentTypesRepository
    {
        public AppointmentTypesRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
        }

        public bool GetByName(string name)
        {
            if(repositoryContext.AppointmentTypes.Find(name) != null)
            {
                return true;
            }

            return false;
        }
    }
}