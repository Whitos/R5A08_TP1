using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using R5A08_TP1.Shared.DTO.Products;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace R5A08_TP1Tests.Controllers.E2E
{
    [TestClass]
    [TestCategory("E2E")]
    public class ProductsControllerE2ETests
    {
        private readonly HttpClient _client;

        public ProductsControllerE2ETests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task ShouldGetAllProducts()
        {
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(body);

            Assert.IsNotNull(products);
        }

        [TestMethod]
        public async Task ShouldGetProductById()
        {
            var response = await _client.GetAsync("/api/products/1");
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task ShouldCreateProduct()
        {
            var dto = new ProductCreateDto { NameProduct = "E2EProduct" };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/products", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task ShouldUpdateProduct()
        {
            var dto = new ProductUpdateDto { NameProduct = "E2EUpdatedProduct" };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/products/1", content);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task ShouldDeleteProduct()
        {
            var response = await _client.DeleteAsync("/api/products/1");
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
