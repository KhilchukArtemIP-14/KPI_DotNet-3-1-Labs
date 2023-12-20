using Azure.Identity;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace GoodsStorage.BAL.Services.Implementations
{
    public class UserSummaryService : IUserSummaryService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserSummaryService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<UserSummaryDTO>> GetUserSummaryById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
 
            if (user != null)
            {
                try
                {
                    var summary = new UserSummaryDTO()
                    {
                        UserId= user.Id,
                        UserName = user.UserName,
                        UserEmail = user.Email,
                        PhoneNumber = user.PhoneNumber
                    };
                    return BaseResponse<UserSummaryDTO>.OkResponse(summary);
                }
                catch(Exception ex)
                {
                    return BaseResponse<UserSummaryDTO>.ErrorResponse($"User summary retrieval exception : {0}");
                }
            }

            return BaseResponse<UserSummaryDTO>.ErrorResponse("User summary retreival error: could not get user");
        }

        public async Task<BaseResponse<UserSummaryDTO>> UpdateUserSummaryById(UpdateUserSummaryDTO updatedUserSummary, string userId)
        {

            ValidationContext context = new ValidationContext(updatedUserSummary, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(updatedUserSummary, context, validationResults, true))
            {
                string message = String.Format("Creating good dto error: {0}", string.Join(", ", validationResults.Select(res => res.ErrorMessage).ToList()));

                return BaseResponse<UserSummaryDTO>.ErrorResponse(message);
            }

            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                try
                {
                    user.UserName=updatedUserSummary.UserName;
                    user.PhoneNumber=updatedUserSummary.PhoneNumber;

                    await _userManager.UpdateAsync(user);

                    var updatedSummary = new UserSummaryDTO()
                    {
                        UserId=user.Id,
                        UserName = user.UserName,
                        UserEmail = user.Email,
                        PhoneNumber = user.PhoneNumber
                    };
                    return BaseResponse<UserSummaryDTO>.OkResponse(updatedSummary);
                }
                catch (Exception ex)
                {
                    return BaseResponse<UserSummaryDTO>.ErrorResponse($"User summary update exception: {ex.Message}");
                }
            }

            return BaseResponse<UserSummaryDTO>.ErrorResponse("User summary update error: could not get user");
        }

    }
}
