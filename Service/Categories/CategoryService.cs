using App.Repositories;
using App.Repositories.Categories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace App.Service.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork,IMapper mapper) : ICategoryService
    {
        public async ValueTask<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            // check unique name
            bool control = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();

            if(control)
            {
                return ServiceResult<int>.Fail("Kategori adı zaten mevcut");
            }

            Category category = mapper.Map<CreateCategoryRequest, Category>(request);

            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(category.Id,$"api/categories/{category.Id}");
        }

        public async ValueTask<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            // check unique name
            bool control = await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
            if (control) 
            {
                return ServiceResult.Fail("Kategori adı zaten mevcut");
            }

            Category? category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                ServiceResult.Fail("Kategori Bulunamadı");
            }

            category!.Name = request.Name;
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }

        public async ValueTask<ServiceResult> DeleteAsync(int id)
        {
            Category? category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("Kategori Bulunamadı");
            }
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }

        public async ValueTask<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            List<Category> categories = await categoryRepository.GetAll().ToListAsync();

            if(categories == null)
            {
                ServiceResult.Fail("Kategori bulunamadı",HttpStatusCode.NotFound);
            }

            var categoriesAsDtos = mapper.Map<List<CategoryDto>>(categories);
                
            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDtos!);
        }

        public async ValueTask<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            Category? category = await categoryRepository.GetByIdAsync(id);

            if(category == null)
            {
                ServiceResult<CategoryDto>.Fail("Kategori bulunamadı");
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto!);
        }

        public async ValueTask<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductById(int id)
        {
            Category? category = await categoryRepository.GetCategoryWithProductByIdAsync(id);

            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Kategori Bulunamadı");
            }

            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto!);
        }

        public async ValueTask<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync()
        {
            List<Category> categoriesWithProducts =  await categoryRepository.GetCategoriesWithProductsAsync();

            if(categoriesWithProducts == null)
            {
                return ServiceResult<List<CategoryWithProductsDto>>.Fail("Kategori Listesi Bulunamadı");
            }

            List<CategoryWithProductsDto> categoriesDto = mapper.Map<List<CategoryWithProductsDto>>(categoriesWithProducts);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesDto);
        }
    }
}
