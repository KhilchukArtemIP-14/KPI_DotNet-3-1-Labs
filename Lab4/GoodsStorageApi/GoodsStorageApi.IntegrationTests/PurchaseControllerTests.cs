using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorageApi.IntegrationTests
{
    public class PurchaseControllerTests:IClassFixture<WebAppFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PurchaseControllerTests(WebAppFactory factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetAll_WhenAuthorized_ReturnsOkResult()
        {
            var client= _factory.CreateClient();
            var jwtToken = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await client.GetAsync($"/api/purchases");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetById_WhenAuthorized_ReturnsOkResult()
        {
            var client = _factory.CreateClient();
            var jwtToken = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var purchaseId = Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c");

            var response = await client.GetAsync($"/api/purchases/{purchaseId}");

            response.EnsureSuccessStatusCode();
            var purchase = await response.Content.ReadFromJsonAsync<PurchaseDTO>();
            Assert.NotNull(purchase);
            Assert.Equal(purchaseId, purchase.Id);
        }

        [Fact]
        public async Task Create_WhenAuthorized_ReturnsCreatedAtAction()
        {
            var client=_factory.CreateClient();
            var jwtToken = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var createModel = new CreatePurchaseDTO
            {
                UserId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                StaffRepId = "656e2574-2314-47f6-b0a8-51c50c57b06f",
                Date = DateTime.Now,
                PurchaseGoodDTOs = new List<CreatePurchaseGoodDTO>
            {
                new CreatePurchaseGoodDTO
                {
                    GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                    Amount = 5
                }
            }
            };

            var response = await client.PostAsJsonAsync("/api/purchases", createModel);

            response.EnsureSuccessStatusCode();
            var createdPurchase = await response.Content.ReadFromJsonAsync<PurchaseDTO>();
            Assert.NotNull(createdPurchase);
            Assert.True(createdPurchase.Id != Guid.Empty);
        }
        [Fact]
        public async Task GetAll_WhenUnauthorized_ReturnsForbidResult()
        {
            var response = await _factory.CreateClient().GetAsync("/api/purchases");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_WhenUnauthorized_ReturnsForbidResult()
        {
            var purchaseId = Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c");

            var response = await _factory.CreateClient().GetAsync($"/api/purchases/{purchaseId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenUnauthorized_ReturnsForbidResult()
        {
            var createModel = new CreatePurchaseDTO
            {
                UserId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                StaffRepId = "656e2574-2314-47f6-b0a8-51c50c57b06f",
                Date = DateTime.Now,
                PurchaseGoodDTOs = new List<CreatePurchaseGoodDTO>
            {
                new CreatePurchaseGoodDTO
                {
                    GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                    Amount = 5
                }
            }
            };

            var response = await _factory.CreateClient().PostAsJsonAsync("/api/purchases", createModel);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
