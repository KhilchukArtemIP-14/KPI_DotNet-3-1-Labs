using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.DAL.Models.Domain;
namespace GoodsStorageApi.IntegrationTests
{
    public class GoodsControllerTests:IClassFixture<WebAppFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public GoodsControllerTests(WebAppFactory factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task GetAll_ReturnsOkResultWithData()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/goods");

            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var goods = JsonConvert.DeserializeObject<IEnumerable<GoodDTO>>(content);

            Assert.NotNull(goods);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOkResultWithData()
        {
            var client = _factory.CreateClient();
            var existingId = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98");

            var response = await client.GetAsync($"/api/goods/{existingId}");

            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var good = JsonConvert.DeserializeObject<GoodDTO>(content);

            Assert.NotNull(good);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            var nonExistingId = Guid.NewGuid();

            var response = await client.GetAsync($"/api/goods/{nonExistingId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task Create_ValidModelAuthorized_ReturnsCreatedAtAction()
        {
            var client = _factory.CreateClient();
            var jwt = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));

            var validModel = new GoodDTO
            {
                Name = "Test Good",
                Description = "Test Description",
                Unit = "Test Unit",
                Price = 10.0m,
                AvailableAmount = 100
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(validModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/goods", jsonContent);

            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            var createdGood = JsonConvert.DeserializeObject<GoodDTO>(content);

            Assert.NotNull(createdGood);
            Assert.True(createdGood.Name == validModel.Name && createdGood.Description == validModel.Description);
        }

        [Fact]
        public async Task Create_InvalidModelAuthorized_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));

            var invalidModel = new GoodDTO(); 
            var jsonContent = new StringContent(JsonConvert.SerializeObject(invalidModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/goods", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task Create_AsCustomer_ReturnsForbidden()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetCustomerJwt(client));

            var invalidModel = new GoodDTO();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(invalidModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/goods", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingIdAndValidModel_ReturnsOkResultWithData()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));

            var existingId = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98");
            var validModel = new GoodDTO
            {
                Name = "Updated Good1",
                Description = "Update description1",
                Unit = "Updated Unit 1",
                Price = 20.0m,
                AvailableAmount = 40,
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(validModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/goods/{existingId}", jsonContent);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var updatedGood = JsonConvert.DeserializeObject<GoodDTO>(content);

            Assert.NotNull(updatedGood);
            Assert.True(updatedGood.Name == validModel.Name && updatedGood.Description == validModel.Description);
        }
        [Fact]
        public async Task Update_UnAuthorized_ReturnsForbidden()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetCustomerJwt(client));

            var existingId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48");
            var validModel = new GoodDTO();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(validModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/goods/{existingId}", jsonContent);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Update_NonExistingId_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));
            var nonExistingId = Guid.NewGuid();
            var validModel = new GoodDTO
            {
                Name = "Updated Good",
                Description = "Updated Description",
                Unit = "Updated Unit",
                Price = 15.0m,
                AvailableAmount = 50
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(validModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/goods/{nonExistingId}", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsOkResultWithData()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));
            var existingId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48");

            var response = await client.DeleteAsync($"/api/goods/{existingId}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var deletedGood = JsonConvert.DeserializeObject<GoodDTO>(content);

            Assert.NotNull(deletedGood);
        }

        [Fact]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetStaffJwt(client));
            var nonExistingId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/goods/{nonExistingId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_UnAuthorized_ReturnsForbidden()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await JwtTokenMock.GetCustomerJwt(client));
            var nonExistingId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/goods/{nonExistingId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}

