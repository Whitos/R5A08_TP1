using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using R5A08_TP1.Shared.DTO.Commun;
using R5A08_TP1.Models.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using R5A08_TP1.Shared.DTO.Products;

namespace R5A08_TP1Tests.Controllers
{
    [TestClass()]
    public class ProductsControllerTests
    {
        private readonly ProductsDbContext _context;
        private readonly ProductsController _productController;
        private readonly IDataRepository<Product> _dataRepository;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            _context = new ProductsDbContext();

            // Configuration AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            _mapper = config.CreateMapper();

            _dataRepository = new ProductManager(_context);
            _productController = new ProductsController(_dataRepository, _mapper);
        }

        [TestMethod()]
        public void ShouldGetAllProducts()
        {
            // Given: Des produits en base de données
            List<Product> productsInDb = new()
            {
                new Product
                {
                    NameProduct = "Chaise Test",
                    Description = "Une chaise de test",
                    NamePhoto = "chaise.jpg",
                    UriPhoto = "https://ikea.fr/chaise.jpg",
                    ActualStock = 10,
                    MinStock = 5,
                    MaxStock = 50,
                    IdBrand = 1,
                    IdTypeProduct = 1
                },
                new Product
                {
                    NameProduct = "Armoire Test",
                    Description = "Une armoire de test",
                    NamePhoto = "armoire.jpg",
                    UriPhoto = "https://ikea.fr/armoire.jpg",
                    ActualStock = 8,
                    MinStock = 3,
                    MaxStock = 30,
                    IdBrand = 1,
                    IdTypeProduct = 1
                }
            };

            _context.Products.AddRange(productsInDb);
            _context.SaveChanges();

            // When: J'appelle la méthode GetAll de mon API
            var result = _productController.GetAll().GetAwaiter().GetResult();

            // Then: Tous les produits sont récupérés sous forme de DTOs
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<ProductDto>));

            var products = (okResult.Value as IEnumerable<ProductDto>).ToList();
            Assert.IsTrue(products.Count >= 2);
        }

        [TestMethod()]
        public void ShouldGetProduct()
        {
            // Given: Un produit en base de données
            Product productInDb = new()
            {
                NameProduct = "Table Test",
                Description = "Une table de test",
                NamePhoto = "table.jpg",
                UriPhoto = "https://ikea.fr/table.jpg",
                ActualStock = 15,
                MinStock = 5,
                MaxStock = 40,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            _context.Products.Add(productInDb);
            _context.SaveChanges();

            // When: J'appelle la méthode Get de mon API pour récupérer le produit
            ActionResult<ProductDetailDto> action = _productController.Get(productInDb.IdProduct).GetAwaiter().GetResult();

            // Then: On récupère le produit et le code de retour est 200 (OK)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));

            var okResult = action.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductDetailDto));

            var returnProduct = okResult.Value as ProductDetailDto;
            Assert.AreEqual(productInDb.IdProduct, returnProduct.IdProduct);
            Assert.AreEqual(productInDb.NameProduct, returnProduct.NameProduct);
        }

        [TestMethod()]
        public void GetProductShouldReturnNotFound()
        {
            // When: J'appelle la méthode Get de mon API pour un ID inexistant
            ActionResult<ProductDetailDto> action = _productController.Get(999999).GetAwaiter().GetResult();

            // Then: On renvoie 404
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Ne renvoie pas 404");
        }

        [TestMethod]
        public void ShouldCreateProduct()
        {
            // Given: Un DTO de création de produit
            ProductCreateDto productDto = new()
            {
                NameProduct = "Bureau Test",
                Description = "Un bureau de test",
                NamePhoto = "bureau.jpg",
                UriPhoto = "https://ikea.fr/bureau.jpg",
                ActualStock = 12,
                MinStock = 4,
                MaxStock = 35,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            // When: J'appelle la méthode Create
            ActionResult<ProductCreateDto> action = _productController.Create(productDto).GetAwaiter().GetResult();

            // Then: Le produit est créé et on reçoit un 201 Created
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));

            var createdResult = action.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);

            var createdDto = createdResult.Value as ProductCreateDto;
            Assert.IsNotNull(createdDto);
            Assert.AreEqual(productDto.NameProduct, createdDto.NameProduct);
        }

        [TestMethod()]
        public void ShouldDeleteProduct()
        {
            // Given: Un produit en base de données
            Product productInDb = new()
            {
                NameProduct = "Lampe Test",
                Description = "Une lampe de test",
                NamePhoto = "lampe.jpg",
                UriPhoto = "https://ikea.fr/lampe.jpg",
                ActualStock = 20,
                MinStock = 8,
                MaxStock = 60,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            _context.Products.Add(productInDb);
            _context.SaveChanges();

            int productId = productInDb.IdProduct;

            // When: Je souhaite supprimer un produit depuis l'API
            IActionResult action = _productController.Delete(productId).GetAwaiter().GetResult();

            // Then: Le produit a bien été supprimé et le code HTTP est NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NoContentResult));
            Assert.IsNull(_context.Products.Find(productId));
        }

        [TestMethod]
        public void ShouldUpdateProduct()
        {
            // Given: Un produit à mettre à jour
            Product productToEdit = new()
            {
                NameProduct = "Canapé Initial",
                Description = "Un canapé initial",
                NamePhoto = "canape.jpg",
                UriPhoto = "https://ikea.fr/canape.jpg",
                ActualStock = 5,
                MinStock = 2,
                MaxStock = 20,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            _context.Products.Add(productToEdit);
            _context.SaveChanges();

            int productId = productToEdit.IdProduct;

            // DTO avec les modifications
            ProductUpdateDto updateDto = new()
            {
                NameProduct = "Canapé Modifié",
                Description = "Un canapé modifié",
                NamePhoto = "canape_modifie.jpg",
                UriPhoto = "https://ikea.fr/canape_modifie.jpg",
                ActualStock = 7,
                MinStock = 2,
                MaxStock = 20,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            // When: On appelle la méthode PUT du controller
            IActionResult action = _productController.Update(productId, updateDto).GetAwaiter().GetResult();

            // Then: Le produit a bien été modifié
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(OkObjectResult));

            var okResult = action as OkObjectResult;
            var updatedDto = okResult.Value as ProductDetailDto;
            Assert.IsNotNull(updatedDto);
            Assert.AreEqual("Canapé Modifié", updatedDto.NameProduct);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        {
            // Given: Un DTO de mise à jour pour un produit inexistant
            ProductUpdateDto updateDto = new()
            {
                NameProduct = "Produit Inexistant",
                Description = "Description",
                NamePhoto = "photo.jpg",
                UriPhoto = "https://test.fr/photo.jpg",
                ActualStock = 10,
                MinStock = 5,
                MaxStock = 30,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            // When: On appelle la méthode PUT avec un ID inexistant
            IActionResult action = _productController.Update(999999, updateDto).GetAwaiter().GetResult();

            // Then: On vérifie que l'API renvoie un code NOT_FOUND (404)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // When: Je souhaite supprimer un produit inexistant
            IActionResult action = _productController.Delete(999999).GetAwaiter().GetResult();

            // Then: Le code HTTP est NOT_FOUND (404)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldCalculateEnReapproCorrectly()
        {
            // Given: Un produit avec un stock actuel inférieur au stock minimum
            Product productInDb = new()
            {
                NameProduct = "Produit En Réappro",
                Description = "Produit à réapprovisionner",
                NamePhoto = "reappro.jpg",
                UriPhoto = "https://test.fr/reappro.jpg",
                ActualStock = 3,
                MinStock = 5,
                MaxStock = 20,
                IdBrand = 1,
                IdTypeProduct = 1
            };

            _context.Products.Add(productInDb);
            _context.SaveChanges();

            // When: Je récupère le détail du produit
            ActionResult<ProductDetailDto> action = _productController.Get(productInDb.IdProduct).GetAwaiter().GetResult();

            // Then: Le flag EnReappro doit être true
            var okResult = action.Result as OkObjectResult;
            var productDto = okResult.Value as ProductDetailDto;

            Assert.IsNotNull(productDto);
            Assert.IsTrue(productDto.EnReappro, "Le produit devrait être en réapprovisionnement");
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    _context.Products.RemoveRange(_context.Products);
        //    _context.SaveChanges();
        //}
    }
}