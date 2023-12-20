using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorage.API.Authorization
{
    public class CanCreateRequestHandler : AuthorizationHandler<CanCreateRequestRequirement, ModifyRequestDTO>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CanCreateRequestHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanCreateRequestRequirement requirement,
            ModifyRequestDTO resource)
        {
            var userIdClaim = context.User.FindFirst("userId");
            if (userIdClaim != null && resource.CustomerId == userIdClaim.Value)
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
