using DotNet_CleanArchitecture.Controllers;
using Moq;
using ProductApi.Application.Interfaces;
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
    public async Task Execute_WithValidId_ReturnsSuccessResponse()
    {
        // Arrange
        int productId = 1;
        var expectedProduct = new SuccessResponse<Product>(new Product
        {
            Id = productId,
            Name = "Laptop",
            Price = 15000
        }, "Product found");

        // Setup mock agar ketika Execute(productId) dipanggil, ia mengembalikan expectedProduct
        _mockGetProductUseCase.Setup(useCase => useCase.Execute(productId))
                              .ReturnsAsync(expectedProduct);

        // Act
        var result = await _mockGetProductUseCase.Object.Execute(productId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Product found", result.Message);
    }
}
