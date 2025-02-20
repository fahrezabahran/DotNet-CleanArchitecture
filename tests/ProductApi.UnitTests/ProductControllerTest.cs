using DotNet_CleanArchitecture.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Test;

public class ProductControllerTest
{
    private Mock<IGetProductUseCase> _mockGetProductUseCase;
    private Mock<IGetAllProductsUseCase> _mockGetAllProductsUseCase;
    private Mock<ICreateProductUseCase> _mockCreateProductUseCase;
    private Mock<IUpdateProductUseCase> _mockUpdateProductUseCase;
    private Mock<IDeleteProductUseCase> _mockDeleteProductUseCase;   

    private ProductsController _controller;
    [SetUp]
    public void Setup()
    {
        _mockGetProductUseCase = new Mock<IGetProductUseCase>();
        _mockGetAllProductsUseCase = new Mock<IGetAllProductsUseCase>();
        _mockCreateProductUseCase = new Mock<ICreateProductUseCase>();
        _mockUpdateProductUseCase = new Mock<IUpdateProductUseCase>();
        _mockDeleteProductUseCase = new Mock<IDeleteProductUseCase>();

        _controller = new ProductsController(_mockCreateProductUseCase.Object, _mockGetAllProductsUseCase.Object, _mockGetProductUseCase.Object, _mockUpdateProductUseCase.Object, _mockDeleteProductUseCase.Object);
    }

    [Test]
    public async Task GetProduct_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        int productId = 2;
        var expectedProduct = new SuccessResponse<Product>(new Product
        {
            Id = productId,
            Name = "Fahreza Bahran",
            Price = 13400000.00M
        }, "Successfull");

        // Setup mock untuk mengembalikan expectedProduct saat metode Execute dipanggil
        _mockGetProductUseCase.Setup(useCase => useCase.Execute(productId))
                              .ReturnsAsync(expectedProduct);

        // Act
        var result = await _controller.Get(productId) as OkObjectResult;

        SuccessResponse<Product> response = result.Value as SuccessResponse<Product>;

        if (result != null && response != null)
        {

            Console.WriteLine("response: " + response.Success);
            Console.WriteLine("response: " + response.Message);
            Console.WriteLine("response: " + response.Data.Id);
            Console.WriteLine("response: " + response.Data.Name);
            Console.WriteLine("response: " + response.Data.Price);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.AreEqual("Successfull", response.Message);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(2, response.Data.Id);
            Assert.AreEqual("Fahreza Bahran", response.Data.Name);
            Assert.AreEqual(13400000.00M, response.Data.Price);
        }
    }
}
