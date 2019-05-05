using Spartan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spartan.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task SaveAsync(Category category);
        Task UpdateAsync(int id, Category category);
    }
}
