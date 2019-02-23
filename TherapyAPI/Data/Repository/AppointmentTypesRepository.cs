using System.Linq;
using System.Threading.Tasks;
using TherapyAPI.Entities;
using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository
{
    public class AppointmentTypeRepository : Repository<AppointmentType>, IAppointmentTypeRepository
    {
        public AppointmentTypeRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
        }

        public bool GetByName(string name)
        {
            var searchResult = repositoryContext.AppointmentType.SingleOrDefault(x => x.Name == name);
            if(searchResult != null)
            {
                return true;
            }

            return false;
        }
    }
}