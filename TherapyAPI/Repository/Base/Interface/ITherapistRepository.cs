using TherapyAPI.Models;

namespace TherapyAPI.Repository.Base.Interface
{
    public interface ITherapistRepository : IRepository<Therapist>
    {
         Therapist Create(Therapist therapist, string password);
         Therapist Authenticate(string username, string password);
    }
}