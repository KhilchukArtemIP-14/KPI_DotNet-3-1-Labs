using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Services.Interfaces
{
    public interface IUserSummaryService
    {
        public Task<BaseResponse<UserSummaryDTO>> GetUserSummaryById(string userId);
        public Task<BaseResponse<UserSummaryDTO>> UpdateUserSummaryById(UpdateUserSummaryDTO updatedUserSummary, string userId);
    }
}
