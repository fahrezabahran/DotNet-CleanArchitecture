using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.UseCases;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Exceptions;

namespace DotNet_CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(CreateProductUseCase createProductUseCase, GetAllProductsUseCase getAllProductsUseCase, UpdateProductUseCase updateProductUseCase, GetProductUseCase getProductUseCase, DeleteProductUseCase deleteProductUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase _createProductUseCase = createProductUseCase;
        private readonly GetAllProductsUseCase _getAllProductsUseCase = getAllProductsUseCase;
        private readonly UpdateProductUseCase _updateProductUseCase = updateProductUseCase;
        private readonly GetProductUseCase _getProductUseCase = getProductUseCase;
        private readonly DeleteProductUseCase _deleteProductUseCase = deleteProductUseCase;

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ProductDto productDto)
        {
            var product = await _createProductUseCase.Execute(productDto);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var products = await _getAllProductsUseCase.Execute();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _getProductUseCase.Execute(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _updateProductUseCase.Execute(product);
                return NoContent();
            } 
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _deleteProductUseCase.Execute(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
