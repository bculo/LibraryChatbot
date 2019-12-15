using INTS_API.Entities;
using INTS_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace INTS_API.Persistence.Repository
{
    public class CategoryRepository : AsyncRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDBContext context) : base(context) { }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
