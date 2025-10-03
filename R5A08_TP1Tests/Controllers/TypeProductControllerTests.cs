using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Shared.DTO.Commun;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Mapping;
using R5A08_TP1.Models.Repository;
using System.Collections.Generic;
using System.Linq;

namespace R5A08_TP1Tests.Controllers
{
    [TestClass]
    public class TypeProductsControllerTests
    {
        private readonly ProductsDbContext _context;
        private readonly TypeProductsController _controller;
        private readonly IDataRepository<TypeProduct> _repository;
        private readonly IMapper _mapper;

        public TypeProductsControllerTests()
        {
            _context = new ProductsDbContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = config.CreateMapper();

            _repository = new TypeProductManager(_context);
            _controller = new TypeProductsController(_repository, _mapper);
        }

        [TestMethod]
        public void ShouldGetAllTypeProducts()
        {
            var result = _controller.GetAll().GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var types = (result.Result as OkObjectResult).Value as IEnumerable<TypeProductDto>;
            Assert.IsNotNull(types);
        }

        [TestMethod]
        public void ShouldGetTypeProduct()
        {
            var type = new TypeProduct { NameTypeProduct = "TestType_" + Guid.NewGuid() };
            _context.TypeProducts.Add(type);
            _context.SaveChanges();

            var result = _controller.Get(type.IdTypeProduct).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetTypeProductShouldReturnNotFound()
        {
            var result = _controller.Get(999999).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldCreateTypeProduct()
        {
            var dto = new TypeProductDto { NameTypeProduct = "NewType" };
            var result = _controller.Create(dto).GetAwaiter().GetResult();

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void ShouldUpdateTypeProduct()
        {
            var type = new TypeProduct { NameTypeProduct = "TestType_" + Guid.NewGuid() };
            _context.TypeProducts.Add(type);
            _context.SaveChanges();

            var updateDto = new TypeProductDto { NameTypeProduct = "UpdatedType" };
            var result = _controller.Update(type.IdTypeProduct, updateDto).GetAwaiter().GetResult();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var updated = (result as OkObjectResult).Value as TypeProductDto;
            Assert.AreEqual("UpdatedType", updated.NameTypeProduct);
        }

        [TestMethod]
        public void ShouldNotUpdateTypeProductBecauseNotFound()
        {
            var updateDto = new TypeProductDto { NameTypeProduct = "DoesNotExist" };
            var result = _controller.Update(999999, updateDto).GetAwaiter().GetResult();

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldDeleteTypeProduct()
        {
            var type = new TypeProduct { NameTypeProduct = "TestType_" + Guid.NewGuid() };
            _context.TypeProducts.Add(type);
            _context.SaveChanges();

            var result = _controller.Delete(type.IdTypeProduct).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void ShouldNotDeleteTypeProductBecauseNotFound()
        {
            var result = _controller.Delete(999999).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
