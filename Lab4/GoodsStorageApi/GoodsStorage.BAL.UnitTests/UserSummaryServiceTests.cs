using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace GoodsStorage.BAL.UnitTests
{
    public class UserSummaryServiceTests
    {
        [Fact]
        public async Task GetUserSummaryById_ExistingUserId_ReturnsUserSummary()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var userSummaryService = new UserSummaryService(userManagerMock.Object);
            var existingUserId = "122";

            var result = await userSummaryService.GetUserSummaryById(existingUserId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetUserSummaryById_NonExistingUserId_ReturnsErrorResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var userSummaryService = new UserSummaryService(userManagerMock.Object);
            var nonExistingUserId = "125";

            var result = await userSummaryService.GetUserSummaryById(nonExistingUserId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateUserSummaryById_ExistingUserId_ReturnsUpdatedUserSummary()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var userSummaryService = new UserSummaryService(userManagerMock.Object);
            var existingUserId = "123";
            var updatedUserSummary = new UpdateUserSummaryDTO { PhoneNumber = "987-654-3210", UserName = "User2" };

            var result = await userSummaryService.UpdateUserSummaryById(updatedUserSummary, existingUserId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateUserSummaryById_NonExistingUserId_ReturnsErrorResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var userSummaryService = new UserSummaryService(userManagerMock.Object);
            var nonExistingUserId = "125";
            var updatedUserSummary = new UpdateUserSummaryDTO { UserName = "UpdatedName", PhoneNumber = "9876543210" };

            var result = await userSummaryService.UpdateUserSummaryById(updatedUserSummary, nonExistingUserId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateUserSummaryById_InvalidData_ReturnsErrorResponse()
        {
            var userManagerMock = MockUserManager.CreateMockUserManager();
            var userSummaryService = new UserSummaryService(userManagerMock.Object);
            var existingUserId = "123";
            var invalidUpdatedUserSummary = new UpdateUserSummaryDTO(); 

            var result = await userSummaryService.UpdateUserSummaryById(invalidUpdatedUserSummary, existingUserId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        
    }
}
