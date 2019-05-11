using System.Collections.Generic;
using System.Threading.Tasks;
using Spartan.Common;
using Spartan.Models;
using Spartan.Resources;

namespace Spartan.Interfaces
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
    }
}
