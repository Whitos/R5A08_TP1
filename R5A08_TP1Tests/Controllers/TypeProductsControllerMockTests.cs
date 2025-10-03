using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Shared.DTO.Commun;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace R5A08_TP1Tests.Controllers
{
    [TestClass]
    [TestCategory("mock")]
    public class TypeProductsControllerMockTests
    {
        private readonly TypeProductsController _controller;
        private readonly Mock<IDataRepository<TypeProduct>> _repositoryMock;
        private readonly Mock<AutoMapper.IMapper> _mapperMock;

        public TypeProductsControllerMockTests()
        {
            _repositoryMock = new Mock<IDataRepository<TypeProduct>>();
            _mapperMock = new Mock<AutoMapper.IMapper>();
            _controller = new TypeProductsController(_repositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task ShouldGetAllTypeProducts()
        {
            var entities = new List<TypeProduct> { new() { IdTypeProduct = 1, NameTypeProduct = "Meuble" } };
            var dtos = new List<TypeProductDto> { new() { IdTypeProduct = 1, NameTypeProduct = "Meuble" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new ActionResult<IEnumerable<TypeProduct>>(entities));
            _mapperMock.Setup(m => m.Map<IEnumerable<TypeProductDto>>(entities)).Returns(dtos);

            var result = await _controller.GetAll();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dtos, ok.Value);
        }

        [TestMethod]
        public async Task ShouldGetTypeProduct()
        {
            var entity = new TypeProduct { IdTypeProduct = 1, NameTypeProduct = "Electronique" };
            var dto = new TypeProductDto { IdTypeProduct = 1, NameTypeProduct = "Electronique" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<TypeProductDto>(entity)).Returns(dto);

            var result = await _controller.Get(1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dto, ok.Value);
        }

        [TestMethod]
        public async Task GetTypeProductShouldReturnNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((TypeProduct)null);

            var result = await _controller.Get(42);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldCreateTypeProduct()
        {
            var dto = new TypeProductDto { NameTypeProduct = "NouveauType" };
            var entity = new TypeProduct { IdTypeProduct = 1, NameTypeProduct = "NouveauType" };

            _mapperMock.Setup(m => m.Map<TypeProduct>(dto)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<TypeProductDto>(entity)).Returns(dto);

            var result = await _controller.Create(dto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ShouldUpdateTypeProduct()
        {
            var existing = new TypeProduct { IdTypeProduct = 1, NameTypeProduct = "OldType" };
            var updateDto = new TypeProductDto { IdTypeProduct = 1, NameTypeProduct = "UpdatedType" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mapperMock.Setup(m => m.Map<TypeProduct>(updateDto)).Returns(existing);
            _mapperMock.Setup(m => m.Map<TypeProductDto>(existing)).Returns(updateDto);

            var result = await _controller.Update(1, updateDto);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task ShouldNotUpdateTypeProductBecauseNotFound()
        {
            var updateDto = new TypeProductDto { IdTypeProduct = 42, NameTypeProduct = "Unknown" };
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((TypeProduct)null);

            var result = await _controller.Update(42, updateDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldDeleteTypeProduct()
        {
            var entity = new TypeProduct { IdTypeProduct = 1, NameTypeProduct = "DeleteType" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            var result = await _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _repositoryMock.Verify(r => r.DeleteAsync(entity), Times.Once);
        }

        [TestMethod]
        public async Task ShouldNotDeleteTypeProductBecauseNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((TypeProduct)null);

            var result = await _controller.Delete(99);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
