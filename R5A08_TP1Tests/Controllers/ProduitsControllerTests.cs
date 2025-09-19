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
        private readonly ProduitsDbContext _context;
        private readonly ProduitsController _produitController;
        private IDataRepository<Produit> _dataRepository;

        public ProduitsControllerTests()
        {
            _context = new ProduitsDbContext();

            _dataRepository = new ProduitManager(_context);
            _produitController = new ProduitsController(_dataRepository);
        }

        [TestMethod()]
        public void ShouldGetAllProduits()
        {
            IEnumerable<Produit> produitsInDb = [
                new ()
                {
                    NomProduit = "Chaise",
                    Description = "Une chaise nul",
                    NomPhoto = "chaise.jpg",
                    UriPhoto = "https://ikea.fr/chaise.jpg"
                },
                new ()
                {
                    NomProduit = "Armoir",
                    Description = "Une superbe armoire",
                    NomPhoto = "Une superbe armoire jaune",
                    UriPhoto = "https://ikea.fr/armoire-jaune.jpg"
                }
            ];

            _context.Produits.AddRange(produitsInDb);
            _context.SaveChanges();

            // When: j'appelle la méthode Get de mon api pour récuperer tous les produits
            var products = _produitController.GetAll().GetAwaiter().GetResult();

            // Then : Tous les produits sont récupérés
            Assert.IsNotNull(products);
            Assert.IsInstanceOfType(products.Value, typeof(IEnumerable<Produit>));
        }

        [TestMethod()]
        public void ShouldGetProduit()
        {
            // Given: un produit en base de données 
            Produit produitInDb = new Produit
            {
                NomProduit = "Chaise",
                Description = "Une chaise nul",
                NomPhoto = "chaise.jpg",
            };

            _context.Produits.Add(produitInDb);
            _context.SaveChanges();


            // When: j'appelle la méthode Get de mon api pour récuperer le produit

            ActionResult<Produit> action = _produitController.Get(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then: on recupere le produit et le code de retour est de 200 (OK)

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Produit));

            Produit returnProduct = action.Value;
            Assert.AreEqual(produitInDb.IdProduit, returnProduct.IdProduit);
            // Assert.IsInstanceOfType(action.Result, typeof(OkObjectResult));

        }

        [TestMethod()]
        public void ProductShouldReturnNotFound()
        {
            // When : J'appelle la méthode get de mon api pour récupérer le produit
            ActionResult<Produit> action = _produitController.Get(0).GetAwaiter().GetResult();

            // Then : On ne renvoie rien et on renvoie 404
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Ne renvoie pas 404");
            Assert.IsNull(action.Value, "Le produit n'est pas null");
        }

        [TestMethod]
        public void ShouldCreateProduct()
        {
            // Given
            Produit productToInsert = new Produit()
            {
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            // When
            ActionResult<Produit> action = _produitController.Create(productToInsert).GetAwaiter().GetResult();

            // Then
            Produit productInDb = _context.Produits.Find(productToInsert.IdProduit);

            Assert.IsNotNull(productInDb);
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod()]
        public void ShouldDeleteProduct()
        {
            // Given: un produit en base de données 
            Produit produitInDb = new Produit()
            {
                NomProduit = "Chaise3",
                Description = "Une chaise nul",
                NomPhoto = "chaise.jpg",
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
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // Given : Un produit enregistré
            Produit produitInDb = new Produit()
            {
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
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