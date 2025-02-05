using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.UseCases;

namespace DotNet_CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(GetAllProductsUseCase getAllProductsUseCase, UpdateProductUseCase updateProductUseCase) : ControllerBase
    {
        private readonly GetAllProductsUseCase _getAllProductsUseCase = getAllProductsUseCase;
        private readonly UpdateProductUseCase _updateProductUseCase = updateProductUseCase;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _getAllProductsUseCase.Execute();
            return Ok(products);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Update(ProductDto productDto)
        {
            var products = await _updateProductUseCase.Execute(productDto);
            return Ok(products);
        }
    }
}
