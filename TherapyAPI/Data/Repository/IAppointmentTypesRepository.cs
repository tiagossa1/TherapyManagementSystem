using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository
{
    public interface IAppointmentTypesRepository : IRepository<AppointmentTypes>
    {
         bool GetByName(string name);
    }
}