using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorageApi.IntegrationTests
{
    public class RequestsControllerTests : IClassFixture<WebAppFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RequestsControllerTests(WebAppFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ValidRequest_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var jwtToken = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await client.GetAsync("/api/requests");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_UnauthorizedRequest_ReturnsForbidden()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/requests");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task GetById_UnauthorizedRequest_ReturnsUnauthorized()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/requests/636e2574-2314-47f6-b0a8-51c50c57b06c"); 

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            var jwtToken = await JwtTokenMock.GetStaffJwt(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await client.GetAsync("/api/requests/asdasdasddsassd");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
