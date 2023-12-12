using Microsoft.AspNetCore.Authorization;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
namespace GoodsStorage.API.Authorization
{
    public class CanAccessPurchaseHandler: AuthorizationHandler<CanAccessPurchaseRequirement, PurchaseDTO>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CanAccessPurchaseHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanAccessPurchaseRequirement requirement,
            PurchaseDTO resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }

            if (resource.UserId == appUser.Id || context.User.IsInRole("Staff"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
