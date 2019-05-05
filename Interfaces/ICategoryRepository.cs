using Spartan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartan.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        void Update(Category category);
    }
}
