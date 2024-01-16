using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorage.API.Authorization
{
    public class CanAccessRequestHandler: AuthorizationHandler<CanAccessRequestRequirement, RequestDTO>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CanAccessRequestHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanAccessRequestRequirement requirement,
            RequestDTO resource)
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
