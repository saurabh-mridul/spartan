using System.Collections.Generic;
using System.Threading.Tasks;
using Spartan.Interfaces;
using Spartan.Models;

namespace Spartan.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task SaveAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
                      
            existingCategory.Name = category.Name;
            _categoryRepository.Update(existingCategory);
        }
    }
}
