//using DotNet_CleanArchitecture.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using ProductApi.Application.UseCases.Product;
//using ProductApi.Domain.Entities;

//namespace ProductApi.Test
//{
//    [TestFixture]
//    public class ProductControllerTests
//    {
//        private Mock<GetProductUseCase> _mockGetProductUseCase;
//        private ProductsController _controller;

//        private readonly Mock<CreateProductUseCase> _createProductUseCase;
//        private readonly Mock<GetAllProductsUseCase> _getAllProductsUseCase;
//        private readonly Mock<UpdateProductUseCase> _updateProductUseCase;
//        private readonly Mock<DeleteProductUseCase> _deleteProductUseCase; 

//        [SetUp]
//        public void Setup()
//        {
//            _mockGetProductUseCase = new Mock<GetProductUseCase>();
//            _controller = new ProductsController(_createProductUseCase.Object, _getAllProductsUseCase.Object, _mockGetProductUseCase.Object, _updateProductUseCase.Object, _deleteProductUseCase.Object);
//        }

//        [Test]
//        public async Task Get_ReturnsOkResult_WhenProductExists()
//        {
//            // Arrange
//            var productId = 1;
//            var expectedProduct = new Product { Id = productId, Name = "Test Product", Price = 100 }; // Ganti dengan properti yang sesuai

//            _mockGetProductUseCase.Setup(useCase => useCase.Execute(productId))
//                .ReturnsAsync(expectedProduct);

//            // Act
//            var result = await _controller.Get(productId);
//            if (result.Result == null) { return; }

//            // Assert
//            var okResult = Assert.<OkObjectResult>(result.Result); // Memastikan result adalah OkObjectResult
//            var actualProduct = okResult.Value as Product; // Mengambil nilai produk dari OkObjectResult

//            Assert.IsNotNull(actualProduct);
//            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
//            Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
//            Assert.AreEqual(expectedProduct.Price, actualProduct.Price); // Memastikan harga juga sesuai
//        }


//        [Test]
//        public async Task Get_ReturnsNotFound_WhenProductDoesNotExist()
//        {
//            // Arrange
//            var productId = 1;

//            _mockGetProductUseCase.Setup(useCase => useCase.Execute(productId))
//                .ReturnsAsync((Product)null); // Simulasi produk tidak ditemukan

//            // Act
//            var result = await _controller.Get(productId);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result.Result);
//        }
//    }
//}