using System.Linq;
using System.Threading.Tasks;
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
            var searchResult = repositoryContext.AppointmentTypes.SingleOrDefault(x => x.Name == name);
            if(searchResult != null)
            {
                return true;
            }

            return false;
        }
    }
}