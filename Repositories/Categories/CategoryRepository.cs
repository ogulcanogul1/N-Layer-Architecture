using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public async ValueTask<Category> GetCategoryWithProductByIdAsync(int id)
        {
            return await context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException("Kategori Bulunamadı");
        }

        public async ValueTask<List<Category>> GetCategoriesWithProductsAsync()
        {
            return await context.Categories.Include(x => x.Products).ToListAsync();
        }

         
    }
}
