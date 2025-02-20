using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Exceptions;
using ProductApi.Application.Interfaces.ProductInterfaces;

namespace DotNet_CleanArchitecture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(ICreateProductUseCase createProductUseCase, IGetAllProductsUseCase getAllProductsUseCase, IGetProductUseCase getProductUseCase, IUpdateProductUseCase updateProductUseCase, IDeleteProductUseCase deleteProductUseCase) : ControllerBase
    {
        private readonly ICreateProductUseCase _createProductUseCase = createProductUseCase;
        private readonly IGetAllProductsUseCase _getAllProductsUseCase = getAllProductsUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase = updateProductUseCase;
        private readonly IGetProductUseCase _getProductUseCase = getProductUseCase;
        private readonly IDeleteProductUseCase _deleteProductUseCase = deleteProductUseCase;

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ProductDto productDto)
        {
            return Ok(await _createProductUseCase.Execute(productDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _getAllProductsUseCase.Execute(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _getProductUseCase.Execute(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Product product)
        {
            return Ok(await _updateProductUseCase.Execute(id, product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            return Ok(await _deleteProductUseCase.Execute(id));
        }
    }
}
