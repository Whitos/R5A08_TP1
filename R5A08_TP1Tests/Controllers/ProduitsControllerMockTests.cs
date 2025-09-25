using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    [TestCategory("mock")]
    public class ProduitsControllerMockTests
    {
        private readonly ProductsDbContext _context;
        private readonly ProductsController _produitController;
        private Mock<IDataRepository<Product>> _dataRepository;

        public ProduitsControllerMockTests()
        {
            _dataRepository = new Mock<IDataRepository<Product>>();
            _produitController = new ProductsController(_dataRepository.Object);
        }


        [TestMethod]
        public void ShouldGetProduct()
        {
            // Given : Un produit en enregistré
            Product produitInDb = new()
            {
                IdProduit = 30,
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitInDb.IdProduit))
                .ReturnsAsync(produitInDb);

            // When : On appelle la méthode GET de l'API pour récupérer le produit
            ActionResult<Product> action = _produitController.Get(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : On récupère le produit et le code de retour est 200
            _dataRepository.Verify(manager => manager.GetByIdAsync(produitInDb.IdProduit), Times.Once);

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Product));

            Product returnProduct = action.Value;
            Assert.AreEqual(produitInDb, returnProduct);
        }

        [TestMethod]
        public void ShouldDeleteProduct()
        {
            // Given : Un produit enregistré
            Product produitInDb = new()
            {
                IdProduit = 20,
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitInDb.IdProduit))
                .ReturnsAsync(produitInDb);

            _dataRepository
                .Setup(manager => manager.DeleteAsync(produitInDb));

            // When : On souhaite supprimer un produit depuis l'API
            IActionResult action = _produitController.Delete(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : Le produit a bien été supprimé et le code HTTP est NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NoContentResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitInDb.IdProduit), Times.Once);
            _dataRepository.Verify(manager => manager.DeleteAsync(produitInDb), Times.Once);
        }

        [TestMethod]
        public void ShouldNotDeleteProductBecauseProductDoesNotExist()
        {
            // Given : Un produit enregistré
            Product produitInDb = new()
            {
                IdProduit = 30,
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitInDb.IdProduit))
                .ReturnsAsync((Product)null);

            // When : On souhaite supprimer un produit depuis l'API
            IActionResult action = _produitController.Delete(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : Le produit a bien été supprimé et le code HTTP est NO_CONTENT (204)
            _dataRepository.Verify(manager => manager.GetByIdAsync(produitInDb.IdProduit), Times.Once);

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ShouldGetAllProducts()
        {
            // Given : Des produits enregistrées
            IEnumerable<Product> productInDb = [
                new()
            {
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            },
            new()
            {
                NameProduct = "Armoir",
                Description = "Une superbe armoire",
                NamePhoto = "Une superbe armoire jaune",
                UriPhoto = "https://ikea.fr/armoire-jaune.jpg"
            }
            ];

            _dataRepository
                .Setup(manager => manager.GetAllAsync())
                .ReturnsAsync(new ActionResult<IEnumerable<Product>>(productInDb));

            // When : On souhaite récupérer tous les produits
            var products = _produitController.GetAll().GetAwaiter().GetResult();

            // Then : Tous les produits sont récupérés
            Assert.IsNotNull(products);
            Assert.IsInstanceOfType(products.Value, typeof(IEnumerable<Product>));
            Assert.IsTrue(productInDb.SequenceEqual(products.Value));

            _dataRepository.Verify(manager => manager.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public void GetProductShouldReturnNotFound()
        {
            //Given : Pas de produit trouvé par le manager
            _dataRepository
                .Setup(manager => manager.GetByIdAsync(30))
                .ReturnsAsync(new ActionResult<Product>((Product)null));

            // When : On appelle la méthode get de mon api pour récupérer le produit
            ActionResult<Product> action = _produitController.Get(30).GetAwaiter().GetResult();

            // Then : On ne renvoie rien et on renvoie NOT_FOUND (404)
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Ne renvoie pas 404");
            Assert.IsNull(action.Value, "Le produit n'est pas null");

            _dataRepository.Verify(manager => manager.GetByIdAsync(30), Times.Once);
        }

        [TestMethod]
        public void ShouldCreateProduct()
        {
            // Given : Un produit a enregistré
            Product productToInsert = new()
            {
                IdProduit = 30,
                NameProduct = "Chaise",
                Description = "Une superbe chaise",
                NamePhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.AddAsync(productToInsert));

            // When : On appel la méthode POST de l'API pour enregistrer le produit
            ActionResult<Product> action = _produitController.Create(productToInsert).GetAwaiter().GetResult();

            // Then : Le produit est bien enregistré et le code renvoyé et CREATED (201)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));

            _dataRepository.Verify(manager => manager.AddAsync(productToInsert), Times.Once);
        }

        [TestMethod]
        public void ShouldUpdateProduct()
        {
            // Given : Un produit à mettre à jour
            Product produitToEdit = new()
            {
                IdProduit = 20,
                NameProduct = "Bureau",
                Description = "Un super bureau",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            // Une fois enregistré, on modifie certaines propriétés 
            Product updatedProduit = new()
            {
                IdProduit = 20,
                NameProduct = "Lit",
                Description = "Un super lit",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitToEdit.IdProduit))
                .ReturnsAsync(produitToEdit);

            _dataRepository
                .Setup(manager => manager.UpdateAsync(produitToEdit, updatedProduit));

            // When : On appelle la méthode PUT du controller pour mettre à jour le produit
            IActionResult action = _produitController.Update(produitToEdit.IdProduit, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que le produit a bien été modifié et que le code renvoyé et NO_CONTENT (204)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NoContentResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitToEdit.IdProduit), Times.Once);
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseIdInUrlIsDifferent()
        {
            // Given : Un produit à mettre à jour
            Product produitToEdit = new()
            {
                IdProduit = 20,
                NameProduct = "Bureau",
                Description = "Un super bureau",
                NamePhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };


            // When : On appelle la méthode PUT du controller pour mettre à jour le produit,
            // mais en précisant un ID différent de celui du produit enregistré
            IActionResult action = _produitController.Update(1, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code BAD_REQUEST (400)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitToEdit.IdProduit), Times.Never);
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Product>()), Times.Never);
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

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitToEdit.IdProduit))
                .ReturnsAsync((Product)null);

            // When : On appelle la méthode PUT du controller pour mettre à jour un produit qui n'est pas enregistré
            IActionResult action = _produitController.Update(produitToEdit.IdProduit, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code NOT_FOUND (404)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitToEdit.IdProduit), Times.Once);
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Product>()), Times.Never);

        }
    }
}