using App.Service.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async ValueTask<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllAsync());

        [HttpPost]
        public async ValueTask<IActionResult> CreateCategory(CreateCategoryRequest request) => CreateActionResult(await categoryService.CreateAsync(request));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.DeleteAsync(id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) => CreateActionResult(await categoryService.UpdateAsync(id, request));

        [HttpGet("{id}/products")]
        public async ValueTask<IActionResult> GetCategoryWithProductById(int id) => CreateActionResult(await categoryService.GetCategoryWithProductById(id));

        [HttpGet("categories/with/products")]
        public async ValueTask<IActionResult> GetCategoriesWithProducts() => CreateActionResult(await categoryService.GetCategoriesWithProductsAsync());

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetById(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));



    }
}
