using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapping;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Shared.DTO.Commun;
using System.Collections.Generic;
using System.Linq;

namespace R5A08_TP1Tests.Controllers
{
    [TestClass]
    public class BrandsControllerTests
    {
        private readonly ProductsDbContext _context;
        private readonly BrandsController _controller;
        private readonly IDataRepository<Brand> _repository;
        private readonly IMapper _mapper;

        public BrandsControllerTests()
        {
            _context = new ProductsDbContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = config.CreateMapper();

            _repository = new BrandManager(_context);
            _controller = new BrandsController(_repository, _mapper);
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    _context.Brands.RemoveRange(_context.Brands);
        //    _context.SaveChanges();

        //    _context.Database.ExecuteSqlRaw("ALTER SEQUENCE brand_id_brand_seq RESTART WITH 1;");
        //}

        [TestMethod]
        public void ShouldGetAllBrands()
        {
            // Arrange
            _context.Brands.Add(new Brand { NameBrand = "BrandA" });
            _context.Brands.Add(new Brand { NameBrand = "BrandB" });
            _context.SaveChanges();

            // Act
            var result = _controller.GetAll().GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var brands = (result.Result as OkObjectResult).Value as IEnumerable<BrandDto>;
            Assert.IsNotNull(brands);
            Assert.IsTrue(brands.Any());
        }

        [TestMethod]
        public void ShouldGetBrand()
        {
            // Arrange
            var brand = new Brand { NameBrand = "DeleteBrand_" + Guid.NewGuid() };
            _context.Brands.Add(brand);
            _context.SaveChanges();

            // Act
            var result = _controller.Get(brand.IdBrand).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var brandDto = okResult.Value as BrandDto;
            Assert.IsNotNull(brandDto);
            Assert.AreEqual(brand.NameBrand, brandDto.NameBrand);

        }

        [TestMethod]
        public void GetBrandShouldReturnNotFound()
        {
            var result = _controller.Get(999999).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldCreateBrand()
        {
            // Arrange
            var brandDto = new BrandCreateDto { NameBrand = "BrandTestCreate" };

            // Act
            var result = _controller.Create(brandDto).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var created = (result.Result as CreatedAtActionResult).Value as BrandDto;
            Assert.IsNotNull(created);
            Assert.AreEqual("BrandTestCreate", created.NameBrand);
        }

        [TestMethod]
        public void ShouldUpdateBrand()
        {
            // Arrange
            var brand = new Brand { NameBrand = "DeleteBrand_" + Guid.NewGuid() };
            _context.Brands.Add(brand);
            _context.SaveChanges();

            var updateDto = new BrandUpdateDto { NameBrand = "UpdatedBrand" };

            // Act
            var result = _controller.Update(brand.IdBrand, updateDto).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var updated = (result as OkObjectResult).Value as BrandDto;
            Assert.IsNotNull(updated);
            Assert.AreEqual("UpdatedBrand", updated.NameBrand);
        }

        [TestMethod]
        public void ShouldNotUpdateBrandBecauseNotFound()
        {
            var updateDto = new BrandUpdateDto { NameBrand = "DoesNotExist" };
            var result = _controller.Update(999999, updateDto).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldDeleteBrand()
        {
            // Arrange
            var brand = new Brand { NameBrand = "DeleteBrand_" + Guid.NewGuid() };
            _context.Brands.Add(brand);
            _context.SaveChanges();

            // Act
            var result = _controller.Delete(brand.IdBrand).GetAwaiter().GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.IsNull(_context.Brands.Find(brand.IdBrand));
        }

        [TestMethod]
        public void ShouldNotDeleteBrandBecauseNotFound()
        {
            var result = _controller.Delete(999999).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
