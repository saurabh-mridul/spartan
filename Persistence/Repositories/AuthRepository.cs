using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spartan.Interfaces;
using Spartan.Models;
using Spartan.Persistence.Contexts;

namespace Spartan.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private AppDbContext _appContext;
        public AuthRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(x => x.Name == userName);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassword(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _appContext.Users.AddAsync(user);
            await _appContext.SaveChangesAsync();

            return user;
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _appContext.Users.AnyAsync(x => x.Name == userName))
                return true;

            return false;
        }
    }
}