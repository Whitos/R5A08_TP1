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
    public class BrandsControllerMockTests
    {
        private readonly BrandsController _controller;
        private readonly Mock<IDataRepository<Brand>> _repositoryMock;
        private readonly Mock<AutoMapper.IMapper> _mapperMock;

        public BrandsControllerMockTests()
        {
            _repositoryMock = new Mock<IDataRepository<Brand>>();
            _mapperMock = new Mock<AutoMapper.IMapper>();
            _controller = new BrandsController(_repositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task ShouldGetAllBrands()
        {
            var entities = new List<Brand> { new() { IdBrand = 1, NameBrand = "Nike" } };
            var dtos = new List<BrandDto> { new() { IdBrand = 1, NameBrand = "Nike" } };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new ActionResult<IEnumerable<Brand>>(entities));
            _mapperMock.Setup(m => m.Map<IEnumerable<BrandDto>>(entities)).Returns(dtos);

            var result = await _controller.GetAll();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dtos, ok.Value);
        }

        [TestMethod]
        public async Task ShouldGetBrand()
        {
            var entity = new Brand { IdBrand = 1, NameBrand = "Adidas" };
            var dto = new BrandDto { IdBrand = 1, NameBrand = "Adidas" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<BrandDto>(entity)).Returns(dto);

            var result = await _controller.Get(1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var ok = result.Result as OkObjectResult;
            Assert.AreEqual(dto, ok.Value);
        }

        [TestMethod]
        public async Task GetBrandShouldReturnNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((Brand)null);

            var result = await _controller.Get(42);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldCreateBrand()
        {
            var createDto = new BrandCreateDto { NameBrand = "Puma" };
            var entity = new Brand { IdBrand = 1, NameBrand = "Puma" };

            _mapperMock.Setup(m => m.Map<Brand>(createDto)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<BrandDto>(entity)).Returns(new BrandDto { IdBrand = 1, NameBrand = "Puma" });

            var result = await _controller.Create(createDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task ShouldUpdateBrand()
        {
            var existing = new Brand { IdBrand = 1, NameBrand = "OldBrand" };
            var updateDto = new BrandUpdateDto { NameBrand = "UpdatedBrand" };
            var updatedDto = new BrandDto { IdBrand = 1, NameBrand = "UpdatedBrand" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _mapperMock.Setup(m => m.Map<Brand>(updateDto)).Returns(existing);
            _mapperMock.Setup(m => m.Map<BrandDto>(existing)).Returns(updatedDto);

            var result = await _controller.Update(1, updateDto);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = result as OkObjectResult;
            Assert.AreEqual(updatedDto, ok.Value);
        }

        [TestMethod]
        public async Task ShouldNotUpdateBrandBecauseNotFound()
        {
            var updateDto = new BrandUpdateDto { NameBrand = "Ghost" };
            _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Brand)null);

            var result = await _controller.Update(99, updateDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldDeleteBrand()
        {
            var entity = new Brand { IdBrand = 1, NameBrand = "DeleteMe" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

            var result = await _controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            _repositoryMock.Verify(r => r.DeleteAsync(entity), Times.Once);
        }

        [TestMethod]
        public async Task ShouldNotDeleteBrandBecauseNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Brand)null);

            var result = await _controller.Delete(99);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
