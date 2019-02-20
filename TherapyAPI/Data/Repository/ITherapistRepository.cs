using TherapyAPI.Repository;

namespace TherapyAPI.Models
{
    public interface ITherapistRepository : IRepository<Therapist>
    {
         Therapist Create(Therapist therapist, string password);
         Therapist Authenticate(string username, string password);
    }
}