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
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }
            
            if (resource.CustomerId == appUser.Id || context.User.IsInRole("Staff"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
