using Microsoft.AspNetCore.Authorization;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
            var userIdClaim = context.User.FindFirst("userId");
            if (userIdClaim != null && resource.UserId == userIdClaim.Value)
            {
                context.Succeed(requirement);
                return;
            }

            if (context.User.IsInRole("Staff"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
