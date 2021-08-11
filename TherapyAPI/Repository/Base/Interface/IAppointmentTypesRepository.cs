using TherapyAPI.Models;

namespace TherapyAPI.Repository.Base.Interface
{
    public interface IAppointmentTypeRepository : IRepository<AppointmentType>
    {
         bool GetByName(string name);
    }
}