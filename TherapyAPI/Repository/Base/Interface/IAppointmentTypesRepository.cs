using System.Threading.Tasks;
using TherapyAPI.Models;

namespace TherapyAPI.Repository.Base.Interface
{
    public interface IAppointmentTypeRepository : IRepository<AppointmentType>
    {
        Task<bool> AnyByName(string name);
    }
}