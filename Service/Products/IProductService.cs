using App.Repositories.Products;
using App.Service.Products.Create;
using App.Service.Products.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> TopSellingProducts(int count);
        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<ProductDto>>> GetAllAsync();
        Task<ServiceResult<List<ProductDto>>> GetPageAsync(int page , int pageSize);
        Task<ServiceResult> UpdateStockAsync(int id, int quantity);
    }
}
