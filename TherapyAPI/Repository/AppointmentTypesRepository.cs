using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class AppointmentTypeRepository : Repository<AppointmentType>, IAppointmentTypeRepository
    {
        public AppointmentTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Task<bool> AnyByName(string name)
        {
            return Context.AppointmentType.AnyAsync(x => x.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}