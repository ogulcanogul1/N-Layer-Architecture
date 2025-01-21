using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        ValueTask<Category> GetCategoryWithProductByIdAsync(int id);
        ValueTask<List<Category>> GetCategoriesWithProductsAsync();
    }
}
