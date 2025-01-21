using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Categories
{
    public interface ICategoryService
    {
        ValueTask<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        ValueTask<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        ValueTask<ServiceResult> DeleteAsync(int id);
        ValueTask<ServiceResult<List<CategoryDto>>> GetAllAsync();
        ValueTask<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        ValueTask<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductById(int id);
        ValueTask<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync();
    }
}
