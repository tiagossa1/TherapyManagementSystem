using System.Linq;
using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class AppointmentTypeRepository : Repository<AppointmentType>, IAppointmentTypeRepository
    {
        public AppointmentTypeRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
        }

        public bool GetByName(string name)
        {
            var searchResult = Context.AppointmentType.FirstOrDefault(x => x.Name == name);
            return searchResult != null;
        }
    }
}