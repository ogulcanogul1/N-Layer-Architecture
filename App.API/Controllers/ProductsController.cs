using App.Service;
using App.Service.Products;
using App.Service.Products.Create;
using App.Service.Products.Update;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace App.API.Controllers;


public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));// ServiceResult<CreateProductResponse>

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPage(int page, int pageSize) => CreateActionResult(await productService.GetPageAsync(page, pageSize));

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateStock(int id, int quantity) => CreateActionResult(await productService.UpdateStockAsync(id, quantity));

    
}
