using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Shared.DTO.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace R5A08_TP1Tests.Controllers
{
    [TestClass]
    [TestCategory("mock")]
    public class ProductsControllerMockTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IDataRepository<Product>> _repositoryMock;
        private readonly Mock<AutoMapper.IMapper> _mapperMock;

        public ProductsControllerMockTests()
        {
            _repositoryMock = new Mock<IDataRepository<Product>>();
            _mapperMock = new Mock<AutoMapper.IMapper>();
            _controller = new ProductsController(_repositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task ShouldGetAllProducts()
        {
            var products = new List<Product> { new() { IdProduct = 1, NameProduct = "Chaise" } };
            var dtos = new List<ProductDto> { new() { IdProduct = 1, NameProduct = "Chaise" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new ActionResult<IEnumerable<Product>>(products));
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(dtos);

            var result = await _controller.GetAll();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dtos, ok.Value);
        }

        [TestMethod]
        public async Task ShouldGetProduct()
        {
            var entity = new Product { IdProduct = 1, NameProduct = "Chaise" };
            var dto = new ProductDetailDto { IdProduct = 1, NameProduct = "Chaise" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<ProductDetailDto>(entity)).Returns(dto);

            var result = await _controller.Get(1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dto, ok.Value);
        }

        [TestMethod]
        public async Task GetProductShouldReturnNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((Product)null);

            var result = await _controller.Get(42);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldCreateProduct()
        {
            var createDto = new ProductCreateDto { NameProduct = "NewProduct" };
            var entity = new Product { IdProduct = 1, NameProduct = "NewProduct" };

            _mapperMock.Setup(m => m.Map<Product>(createDto)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<ProductCreateDto>(entity)).Returns(createDto);

            var result = await _controller.Create(createDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ShouldUpdateProduct()
        {
            var existing = new Product { IdProduct = 1, NameProduct = "Old" };
            var updateDto = new ProductUpdateDto { NameProduct = "Updated" };
            var updatedDetailDto = new ProductDetailDto { IdProduct = 1, NameProduct = "Updated" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mapperMock.Setup(m => m.Map<Product>(updateDto)).Returns(existing);
            _mapperMock.Setup(m => m.Map<ProductDetailDto>(existing)).Returns(updatedDetailDto);

            var result = await _controller.Update(1, updateDto);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = result as OkObjectResult;
            Assert.AreEqual(updatedDetailDto, ok.Value);
        }

        [TestMethod]
        public async Task ShouldNotUpdateProductBecauseNotFound()
        {
            var updateDto = new ProductUpdateDto { NameProduct = "Unknown" };
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((Product)null);

            var result = await _controller.Update(42, updateDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldDeleteProduct()
        {
            var entity = new Product { IdProduct = 1, NameProduct = "Delete" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            var result = await _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _repositoryMock.Verify(r => r.DeleteAsync(entity), Times.Once);
        }

        [TestMethod]
        public async Task ShouldNotDeleteProductBecauseNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((Product)null);

            var result = await _controller.Delete(42);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
