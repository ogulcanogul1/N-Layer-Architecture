using App.Service.Products;

namespace App.Service.Categories
{
    public record CategoryWithProductsDto(int Id,string Name , List<ProductDto> Products);
}
