using FakeItEasy;
using FluentAssertions;
using Restaurant.Services.ProductAPI.Controllers;
using Restaurant.Services.ProductAPI.Models.Dto;
using Restaurant.Services.ProductAPI.Models.Dtos;
using Restaurant.Services.ProductAPI.Repository;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.ProductAPI.Tests.Controller
{
    public class ProductAPIControllerTest
    {
        private readonly IProductRepository _productRepository;
        public ProductAPIControllerTest()
        {
            _productRepository = A.Fake<IProductRepository>();
        }

        [Fact]
        public void ProductAPIController_GetProducts_ReturnAllProducts()
        {
            //Arrange
            var products = A.Fake<IEnumerable<ProductDto>>();
            A.CallTo(() => _productRepository.GetProducts()).Returns(products);
            var controller = new ProductApiController(_productRepository);

            //Act
            var result = controller.Get();

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ResponseDto>();
        }

        [Fact]
        public void ProductAPIController_GetProduct_ReturnProductById()
        {
            //Arrange
            var product = A.Fake<ProductDto>();
            A.CallTo(() => _productRepository.GetProductById(1)).Returns(product);
            var controller = new ProductApiController(_productRepository);

            //Act
            var result = controller.Get(1);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ResponseDto>();
        }

    }
}
