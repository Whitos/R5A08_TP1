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
        private readonly ProduitsDbContext _context;
        private readonly ProduitsController _produitController;
        private Mock<IDataRepository<Produit>> _dataRepository;

        public ProduitsControllerMockTests()
        {
            _dataRepository = new Mock<IDataRepository<Produit>>();
            _produitController = new ProduitsController(_dataRepository.Object);
        }


        [TestMethod]
        public void ShouldGetProduct()
        {
            // Given : Un produit en enregistré
            Produit produitInDb = new()
            {
                IdProduit = 30,
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitInDb.IdProduit))
                .ReturnsAsync(produitInDb);

            // When : On appelle la méthode GET de l'API pour récupérer le produit
            ActionResult<Produit> action = _produitController.Get(produitInDb.IdProduit).GetAwaiter().GetResult();

            // Then : On récupère le produit et le code de retour est 200
            _dataRepository.Verify(manager => manager.GetByIdAsync(produitInDb.IdProduit), Times.Once);

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Value, typeof(Produit));

            Produit returnProduct = action.Value;
            Assert.AreEqual(produitInDb, returnProduct);
        }

        [TestMethod]
        public void ShouldDeleteProduct()
        {
            // Given : Un produit enregistré
            Produit produitInDb = new()
            {
                IdProduit = 20,
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
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
            Produit produitInDb = new()
            {
                IdProduit = 30,
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitInDb.IdProduit))
                .ReturnsAsync((Produit)null);

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
            IEnumerable<Produit> productInDb = [
                new()
            {
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            },
            new()
            {
                NomProduit = "Armoir",
                Description = "Une superbe armoire",
                NomPhoto = "Une superbe armoire jaune",
                UriPhoto = "https://ikea.fr/armoire-jaune.jpg"
            }
            ];

            _dataRepository
                .Setup(manager => manager.GetAllAsync())
                .ReturnsAsync(new ActionResult<IEnumerable<Produit>>(productInDb));

            // When : On souhaite récupérer tous les produits
            var products = _produitController.GetAll().GetAwaiter().GetResult();

            // Then : Tous les produits sont récupérés
            Assert.IsNotNull(products);
            Assert.IsInstanceOfType(products.Value, typeof(IEnumerable<Produit>));
            Assert.IsTrue(productInDb.SequenceEqual(products.Value));

            _dataRepository.Verify(manager => manager.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public void GetProductShouldReturnNotFound()
        {
            //Given : Pas de produit trouvé par le manager
            _dataRepository
                .Setup(manager => manager.GetByIdAsync(30))
                .ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // When : On appelle la méthode get de mon api pour récupérer le produit
            ActionResult<Produit> action = _produitController.Get(30).GetAwaiter().GetResult();

            // Then : On ne renvoie rien et on renvoie NOT_FOUND (404)
            Assert.IsInstanceOfType(action.Result, typeof(NotFoundResult), "Ne renvoie pas 404");
            Assert.IsNull(action.Value, "Le produit n'est pas null");

            _dataRepository.Verify(manager => manager.GetByIdAsync(30), Times.Once);
        }

        [TestMethod]
        public void ShouldCreateProduct()
        {
            // Given : Un produit a enregistré
            Produit productToInsert = new()
            {
                IdProduit = 30,
                NomProduit = "Chaise",
                Description = "Une superbe chaise",
                NomPhoto = "Une superbe chaise bleu",
                UriPhoto = "https://ikea.fr/chaise.jpg"
            };

            _dataRepository
                .Setup(manager => manager.AddAsync(productToInsert));

            // When : On appel la méthode POST de l'API pour enregistrer le produit
            ActionResult<Produit> action = _produitController.Create(productToInsert).GetAwaiter().GetResult();

            // Then : Le produit est bien enregistré et le code renvoyé et CREATED (201)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action.Result, typeof(CreatedAtActionResult));

            _dataRepository.Verify(manager => manager.AddAsync(productToInsert), Times.Once);
        }

        [TestMethod]
        public void ShouldUpdateProduct()
        {
            // Given : Un produit à mettre à jour
            Produit produitToEdit = new()
            {
                IdProduit = 20,
                NomProduit = "Bureau",
                Description = "Un super bureau",
                NomPhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            // Une fois enregistré, on modifie certaines propriétés 
            Produit updatedProduit = new()
            {
                IdProduit = 20,
                NomProduit = "Lit",
                Description = "Un super lit",
                NomPhoto = "Un super bureau bleu",
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
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Produit>()), Times.Once);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseIdInUrlIsDifferent()
        {
            // Given : Un produit à mettre à jour
            Produit produitToEdit = new()
            {
                IdProduit = 20,
                NomProduit = "Bureau",
                Description = "Un super bureau",
                NomPhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };


            // When : On appelle la méthode PUT du controller pour mettre à jour le produit,
            // mais en précisant un ID différent de celui du produit enregistré
            IActionResult action = _produitController.Update(1, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code BAD_REQUEST (400)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(BadRequestResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitToEdit.IdProduit), Times.Never);
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Produit>()), Times.Never);
        }

        [TestMethod]
        public void ShouldNotUpdateProductBecauseProductDoesNotExist()
        {
            // Given : Un produit à mettre à jour qui n'est pas enregistré
            Produit produitToEdit = new()
            {
                IdProduit = 20,
                NomProduit = "Bureau",
                Description = "Un super bureau",
                NomPhoto = "Un super bureau bleu",
                UriPhoto = "https://ikea.fr/bureau.jpg"
            };

            _dataRepository
                .Setup(manager => manager.GetByIdAsync(produitToEdit.IdProduit))
                .ReturnsAsync((Produit)null);

            // When : On appelle la méthode PUT du controller pour mettre à jour un produit qui n'est pas enregistré
            IActionResult action = _produitController.Update(produitToEdit.IdProduit, produitToEdit).GetAwaiter().GetResult();

            // Then : On vérifie que l'API renvoie un code NOT_FOUND (404)
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(NotFoundResult));

            _dataRepository.Verify(manager => manager.GetByIdAsync(produitToEdit.IdProduit), Times.Once);
            _dataRepository.Verify(manager => manager.UpdateAsync(produitToEdit, It.IsAny<Produit>()), Times.Never);

        }
    }
}