using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GoodsStorage.BAL.UnitTests
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var configMock = MockConfiguration.CreateMockConfiguration();
            var authService = new AuthService(userManagerMock.Object, configMock);
            var loginUserDTO = new LoginUserDTO { Email = "oof1@gmail.com", Password = "TestPassword" };

            var result = await authService.Login(loginUserDTO);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsErrorResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var configMock = MockConfiguration.CreateMockConfiguration();
            var authService = new AuthService(userManagerMock.Object, configMock);
            var loginUserDTO = new LoginUserDTO { Email = "oof5@gmail.com", Password = "InvalidPassword" };

            var result = await authService.Login(loginUserDTO);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsSuccessResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var configMock = MockConfiguration.CreateMockConfiguration();
            var authService = new AuthService(userManagerMock.Object, configMock);
            var registerUserDTO = new RegisterUserDTO
            {
                Name = "NewUser",
                Email = "newuser@example.com",
                PhoneNumber = "123-456-7890",
                Password = "TestPassword",
                Roles = new string[] { "User" }
            };

            var result = await authService.Register(registerUserDTO);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Succeeded);
        }

        [Fact]
        public async Task Register_InvalidUser_ReturnsErrorResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            userManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Registration failed" }));

            var configMock = MockConfiguration.CreateMockConfiguration();
            var authService = new AuthService(userManagerMock.Object, configMock);
            var registerUserDTO = new RegisterUserDTO
            {
                Name = "InvalidUser",
                Email = "invaliduser@example.com",
                PhoneNumber = "987-654-3210",
                Password = "InvalidPassword"
            };

            var result = await authService.Register(registerUserDTO);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }
    }
}
