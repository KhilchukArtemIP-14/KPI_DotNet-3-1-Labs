using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorage.API.Authorization
{
    public class CanAccessUserSummaryHandler: AuthorizationHandler<CanAccessUserSummaryRequirement, string>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CanAccessUserSummaryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanAccessUserSummaryRequirement requirement,
            string requestedUserId)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }

            if (requestedUserId == appUser.Id || context.User.IsInRole("Staff"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
