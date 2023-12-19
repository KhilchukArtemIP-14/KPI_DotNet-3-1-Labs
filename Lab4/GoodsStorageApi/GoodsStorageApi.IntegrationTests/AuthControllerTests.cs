using GoodsStorage.BAL.Models.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorageApi.IntegrationTests
{
    public class AuthControllerTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            var tmp = DateTime.Now.Ticks;
            var client = _factory.CreateClient();
            var registerDto = new RegisterUserDTO
            {
                Name = $"string{tmp}",
                Email = $"user{tmp}@example.com",
                Password = "string12345",
                PhoneNumber = "212-456-7890",
                Roles = new string[] { "Staff" }
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/register", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal("User successfully created", responseContent);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsJwtToken()
        {
            var tmp = DateTime.Now.Ticks;
            var client = _factory.CreateClient();
            var registerDto = new RegisterUserDTO
            {
                Name = $"string{tmp}",
                Email = $"user{tmp}@example.com",
                Password = "string12345",
                PhoneNumber = "212-456-7890",
                Roles = new string[] { "Staff" }
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/register", content);
            response.EnsureSuccessStatusCode();

            content = new StringContent(JsonConvert.SerializeObject(new LoginUserDTO()
            {
                Email=registerDto.Email,
                Password=registerDto.Password
            }), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/api/auth/login", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeAnonymousType(responseContent, new { Jwt = "" });
            Assert.NotNull(responseData.Jwt);
        }
    }
}
