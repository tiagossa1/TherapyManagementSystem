using System;
using System.Linq;
using TherapyAPI.Context;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Repository
{
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(RepositoryContext RepositoryContext) : base(RepositoryContext) { }

        public Therapist Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = Context.Therapists.FirstOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public Therapist Create(Therapist therapist, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (Context.Therapists.Any(x => x.Username == therapist.Username))
                throw new Exception($"Username {therapist.Username} is already taken.");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            therapist.PasswordHash = passwordHash;
            therapist.PasswordSalt = passwordSalt;

            Context.Therapists.Add(therapist);
            Context.SaveChanges();

            return therapist;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}