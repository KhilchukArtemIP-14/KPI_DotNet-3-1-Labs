using Microsoft.AspNetCore.Identity;
using Moq;

namespace GoodsStorage.BAL.UnitTests
{
    public static class MockUserManager
    {
        public static Mock<UserManager<IdentityUser>> CreateMockUserManager()
        {
            var users = new Dictionary<string, IdentityUser>
        {
            {
                "122",
                new IdentityUser { Id = "122", Email = "oof1@gmail.com", PhoneNumber = "123-456-7890", UserName = "User1" }
            },
            {
                "123",
                new IdentityUser { Id = "123", Email = "ooof2@gmail.com", PhoneNumber = "987-654-3210", UserName = "User2" }
            },
        };

            var userManagerMock = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
        .ReturnsAsync((string userId) => users.TryGetValue(userId, out var user) ? user : null);

            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.Values.FirstOrDefault(u => u.Email == email));

            userManagerMock.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(new List<string> { "User" });

            userManagerMock.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(m => m.AddToRolesAsync(It.IsAny<IdentityUser>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            return userManagerMock;
        }
    }
}
