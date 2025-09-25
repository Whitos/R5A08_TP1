using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
using R5A08_TP1.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R5A08_TP1.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private readonly ProductsDbContext _context;
        private readonly ProductsController _produitController;
        private IDataRepository<Product> _dataRepository;

        public ProduitsControllerTests()
        {
            _context = new ProductsDbContext();

            _dataRepository = new ProductManager(_context);
            _produitController = new ProductsController(_dataRepository);
        }

        [TestMethod()]
        public void ShouldGetAllProduits()
        {
            IEnumerable<Product> produitsInDb = [
                new ()
                {
                    NameProduct = "Chaise",
                    Description = "Une chaise nul",
                    NamePhoto = "chaise.jpg",
                    UriPhoto = "https://ikea.fr/chaise.jpg"
                },
                new ()
                {
                    NameProduct = "Armoir",
                    Description = "Une superbe armoire",
                    NamePhoto = "Une superbe armoire jaune",
                    UriPhoto = "https://ikea.fr/armoire-jaune.jpg"
                }
            ];

            _context.Produits.AddRange(produitsInDb);
            _context.SaveChanges();

            // When: j'appelle la méthode Get de mon api pour récuperer tous les produits
            var products = _produitController.GetAll().GetAwaiter().GetResult();

            // Then : Tous les produits sont récupérés
            Assert.IsNotNull(products);
            Assert.IsInstanceOfType(products.Value, typeof(IEnumerable<Product>));
        }

        [TestMethod()]
        public void ShouldGetProduit()
        {
            // Given: un produit en base de données 
            Product produitInDb = new Product
            {
                NameProduct = "Chaise",
                Description = "Une chaise nul",
                NamePhoto = "chaise.jpg",
            };

            _context.Produits.Add(produitInDb);
            _context.SaveChanges();


            // When: j'appelle la méthode Get de mon api pour récuperer le produit

            ActionResult<Product> action = _produitController.Get(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then: on recupere le produit et le code de retour est de 200 (OK)

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Product));

            Product returnProduct = action.Value;
            Assert.AreEqual(produitInDb.IdProduit, returnProduct.IdProduit);
            // Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));

        }

        [TestMethod()]
        public void GetProductShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon api pour récupérer le produit
            ActionResult<Product> action = _produitController.Get(0).GetAwaiter().GetResult();

            // Then : On ne renvoie rien et on renvoie 404
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Ne renvoie pas 404");
            Assert.IsNull(action.Value, "Le produit n'est pas null");
        }

        [TestMethod]
        public void ShouldCreateProduct()
        {
            // Given
            Product productToInsert = new Product()
            {
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            // When
            ActionResult<Product> action = _produitController.Create(productToInsert).GetAwaiter().GetResult();

            // Then
            Product productInDb = _context.Produits.Find(productToInsert.IdProduit);

            Assert.IsNotNull(productInDb);
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod()]
        public void ShouldDeleteProduct()
        {
            // Given: un produit en base de données 
            Product produitInDb = new Product()
            {
                NameProduct = "Chaise3",
                Description = "Une chaise nul",
                NamePhoto = "chaise.jpg",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _context.Produits.Add(produitInDb);
            _context.SaveChanges();

            // When : Je souhaite supprimé un produit depuis l'API
            IActionResult action = _produitController.Delete(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : Le produit a bien été supprimé et le code HTTP est NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NoContentResult));
            Assert.IsNull(_context.Produits.Find(produitInDb.IdProduit));
        }

        [TestMethod]
        public void ShouldUpdateProduct()
        {
            // Given : Un produit à mettre à jour
            Product produitToEdit = new()
            {
              
                NameProduct = "Bureau",
                Description = "Un super bureau",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            _context.Produits.Add(produitToEdit);
            _context.SaveChanges();

            // Une fois enregistré, on modifie certaines propriétés 
            produitToEdit.NameProduct = "Lit";
            produitToEdit.Description = "Un super lit";

            // When : On appelle la méthode PUT du controller pour mettre à jour le produit
            IActionResult action = _produitController.Update(produitToEdit.IdProduit, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que le produit a bien été modifié et que le code renvoyé et NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NoContentResult));

            Product editedProductInDb = _context.Produits.Find(produitToEdit.IdProduit);

            Assert.IsNotNull(editedProductInDb);
            Assert.AreEqual(produitToEdit, editedProductInDb);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseIdInUrlIsDifferent()
        {
            // Given : Un produit à mettre à jour
            Product produitToEdit = new()
            {
                NameProduct = "Bureau",
                Description = "Un super bureau",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            _context.Produits.Add(produitToEdit);
            _context.SaveChanges();

            produitToEdit.NameProduct = "Lit";
            produitToEdit.Description = "Un super lit";

            // When : On appelle la méthode PUT du controller pour mettre à jour le produit,
            // mais en précisant un ID différent de celui du produit enregistré
            IActionResult action = _produitController.Update(0, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code BAD_REQUEST (400)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        {
            // Given : Un produit à mettre à jour qui n'est pas enregistré
            Product produitToEdit = new()
            {
                IdProduit = 20,
                NameProduct = "Bureau",
                Description = "Un super bureau",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            // When : On appelle la méthode PUT du controller pour mettre à jour un produit qui n'est pas enregistré
            IActionResult action = _produitController.Update(produitToEdit.IdProduit, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code NOT_FOUND (404)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // Given : Un produit enregistré
            Product produitInDb = new Product()
            {
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            // When : Je souhaite supprimé un produit depuis l'API
            IActionResult action = _produitController.Delete(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : Le produit a bien été supprimé et le code HTTP est NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Produits.RemoveRange(_context.Produits);
            _context.SaveChanges();
        }
    }
}