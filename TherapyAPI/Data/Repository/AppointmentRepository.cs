using TherapyAPI.Entities;
using TherapyAPI.Models;
using TherapyAPI.Repository;

namespace TherapyAPI.Data.Repository {
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository {
        public AppointmentRepository (RepositoryContext RepositoryContext) : base (RepositoryContext) { }
    }
}