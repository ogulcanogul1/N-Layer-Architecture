using App.Repositories;
using App.Repositories.Products;
using App.Service.ExceptionHandlers;
using App.Service.Products.Create;
using App.Service.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Service.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> TopSellingProducts(int count)
        {
            //return productRepository.TopSellingProducts(count);
            var product = await productRepository.TopSellingProducts(count);

            //var productAsDto = product.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productAsDto = mapper.Map<List<ProductDto>>(product);   
            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                //return new ServiceResult<Product> { ErrorMessage = new List<string>() { "Product Mot Found" } };

                return ServiceResult<ProductDto?>.Fail("Product Not Found", HttpStatusCode.NotFound);
            }
            //var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);

            var productAsDto = mapper.Map<ProductDto>(product);

            //return new ServiceResult<Product> { Data = product };
            return ServiceResult<ProductDto?>.Success(productAsDto)!; // Factory ile nesne üretildi her yerde new keywordu kullanmamak için
            // Dto ile geri döndürme işlemi yapıldı.
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            bool uniqueControl = await productRepository.GetAll().AnyAsync(p => p.Name == request.Name);

            if(uniqueControl)
            {
                return ServiceResult<CreateProductResponse>.Fail("Girilen ürün ismi kayıtlarda bulunuyor.");
            }

            //var product = new Product
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    Stock = request.Stock
            //};
            var product = mapper.Map<Product>(request);
            if(product is null)
            {
                return ServiceResult<CreateProductResponse>.Fail("Product not found", HttpStatusCode.NotFound);
            }
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id) , $"api/Products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id,UpdateProductRequest request)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) 
            {
                ServiceResult.Fail("Product not found",HttpStatusCode.NotFound);
            }

            var isNameUnique = await productRepository.Where(p => p.Name == request.Name && p.Id != id).AnyAsync();

            if(isNameUnique)
            {
                return ServiceResult.Fail("Güncellemek istediğiniz ürün ismi zaten veri tabanında bulunuyor");
            }

            product!.Name = request.Name;    
            product.Price = request.Price;  
            product.Stock = request.Stock;  

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();    
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                ServiceResult.Fail("Product not found");
            }

            productRepository.Delete(product!);
            await unitOfWork.SaveChangesAsync();    
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();

            //List<ProductDto> dtos = products.Select(p => new ProductDto(p.Id,p.Name,p.Price,p.Stock)).ToList();

            var dtos = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(dtos);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPageAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            List<Product> products = await productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();

            if(products == null)
            {
                return ServiceResult<List<ProductDto>>.Fail("Product not Found");
            }

            //List<ProductDto> dtos = products!.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var dtos = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(dtos);

        }

        public async Task<ServiceResult> UpdateStockAsync(int id,int quantity)
        {
            Product product = await productRepository.GetByIdAsync(id);
            if (product == null) 
            {
                return ServiceResult.Fail("Product Not Found");
            }

            product.Stock = quantity;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }

    


}
