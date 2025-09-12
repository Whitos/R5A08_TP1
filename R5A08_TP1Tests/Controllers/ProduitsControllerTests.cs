using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R5A08_TP1.Controllers;
using R5A08_TP1.Models.DataManager;
using R5A08_TP1.Models.EntityFramework;
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
        private readonly ProduitManager _manager;

        public ProduitsControllerTests()
        {
            _context = new ProduitsDbContext();
            _manager = new ProduitManager();
            _produitController = new ProduitsController(_manager);
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
    }
}