using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository
{
    public interface IAppointmentTypeRepository : IRepository<AppointmentType>
    {
         bool GetByName(string name);
    }
}