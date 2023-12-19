using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorageApi.IntegrationTests
{
    public static class JwtTokenMock
    {
        public static async Task<string> GetStaffJwt(HttpClient client)
        {
            var tmp = DateTime.Now.Ticks;
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
                Email = registerDto.Email,
                Password = registerDto.Password
            }), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/api/auth/login", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeAnonymousType(responseContent, new { Jwt = "" });
            return responseData.Jwt;
        }
        public static async Task<string> GetCustomerJwt(HttpClient client)
        {
            var tmp = DateTime.Now.Ticks;
            var registerDto = new RegisterUserDTO
            {
                Name = $"string{tmp}",
                Email = $"user{tmp}@example.com",
                Password = "string12345",
                PhoneNumber = "212-456-7890",
                Roles = new string[] { "Customer" }
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/register", content);
            response.EnsureSuccessStatusCode();

            content = new StringContent(JsonConvert.SerializeObject(new LoginUserDTO()
            {
                Email = registerDto.Email,
                Password = registerDto.Password
            }), Encoding.UTF8, "application/json");

            response = await client.PostAsync("/api/auth/login", content);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeAnonymousType(responseContent, new { Jwt = "" });
            return responseData.Jwt;
        }
    }
}
