using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Spartan.Models;
using Spartan.Persistence.Contexts;

namespace Spartan.Data
{
    public class Seed
    {
        private readonly AppDbContext _context;
        public Seed(AppDbContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePassword("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Name = user.Name.ToLower();

                    _context.Users.Add(user);

                }
                _context.SaveChanges();
            }
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}